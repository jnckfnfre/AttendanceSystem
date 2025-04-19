/*
    Maha Shaikh 4/1/2025
    Nahyan Munawar 4/2/2025
    Controller for the Questions model
    Handles CRUD operations for questions
    Questions are associated with both a quiz and a question pool
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
    public class QuestionsController : ControllerBase
    {
        // Database context for accessing the database
        // Handles all database operations for questions
        private readonly AttendanceDbContext _context;

        // Constructor for the QuestionsController
        // Injects the database context into the controller
        public QuestionsController(AttendanceDbContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        // Gets all questions
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _context.Questions.ToListAsync();
            return Ok(questions);
        }

        // GET: api/Questions/quiz/{quizId}
        // Gets all questions for a specific quiz
        [HttpGet("quiz/{quizId}")]
        public async Task<IActionResult> GetQuestionsByQuizId(int quizId)
        {
            var questions = await _context.Questions
                .Where(q => q.QuizId == quizId)
                .ToListAsync();

            return Ok(questions);
        }

        // GET: api/Questions/{id}
        // Gets a specific question by its ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            
            if (question == null)
                return NotFound();

            return Ok(question);
        }

        // POST: api/Questions
        // Creates a new question
        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionsCreateDto dto)
        {
            // Validate that the quiz exists
            var quizExists = await _context.Quizzes.AnyAsync(q => q.QuizId == dto.QuizId);
            if (!quizExists)
                return BadRequest("Invalid Quiz ID");

            // Validate that the question pool exists
            var poolExists = await _context.QuestionPools.AnyAsync(p => p.PoolId == dto.PoolId);
            if (!poolExists)
                return BadRequest("Invalid Pool ID");

            // Validate that the correct answer matches one of the options
            var correctAnswer = dto.CorrectAnswer.ToUpper();
            if (correctAnswer != "A" && correctAnswer != "B" && 
                correctAnswer != "C" && correctAnswer != "D")
                return BadRequest("Correct answer must be A, B, C, or D");

            // Create new question from DTO
            var question = new Question
            {
                OptionA = dto.OptionA,
                OptionB = dto.OptionB,
                OptionC = dto.OptionC,
                OptionD = dto.OptionD,
                CorrectAnswer = correctAnswer,
                QuizId = dto.QuizId,
                PoolId = dto.PoolId
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, question);
        }

        // PUT: api/Questions/{id}
        // Updates a specific question
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionsCreateDto dto)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return NotFound();

            // Validate that the quiz exists
            var quizExists = await _context.Quizzes.AnyAsync(q => q.QuizId == dto.QuizId);
            if (!quizExists)
                return BadRequest("Invalid Quiz ID");

            // Validate that the question pool exists
            var poolExists = await _context.QuestionPools.AnyAsync(p => p.PoolId == dto.PoolId);
            if (!poolExists)
                return BadRequest("Invalid Pool ID");

            // Validate that the correct answer matches one of the options
            var correctAnswer = dto.CorrectAnswer.ToUpper();
            if (correctAnswer != "A" && correctAnswer != "B" && 
                correctAnswer != "C" && correctAnswer != "D")
                return BadRequest("Correct answer must be A, B, C, or D");

            // Update question from DTO
            question.OptionA = dto.OptionA;
            question.OptionB = dto.OptionB;
            question.OptionC = dto.OptionC;
            question.OptionD = dto.OptionD;
            question.CorrectAnswer = correctAnswer;
            question.QuizId = dto.QuizId;
            question.PoolId = dto.PoolId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Questions/{id}
        // Deletes a specific question
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return NotFound();

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
