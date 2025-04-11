// Maha Shaikh 4/1/2025

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;

namespace AttendanceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly AttendanceDbContext _context;

        public QuizController(AttendanceDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.Questions)
                .Include(q => q.QuestionPool)
                .ToListAsync();

            return Ok(quizzes);
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .Include(q => q.QuestionPool)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quiz == null)
                return NotFound();

            return Ok(quiz);
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizCreateDto dto)
        {
            // Validate that the question pool exists
            var poolExists = await _context.QuestionPools.AnyAsync(p => p.PoolId == dto.PoolId);
            if (!poolExists)
                return BadRequest("Invalid Pool ID");

            var quiz = new Quiz
            {
                DueDate = dto.DueDate,
                PoolId = dto.PoolId,
                Questions = new List<Question>(),
                Sessions = new List<ClassSession>(),
                Submissions = new List<Submission>()
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQuizById), new { id = quiz.QuizId }, quiz);
        }

        // PUT: api/Quiz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizCreateDto dto)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound();

            // Validate that the question pool exists
            var poolExists = await _context.QuestionPools.AnyAsync(p => p.PoolId == dto.PoolId);
            if (!poolExists)
                return BadRequest("Invalid Pool ID");

            quiz.DueDate = dto.DueDate;
            quiz.PoolId = dto.PoolId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quizzes.Any(q => q.QuizId == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
                return NotFound();

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
