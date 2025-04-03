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

public class SubmissionsController : ControllerBase {
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

    // POST: api/Submissions
    // Creates a new submission
    [HttpPost]
    public async Task<IActionResult> AddSubmission([FromBody] SubmissionCreateDto dto) {
        // Validate that the student exists
        var studentExists = await _context.Students.AnyAsync(s => s.UtdId == dto.Utd_Id);
        if (!studentExists)
            return BadRequest("Invalid Student ID");

        // Validate that the quiz exists
        var quizExists = await _context.Quizzes.AnyAsync(q => q.QuizId == dto.Quiz_Id);
        if (!quizExists)
            return BadRequest("Invalid Quiz ID");

        // Validate that the class session exists
        var sessionExists = await _context.ClassSessions.AnyAsync(cs => 
            cs.Course_Id == dto.Course_Id && cs.SessionDate == dto.SessionDate);
        if (!sessionExists)
            return BadRequest("Invalid Class Session");

        var submission = new Submission 
        {
            Course_Id = dto.Course_Id,
            SessionDate = dto.SessionDate,
            Utd_Id = dto.Utd_Id,
            Quiz_Id = dto.Quiz_Id,
            Ip_Address = dto.Ip_Address,
            Submission_Time = dto.Submission_Time,
            Answer_1 = dto.Answer_1,
            Answer_2 = dto.Answer_2,
            Answer_3 = dto.Answer_3,
            Status = dto.Status
        };

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSubmissions), new { id = submission.Submission_Id }, submission);
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