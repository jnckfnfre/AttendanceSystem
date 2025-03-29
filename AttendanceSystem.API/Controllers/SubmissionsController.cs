/*
    David Sajdak 3/28/2025
    Controller for the Submission model
    Get, put, post methods for the Submission model
    No delete method necessary because we don't want to delete submissions
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;

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
    public async Task<IActionResult> AddSubmission([FromBody] Submission submission) {
        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSubmissions), new { id = submission.Submission_Id }, submission);
    }
    
    // PUT: api/Submissions/{id}
    // Updates a submission by id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubmission(int id, [FromBody] Submission updated) {
        var submission = await _context.Submissions.FindAsync(id);
        if (submission == null) {
            return NotFound();
        }

        // don't update PK or FKs
        submission.Ip_Address = updated.Ip_Address;
        submission.Submission_Time = updated.Submission_Time;
        submission.Answer_1 = updated.Answer_1;
        submission.Answer_2 = updated.Answer_2;
        submission.Answer_3 = updated.Answer_3;
        submission.Status = updated.Status;

        await _context.SaveChangesAsync();
        return NoContent();
    }
    
}