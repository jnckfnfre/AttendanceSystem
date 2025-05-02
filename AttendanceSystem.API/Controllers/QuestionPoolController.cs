/*
    Nahyan Munawar 4/2/2025
    Controller for the QuestionPool model
    Handles CRUD operations for question pools
    Question pools are collections of questions that can be used across multiple quizzes
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;

namespace AttendanceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionPoolController : ControllerBase
    {
        // Database context for accessing the database
        // Handles all database operations for question pools
        private readonly AttendanceDbContext _context;

        // Constructor for the QuestionPoolController
        // Injects the database context into the controller
        public QuestionPoolController(AttendanceDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionPool
        // Gets all question pools
        [HttpGet]
        public async Task<IActionResult> GetQuestionPools()
        {
            var pools = await _context.QuestionPools
                .Include(p => p.Questions)
                .ToListAsync();
            return Ok(pools);
        }

        // GET: api/QuestionPool/5
        // Gets a question pool by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionPool(int id)
        {
            var pool = await _context.QuestionPools
                .Include(p => p.Questions)
                .FirstOrDefaultAsync(p => p.PoolId == id);

            if (pool == null)
                return NotFound();

            return Ok(pool);
        }

        // POST: api/QuestionPool
        // Creates a new question pool
        [HttpPost]
        public async Task<IActionResult> CreateQuestionPool([FromBody] QuestionPoolCreateDto dto)
        {
            var pool = new QuestionPool
            {
                PoolName = dto.PoolName,
                CourseId = dto.CourseId,
                Questions = new List<Question>(),
                Quizzes = new List<Quiz>()
            };

            _context.QuestionPools.Add(pool);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestionPool), new { id = pool.PoolId }, pool);
        }

        // PUT: api/QuestionPool/5
        // Updates a question pool by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestionPool(int id, [FromBody] QuestionPoolCreateDto dto)
        {
            var pool = await _context.QuestionPools.FindAsync(id);
            if (pool == null)
                return NotFound();

            pool.PoolName = dto.PoolName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionPoolExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/QuestionPool/5
        // Deletes a question pool by id
        // Note: This will cascade delete all questions in the pool due to the foreign key constraint
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionPool(int id)
        {
            var pool = await _context.QuestionPools.FindAsync(id);
            if (pool == null)
                return NotFound();

            _context.QuestionPools.Remove(pool);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionPoolExists(int id)
        {
            return _context.QuestionPools.Any(e => e.PoolId == id);
        }
    }
} 