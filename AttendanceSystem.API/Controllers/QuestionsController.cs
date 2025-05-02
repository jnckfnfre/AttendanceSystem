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

        /* 
            David Sajdak 5/2/2025
            Batch insert questions, created to help with creating new QB
            functionality in desktop application
        */
        // POST: api/Questions/batch-upload
        [HttpPost("batch-upload")]
        public async Task<IActionResult> BatchUploadQuestions([FromBody] List<QuestionsCreateDto> questionDtos)
        {
            if (questionDtos == null || questionDtos.Count == 0)
            {
                return BadRequest("No questions provided for upload.");
            }

            var questions = questionDtos.Select(dto => new Question
            {
                Text = dto.Text,
                OptionA = dto.OptionA,
                OptionB = dto.OptionB,
                OptionC = dto.OptionC,
                OptionD = dto.OptionD,
                CorrectAnswer = dto.CorrectAnswer,
                QuizId = dto.QuizId,
                PoolId = dto.PoolId
            }).ToList();

            await _context.Questions.AddRangeAsync(questions);
            await _context.SaveChangesAsync();

            return Ok(new { inserted = questions.Count });
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

        //Maha Shaikh 4/24/2025
        // [HttpGet("view-question-banks")]
        // public ActionResult<IEnumerable<QuestionBankViewDto>> GetQuestionBankView()
        // {
        //     var questionData = _context.Questions
        //         .Include(q => q.QuestionPool)
        //         .Include(q => q.Course)
        //         .Select(q => new QuestionBankViewDto
        //         {
        //             QuestionId = q.QuestionId,
        //             Text = q.Text, 
        //             PoolName = q.QuestionPool.PoolName, 
        //             Course_Id = q.Course.Course_Id,     
        //         })
        //         .ToList();

        //     return Ok(questionData);
        // }

        [HttpGet("view-question-banks")]
        public ActionResult<IEnumerable<QuestionBankViewDto>> GetQuestionBankView()
        {
            var questionData = _context.Questions
                .Include(q => q.QuestionPool)
                    .ThenInclude(qp => qp.Course) // Include Course through QuestionPool
                .Select(q => new QuestionBankViewDto
                {
                    QuestionId = q.QuestionId,
                    Text = q.Text,
                    PoolName = q.QuestionPool.PoolName,
                    Course_Id = q.QuestionPool.Course.Course_Id // Access Course via QuestionPool
                })
                .ToList();

            return Ok(questionData);
        }

        // Eduardo Zamora 4/30/2025
        // This endpoint retrieves questions from all courses' question pools and their associated quizzes.
        [HttpGet("GetCoursePoolQuestions")]
        public async Task<IActionResult> GetCoursePoolQuestions()
        {
            var data = await (from course in _context.Courses
                            join pool in _context.QuestionPools on course.Course_Id equals pool.Course_Id
                            join question in _context.Questions on pool.PoolId equals question.PoolId
                            join quiz in _context.Quizzes on pool.PoolId equals quiz.PoolId
                            select new CourseQuestionPoolQuestionDto
                            {
                                Course_Id = course.Course_Id,
                                Course_Name = course.Course_Name,
                                Start_Time = course.Start_Time,
                                End_Time = course.End_Time,
                                PoolId = pool.PoolId,
                                PoolName = pool.PoolName,
                                QuestionId = question.QuestionId,
                                Text = question.Text,
                                OptionA = question.OptionA,
                                OptionB = question.OptionB,
                                OptionC = question.OptionC,
                                OptionD = question.OptionD,
                                CorrectAnswer = question.CorrectAnswer,
                                QuizId = quiz.QuizId
                            }).ToListAsync();

            return Ok(data);
        }

        // Eduardo Zamora 4/30/2025
        // This endpoint retrieves questions from a specific course's question pool that are not associated with any quiz.
        // It returns a list of questions along with their course and pool information.
        [HttpGet("GetCoursePoolQuestionsByCourseId")]
        public async Task<IActionResult> GetCoursePoolQuestionsByCourseId([FromQuery] string courseId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                return BadRequest("Missing courseId");

            var data = await (from course in _context.Courses
                            join pool in _context.QuestionPools on course.Course_Id equals pool.Course_Id
                            join question in _context.Questions on pool.PoolId equals question.PoolId
                            where course.Course_Id == courseId && question.QuizId == null // Only get questions without a quizId
                            select new CourseQuestionPoolQuestionDto
                            {
                                Course_Id = course.Course_Id,
                                Course_Name = course.Course_Name,
                                Start_Time = course.Start_Time,
                                End_Time = course.End_Time,
                                PoolId = pool.PoolId,
                                PoolName = pool.PoolName,
                                QuestionId = question.QuestionId,
                                Text = question.Text,
                                OptionA = question.OptionA,
                                OptionB = question.OptionB,
                                OptionC = question.OptionC,
                                OptionD = question.OptionD,
                                CorrectAnswer = question.CorrectAnswer,
                                QuizId = question.QuizId
                            }).ToListAsync();

            return Ok(data);
        }

        // Eduardo Zamora 4/30/2025
        // This endpoint retrieves questions from a specific course's question pool that are associated with a quiz.
        // It returns a list of questions along with their course and pool information.
        [HttpPost("AssignQuizToQuestions")]
        public IActionResult AssignQuizToQuestions([FromBody] List<QuestionsUpdateDto> updatedQuestions)
        {
            foreach (var dto in updatedQuestions)
            {
                var question = _context.Questions.FirstOrDefault(q => q.QuestionId == dto.QuestionsId);
                if (question != null)
                {
                    question.QuizId = dto.QuizId;
                }
            }

            _context.SaveChanges();
            return Ok(new { message = "QuizId assigned to selected questions." });
        }




    }
}
