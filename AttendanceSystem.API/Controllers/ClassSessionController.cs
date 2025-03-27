/*
    Eduardo Zamora 3/26/2025
    Controller for the ClassSession model
    Get, put, post, delete methods for the ClassSession model  
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace AttendanceSystem.API.Controllers
{
    // ApiController attribute enables automatic model validation and binding
    [ApiController]
    // Route attribute defines the base URL path for all endpoints in this controller
    [Route("api/[controller]")]
    public class ClassSessionController : ControllerBase
    {
        // Database context for performing database operations
        private readonly AttendanceSystemContext _context;

        // Constructor injection of the database context
        public ClassSessionController(AttendanceSystemContext context)
        {
            _context = context;
        }

        // GET: api/ClassSession
        // Returns all class sessions with their related Course and Quiz data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassSession>>> GetClassSessions()
        {
            // Include related entities to avoid N+1 query problems
            return await _context.ClassSessions
                .Include(cs => cs.Course)    // Load related Course data
                .Include(cs => cs.Quiz)      // Load related Quiz data
                .ToListAsync();              // Execute query and return results
        }

        // GET: api/ClassSession/5
        // Returns a single class session by its QuizId
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassSession>> GetClassSession(int id)
        {
            // Find the session and include related data
            var classSession = await _context.ClassSessions
                .Include(cs => cs.Course)    // Load related Course data
                .Include(cs => cs.Quiz)      // Load related Quiz data
                .FirstOrDefaultAsync(cs => cs.QuizId == id);  // Find by QuizId

            // Return 404 if no session found
            if (classSession == null)
            {
                return NotFound();
            }

            return classSession;
        }

        // POST: api/ClassSession
        // Creates a new class session with validation and password generation
        [HttpPost]
        public async Task<ActionResult<ClassSession>> CreateClassSession(ClassSession classSession)
        {
            // Validate that the referenced course exists
            var course = await _context.Courses.FindAsync(classSession.CourseId);
            if (course == null)
            {
                return BadRequest("Invalid CourseId");
            }

            // Validate that the referenced quiz exists
            var quiz = await _context.Quizzes.FindAsync(classSession.QuizId);
            if (quiz == null)
            {
                return BadRequest("Invalid QuizId");
            }

            // Generate a secure random password for attendance verification
            classSession.Password = GenerateRandomPassword();

            // Add the new session to the database
            _context.ClassSessions.Add(classSession);
            await _context.SaveChangesAsync();

            // Return 201 Created with the location of the new resource
            return CreatedAtAction(nameof(GetClassSession), new { id = classSession.QuizId }, classSession);
        }

        // PUT: api/ClassSession/5
        // Updates an existing class session with concurrency handling
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassSession(int id, ClassSession classSession)
        {
            // Ensure the ID in URL matches the ID in request body
            if (id != classSession.QuizId)
            {
                return BadRequest();
            }

            // Find the existing session to update
            var existingSession = await _context.ClassSessions.FindAsync(id);
            if (existingSession == null)
            {
                return NotFound();
            }

            // Only update allowed fields (SessionDate and CourseId)
            // Note: Password is not updated here for security
            existingSession.SessionDate = classSession.SessionDate;
            existingSession.CourseId = classSession.CourseId;

            try
            {
                // Attempt to save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrent update conflicts
                if (!ClassSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;  // Re-throw if it's a different type of error
                }
            }

            // Return 204 No Content on successful update
            return NoContent();
        }

        // DELETE: api/ClassSession/5
        // Deletes a class session by its QuizId
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassSession(int id)
        {
            // Find the session to delete
            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession == null)
            {
                return NotFound();
            }

            // Remove the session and save changes
            _context.ClassSessions.Remove(classSession);
            await _context.SaveChangesAsync();

            // Return 204 No Content on successful deletion
            return NoContent();
        }

        // POST: api/ClassSession/5/verify-password
        // Verifies if a provided password matches the session's password
        [HttpPost("{id}/verify-password")]
        public async Task<ActionResult<bool>> VerifyPassword(int id, [FromBody] string password)
        {
            // Find the session by ID
            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession == null)
            {
                return NotFound();
            }

            // Compare the provided password with the stored password
            return Ok(classSession.Password == password);
        }

        // Helper method to check if a class session exists
        // Used primarily for concurrency handling in updates
        private bool ClassSessionExists(int id)
        {
            // Efficiently check existence without loading the entire record
            return _context.ClassSessions.Any(e => e.QuizId == id);
        }

        // Helper method to generate a random 6-character password
        // Used when creating new class sessions
        private string GenerateRandomPassword()
        {
            // Define the character set for password generation
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            // Generate a 6-character random string
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
