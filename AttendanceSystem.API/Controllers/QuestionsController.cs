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
                .Where(q => q.Quiz_Id == quizId)
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
            var quizExists = await _context.Quizzes.AnyAsync(q => q.Quiz_Id == dto.Quiz_Id);
            if (!quizExists)
                return BadRequest("Invalid Quiz ID");

            // Validate that the question pool exists
            var poolExists = await _context.QuestionPools.AnyAsync(p => p.Pool_Id == dto.Pool_Id);
            if (!poolExists)
                return BadRequest("Invalid Pool ID");

            // Validate that the correct answer matches one of the options
            var correctAnswer = dto.Correct_Answer.ToUpper();
            if (correctAnswer != "A" && correctAnswer != "B" && 
                correctAnswer != "C" && correctAnswer != "D")
                return BadRequest("Correct answer must be A, B, C, or D");

            // Create new question from DTO
            var question = new Question
            {
                Text = dto.Text, // Hamza Khawaja 4/28/25 - fixed this action, controller was failing to add text property when new question was created
                Option_A = dto.Option_A,
                Option_B = dto.Option_B,
                Option_C = dto.Option_C,
                Option_D = dto.Option_D,
                Correct_Answer = correctAnswer,
                Quiz_Id = dto.Quiz_Id,
                Pool_Id = dto.Pool_Id
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQuestion), new { id = question.Question_Id }, question);
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
                Option_A = dto.Option_A,
                Option_B = dto.Option_B,
                Option_C = dto.Option_C,
                Option_D = dto.Option_D,
                Correct_Answer = dto.Correct_Answer,
                Quiz_Id = dto.Quiz_Id,
                Pool_Id = dto.Pool_Id
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
            var quizExists = await _context.Quizzes.AnyAsync(q => q.Quiz_Id == dto.Quiz_Id);
            if (!quizExists)
                return BadRequest("Invalid Quiz ID");

            // Validate that the question pool exists
            var poolExists = await _context.QuestionPools.AnyAsync(p => p.Pool_Id == dto.Pool_Id);
            if (!poolExists)
                return BadRequest("Invalid Pool ID");

            // Validate that the correct answer matches one of the options
            var correctAnswer = dto.Correct_Answer.ToUpper();
            if (correctAnswer != "A" && correctAnswer != "B" && 
                correctAnswer != "C" && correctAnswer != "D")
                return BadRequest("Correct answer must be A, B, C, or D");

            // Update question from DTO
            question.Option_A = dto.Option_A;
            question.Option_B = dto.Option_B;
            question.Option_C = dto.Option_C;
            question.Option_D = dto.Option_D;
            question.Correct_Answer = correctAnswer;
            question.Quiz_Id = dto.Quiz_Id;
            question.Pool_Id = dto.Pool_Id;

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
                    Question_Id = q.Question_Id,
                    Text = q.Text,
                    Pool_Name = q.QuestionPool.Pool_Name,
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
                            join question in _context.Questions on pool.Pool_Id equals question.Pool_Id
                            join quiz in _context.Quizzes on pool.Pool_Id equals quiz.Pool_Id
                            select new CourseQuestionPoolQuestionDto
                            {
                                Course_Id = course.Course_Id,
                                Course_Name = course.Course_Name,
                                Start_Time = course.Start_Time,
                                End_Time = course.End_Time,
                                Pool_Id = pool.Pool_Id,  
                                Pool_Name = pool.Pool_Name,
                                Question_Id = question.Question_Id,
                                Text = question.Text,
                                Option_A = question.Option_A,
                                Option_B = question.Option_B,
                                Option_C = question.Option_C,
                                Option_D = question.Option_D,
                                Correct_Answer = question.Correct_Answer,
                                Quiz_Id = quiz.Quiz_Id
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
                            join question in _context.Questions on pool.Pool_Id equals question.Pool_Id
                            where course.Course_Id == courseId && question.Quiz_Id == null // Only get questions without a quizId
                            select new CourseQuestionPoolQuestionDto
                            {
                                Course_Id = course.Course_Id,
                                Course_Name = course.Course_Name,
                                Start_Time = course.Start_Time,
                                End_Time = course.End_Time,
                                Pool_Id = pool.Pool_Id,   
                                Pool_Name = pool.Pool_Name,
                                Question_Id = question.Question_Id,
                                Text = question.Text,
                                Option_A = question.Option_A,
                                Option_B = question.Option_B,
                                Option_C = question.Option_C,
                                Option_D = question.Option_D,
                                Correct_Answer = question.Correct_Answer,
                                Quiz_Id = question.Quiz_Id
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
                var question = _context.Questions.FirstOrDefault(q => q.Question_Id == dto.Questions_Id);
                if (question != null)
                {
                    question.Quiz_Id = dto.Quiz_Id;
                }
            }

            _context.SaveChanges();
            return Ok(new { message = "QuizId assigned to selected questions." });
        }

    }
}
