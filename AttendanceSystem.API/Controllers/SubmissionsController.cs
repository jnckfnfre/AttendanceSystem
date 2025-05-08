/*
    David Sajdak started: 3/28/2025
    Controller for the Submission model
    Get, put, post, delete methods for the Submission model
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;

[ApiController]
[Route("api/[controller]")]

public class SubmissionsController : Controller {
    // database context for accessing the database
    // handles all database operations for submissions
    private readonly AttendanceDbContext _context;

    // constructor for the SubmissionsController
    // injects the database context into the controller
    public SubmissionsController(AttendanceDbContext context) {
        _context = context;
    }

    // GET: api/Submissions
    // Gets all submissions
    [HttpGet]
    public async Task<IActionResult> GetSubmissions() {
        var submissions = await _context.Submissions.ToListAsync();
        return Ok(submissions);
    }

    // GET: api/Submissions/{id}
    // Gets a submission by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubmission(int id) {
        var submission = await _context.Submissions.FindAsync(id);
        if (submission == null) {
            return NotFound();
        }
        return Ok(submission);
    }

    // GET: api/Submissions/WithStudent
    // gets all submissions with student name included
    [HttpGet("WithStudent")]
    public async Task<IActionResult> GetSubmissionsWithStudent()
    {
        var submissions = await _context.Submissions
            .Include(s => s.Student) // join with student table
            .Select(s => new SubmissionWithStudentDto
            {
                Submission_Id = s.Submission_Id,
                Course_Id = s.Course_Id,
                Session_Date = s.Session_Date,
                Utd_Id = s.Utd_Id,
                Student_Name = s.Student.First_Name + " " + s.Student.Last_Name, // get student name from student table
                Quiz_Id = s.Quiz_Id,
                Submission_Time = s.Submission_Time,
                Ip_Address = s.Ip_Address,
                Answer_1 = s.Answer_1,
                Answer_2 = s.Answer_2,
                Answer_3 = s.Answer_3,
                Status = s.Status
            })
            .ToListAsync();

        return Ok(submissions);
    }

    // POST: api/Submissions
    // Creates a new submission
    [HttpPost]
    public async Task<IActionResult> AddSubmission([FromForm] SubmissionCreateDto dto)
    {
        // Check for duplicate submission
        bool alreadySubmitted = await _context.Submissions.AnyAsync(s =>
            s.Course_Id == dto.Course_Id &&
            s.Session_Date == dto.Session_Date &&
            s.Utd_Id == dto.Utd_Id &&
            s.Quiz_Id == dto.Quiz_Id);

        if (alreadySubmitted)
        {
            // Return a friendly error (API style)
            return BadRequest("You have already submitted this quiz.");
        }

        // Retrieve UtdId from session
        var utdId = HttpContext.Session.GetString("Utd_Id");
        if (string.IsNullOrEmpty(utdId))
        {
            return BadRequest("Utd_Id is missing. Please log in again.");
        }

        // Validate dto fields
        if (dto == null || string.IsNullOrEmpty(dto.Course_Id) || dto.Session_Date == default || dto.Quiz_Id <= 0)
        {
            return BadRequest("Invalid submission data.");
        }

        // Validate that the student exists
        var studentExists = await _context.Students.AnyAsync(s => s.Utd_Id == utdId);
        if (!studentExists)
        {
            return BadRequest("Invalid Student ID.");
        }

        // Validate that the quiz exists
        var quizExists = await _context.Quizzes.AnyAsync(q => q.Quiz_Id == dto.Quiz_Id);
        if (!quizExists)
        {
            return BadRequest("Invalid Quiz ID.");
        }

        // Validate that the class session exists
        var sessionExists = await _context.ClassSessions.AnyAsync(cs =>
            cs.Course_Id == dto.Course_Id && cs.Session_Date == dto.Session_Date);
        if (!sessionExists)
        {
            return BadRequest("Invalid Class Session.");
        }

        // Get the client IP
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var now = DateTime.UtcNow;

        // Create a new Submission object
        var submission = new Submission
        {
            Course_Id = dto.Course_Id,
            Session_Date = dto.Session_Date,
            Utd_Id = utdId, // Use the UtdId from session
            Quiz_Id = dto.Quiz_Id,
            Ip_Address = ip ?? "0.0.0.0",
            Submission_Time = now,
            Answer_1 = dto.Answers.ElementAtOrDefault(0) ?? "x",
            Answer_2 = dto.Answers.ElementAtOrDefault(1) ?? "x",
            Answer_3 = dto.Answers.ElementAtOrDefault(2) ?? "x",
            Status = dto.Status
        };

        // Add the submission to the database
        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();

        
        return RedirectToAction("Confirmation", "Quiz");

        // Return the created submission
        // return CreatedAtAction(nameof(GetSubmission), new { id = submission.Submission_Id }, submission);
    }

    // Eduardo Zamora 5/1/2025
    // POST: api/Submissions/bulk-create
    // Creates multiple submissions for all students in a class session
    [HttpPost("bulk-create")]
    public async Task<IActionResult> BulkCreateSubmissions([FromBody] List<SubmissionCreateDto> submissions)
    {
        if (submissions == null || !submissions.Any())
            return BadRequest("No submissions to create.");

        foreach (var dto in submissions)
        {
            var submission = new Submission
            {
                Course_Id = dto.Course_Id,
                Session_Date = dto.Session_Date,
                Utd_Id = dto.Utd_Id,
                Quiz_Id = dto.Quiz_Id,
                Ip_Address = dto.Ip_Address,
                Submission_Time = dto.Submission_Time,
                Answer_1 = dto.Answers.ElementAtOrDefault(0) ?? "x",
                Answer_2 = dto.Answers.ElementAtOrDefault(1) ?? "x",
                Answer_3 = dto.Answers.ElementAtOrDefault(2) ?? "x",
                Status = dto.Status
            };
            _context.Submissions.Add(submission);
        }

        await _context.SaveChangesAsync();
        return Ok(new { created = submissions.Count });
    }

    // Eduardo Zamora
    // PUT: api/Submissions/{id}
    // Updates only modifiable fields of a submission (answers and status)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubmission(int id, [FromBody] SubmissionUpdateDto dto) {
        var submission = await _context.Submissions.FindAsync(id);

        if (submission == null) {
            return NotFound();
        }

        // Only update modifiable fields
        submission.Answer_1 = dto.Answer_1;
        submission.Answer_2 = dto.Answer_2;
        submission.Answer_3 = dto.Answer_3;

        submission.Status = dto.Status;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    
    // David Sajdak 5/7/2025
    // PUT: api/Submissions/{id}
    // Updates only modifiable fields of a submission (answers and status)
    [HttpPut]
    public async Task<IActionResult> UpdateSubmission([FromForm] SubmissionCreateDto dto) {
        // Retrieve UtdId, course id, session date, and quiz id from session
        var utdId = HttpContext.Session.GetString("Utd_Id");
        if (string.IsNullOrEmpty(utdId))
        {
            return BadRequest("Utd_Id is missing. Please log in again.");
        }

        var courseId = HttpContext.Session.GetString("Course_Id");
        var sessionDate = HttpContext.Session.GetString("Session_Date");
        var quizId = HttpContext.Session.GetInt32("Quiz_Id");

        // parse and validate session date
        if (!DateTime.TryParse(sessionDate, out var parsedSessionDate))
            return BadRequest("Invalid session date");
        
        // grab appropiate submission
        var submission = await _context.Submissions
            .FirstOrDefaultAsync(s =>
                s.Utd_Id == utdId &&
                s.Course_Id == courseId &&
                s.Session_Date.Date == parsedSessionDate.Date &&
                s.Quiz_Id == quizId);

        if (submission == null) {
            return NotFound();
        }

        if (submission.Status == "Present") {
            return BadRequest("Cannot update a submission that is already marked as present.");
        }

        // Get the client IP
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var now = DateTime.Now;

        // Update the submission fields
        submission.Ip_Address = ip ?? "0.0.0.0";
        submission.Submission_Time = now;
        submission.Answer_1 = dto.Answers.ElementAtOrDefault(0) ?? "x";
        submission.Answer_2 = dto.Answers.ElementAtOrDefault(1) ?? "x";
        submission.Answer_3 = dto.Answers.ElementAtOrDefault(2) ?? "x";
        submission.Status = dto.Status;

        await _context.SaveChangesAsync();
        return RedirectToAction("Confirmation", "Quiz");
    }

    // DELETE: api/Submissions/{id}
    // Deletes a submission by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubmission(int id) {
        var submission = await _context.Submissions.FindAsync(id);
        if (submission == null) {
            return NotFound();
        }

        _context.Submissions.Remove(submission);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}