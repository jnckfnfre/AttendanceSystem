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
                SessionDate = s.SessionDate,
                Utd_Id = s.Utd_Id,
                Student_Name = s.Student.FirstName + " " + s.Student.LastName, // get student name from student table
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
        // Retrieve UtdId from session
        var utdId = HttpContext.Session.GetString("UtdId");
        if (string.IsNullOrEmpty(utdId))
        {
            return BadRequest("UtdId is missing. Please log in again.");
        }

        // Validate dto fields
        if (dto == null || string.IsNullOrEmpty(dto.CourseId) || dto.SessionDate == default || dto.QuizId <= 0)
        {
            return BadRequest("Invalid submission data.");
        }

        // Validate that the student exists
        var studentExists = await _context.Students.AnyAsync(s => s.UtdId == utdId);
        if (!studentExists)
        {
            return BadRequest("Invalid Student ID.");
        }

        // Validate that the quiz exists
        var quizExists = await _context.Quizzes.AnyAsync(q => q.QuizId == dto.QuizId);
        if (!quizExists)
        {
            return BadRequest("Invalid Quiz ID.");
        }

        // Validate that the class session exists
        var sessionExists = await _context.ClassSessions.AnyAsync(cs =>
            cs.Course_Id == dto.CourseId && cs.SessionDate == dto.SessionDate);
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
            Course_Id = dto.CourseId,
            SessionDate = dto.SessionDate,
            Utd_Id = utdId, // Use the UtdId from session
            Quiz_Id = dto.QuizId,
            Ip_Address = ip,
            Submission_Time = now,
            Answer_1 = dto.Answers.ElementAtOrDefault(0) ?? "",
            Answer_2 = dto.Answers.ElementAtOrDefault(1) ?? "",
            Answer_3 = dto.Answers.ElementAtOrDefault(2) ?? "",
            Status = dto.Status
        };

        // Add the submission to the database
        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();

        
        return RedirectToAction("Confirmation", "Quiz");

        // Return the created submission
        // return CreatedAtAction(nameof(GetSubmission), new { id = submission.Submission_Id }, submission);
    }
    
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