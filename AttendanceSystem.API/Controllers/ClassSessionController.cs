/*
    Eduardo Zamora 3/26/2025
        Revised By David Sajdak 4/21/2025, further comments on what changed in code below
    Controller for the ClassSession model
    Routes:
    GET    /api/ClassSession                    - Get all class sessions
    GET    /api/ClassSession/{courseId}/{sessionDate} - Get specific class session
    GET    /api/ClassSession/{id}/detailed      - Get detailed class session with related data
    POST   /api/ClassSession                    - Create new class session
    PUT    /api/ClassSession/{courseId}/{sessionDate} - Update existing class session
    DELETE /api/ClassSession/{courseId}/{sessionDate} - Delete class session
    POST   /api/ClassSession/verify-password    - Verify class session password
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace AttendanceSystem.API.Controllers
{
    /// Controller for managing class sessions in the attendance system.
    /// Provides endpoints for CRUD operations and password verification.
    [ApiController]
    [Route("api/[controller]")]
    public class ClassSessionController : ControllerBase
    {
        private readonly AttendanceDbContext _context;

        /// Constructor that injects the database context for data access.
        /// <param name="context">The database context for attendance system data.</param>
        public ClassSessionController(AttendanceDbContext context)
        {
            _context = context;
        }

        /// GET /api/ClassSession
        /// Retrieves all class sessions from the database.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassSession>>> GetClassSessions()
        {
            return await _context.ClassSessions.ToListAsync();
        }

        /// GET /api/ClassSession/{courseId}/{sessionDate}
        /// Retrieves a specific class session by its composite key (course ID and session date).
        [HttpGet("{courseId}/{sessionDate}")]
        public async Task<ActionResult<ClassSession>> GetClassSession(string courseId, DateTime sessionDate)
        {
            var classSession = await _context.ClassSessions
                .FirstOrDefaultAsync(cs => cs.Course_Id == courseId && cs.Session_Date == sessionDate);

            if (classSession == null)
            {
                return NotFound();
            }

            return classSession;
        }

        /// GET /api/ClassSession/{courseId}/{sessionDate}/detailed
        /// Retrieves a detailed view of a class session including related course and quiz information.
        [HttpGet("{courseId}/{sessionDate}/detailed")]
        public async Task<ActionResult<ClassSession>> GetClassSessionDetailed(string courseId, DateTime sessionDate)
        {
            var classSession = await _context.ClassSessions
                .Include(cs => cs.Course)
                .Include(cs => cs.Quiz)
                .Include(cs => cs.AttendanceRecords)
                    .ThenInclude(a => a.Student)
                .Include(cs => cs.Submissions)
                    .ThenInclude(s => s.Student)
                .FirstOrDefaultAsync(cs => cs.Course_Id == courseId && cs.Session_Date == sessionDate);

            if (classSession == null)
            {
                return NotFound();
            }

            return classSession;
        }

        // David Sajdak 4/21/2025
        // GET /api/ClassSession/WithConfigData
        // gets class session info along with other data needed for configuration table
        [HttpGet("WithConfigData")]
        public async Task<IActionResult> GetClassSessionsWithConfigData()
        {
            var classSession = await _context.ClassSessions
                .Include(cs => cs.Course)   // join with Course Table
                .Include(cs => cs.Quiz)     // Join with Quiz table
                .Select(cs => new ClassSessionWithConfigDataDto
                {
                    Course_Id = cs.Course_Id,
                    Course_Name = cs.Course.Course_Name,
                    Session_Date = cs.Session_Date,
                    Start_Time = cs.Course.Start_Time,
                    End_Time = cs.Course.End_Time,
                    Password = cs.Password,
                    Due_Date = cs.Quiz.Due_Date,
                    Pool_Id = cs.Quiz.Pool_Id
                })
                .ToListAsync();

            return Ok(classSession);
        }

        /// POST /api/ClassSession
        /// Creates a new class session.
        /// Validates course and quiz existence, and checks for duplicate sessions.
        [HttpPost]
        public async Task<ActionResult<ClassSession>> CreateClassSession(ClassSessionCreateDto classSessionDto)
        {
            // Validate that the course exists
            var courseExists = await _context.Courses.AnyAsync(c => c.Course_Id == classSessionDto.Course_Id);
            if (!courseExists)
            {
                return BadRequest("Invalid CourseId");
            }

            // Validate that the quiz exists
            var quizExists = await _context.Quizzes.AnyAsync(q => q.Quiz_Id == classSessionDto.Quiz_Id);
            if (!quizExists)
            {
                return BadRequest("Invalid QuizId");
            }

            // Check for duplicate sessions
            var sessionExists = await _context.ClassSessions
                .AnyAsync(cs => cs.Course_Id == classSessionDto.Course_Id && cs.Session_Date == classSessionDto.SessionDate);
            if (sessionExists)
            {
                return BadRequest("A session already exists for this course on this date");
            }

            // Create new class session
            var classSession = new ClassSession
            {
                Session_Date = classSessionDto.SessionDate,
                Course_Id = classSessionDto.Course_Id,
                Password = classSessionDto.Password,
                Quiz_Id = classSessionDto.Quiz_Id
            };

            _context.ClassSessions.Add(classSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassSession), 
                new { courseId = classSession.Course_Id, sessionDate = classSession.Session_Date }, 
                classSession);
        }

        /// POST /api/ClassSession/verify-password
        /// Verifies if a provided password matches the class session password.
        [HttpPost("verify-password")]
        public async Task<ActionResult<bool>> VerifyPassword(PasswordVerificationDto verificationDto)
        {
            var classSession = await _context.ClassSessions
                .FirstOrDefaultAsync(cs => cs.Course_Id == verificationDto.Course_Id && 
                                         cs.Session_Date == verificationDto.Session_Date);

            if (classSession == null)
            {
                return NotFound("Class session not found");
            }

            return Ok(classSession.Password == verificationDto.Password);
        }

        /// PUT /api/ClassSession/{courseId}/{sessionDate}
        /// Updates an existing class session.
        /// Validates quiz existence and handles concurrency conflicts.
        [HttpPut("{courseId}/{sessionDate}")]
        public async Task<IActionResult> UpdateClassSession(string courseId, DateTime sessionDate, ClassSessionUpdateDto classSessionDto)
        {
            var classSession = await _context.ClassSessions
                .FirstOrDefaultAsync(cs => cs.Course_Id == courseId && cs.Session_Date == sessionDate);

            if (classSession == null)
            {
                return NotFound();
            }

            // Validate that the quiz exists
            var quizExists = await _context.Quizzes.AnyAsync(q => q.Quiz_Id == classSessionDto.Quiz_Id);
            if (!quizExists)
            {
                return BadRequest("Invalid QuizId");
            }

            // Update class session properties
            classSession.Password = classSessionDto.Password;
            classSession.Quiz_Id = classSessionDto.Quiz_Id; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSessionExists(courseId, sessionDate))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// DELETE /api/ClassSession/{courseId}/{sessionDate}
        /// Deletes a class session.
        [HttpDelete("{courseId}/{sessionDate}")]
        public async Task<IActionResult> DeleteClassSession(string courseId, DateTime sessionDate)
        {
            var classSession = await _context.ClassSessions
                .FirstOrDefaultAsync(cs => cs.Course_Id == courseId && cs.Session_Date == sessionDate);

            if (classSession == null)
            {
                return NotFound();
            }

            _context.ClassSessions.Remove(classSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// Checks if a class session exists in the database.
        private bool ClassSessionExists(string courseId, DateTime sessionDate)
        {
            return _context.ClassSessions.Any(e => e.Course_Id == courseId && e.Session_Date == sessionDate);
        }

        /// Generates a random password for class sessions.
        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
