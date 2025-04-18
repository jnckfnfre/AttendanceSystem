using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;
/*
        Hamza Khawaja 4/11/2025 
        - Looks up quiz by id
        - includes related Questions 
        - Sends all that to the razor view
        - handle quiz submission
*/

namespace AttendanceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : Controller // Hamza Khawaja *4/13/2025* change inheritance from controllerbase to controller so that we can return a View()
    {
        private readonly AttendanceDbContext _context;

        public QuizController(AttendanceDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes() // This end point(at api/Quiz) returns all quizzes as JSON - useful for admin
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.Questions)
                .Include(q => q.QuestionPool)
                .ToListAsync();

            return Ok(quizzes);
        }
        /*
        Hamza Khawaja 4/17/2025 
        - Uses the student's UTD ID (from session/tempdata or a form)
        - Fetches their Course_Id 
        - Finds a Quiz tied to that course
        - Returns the quiz to the view
        */
        [HttpGet("/Quiz/TakeByCourse/{utdId}")]
        public async Task<IActionResult> TakeByCourse(String utdId){
            // first step is to find the student using the UTD ID
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UtdId == utdId);
            
            if (student == null){
                return NotFound("Student not enrolled in this course.");
            }

            // next we find a quiz that matches the student's Course_Id
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .Include(q => q.QuestionPool)
                .FirstOrDefaultAsync(q => q.Course_Id == student.Course_Id);

            if (quiz == null){
                return NotFound("No Quiz Found! :)");
            }

            // passing stundetns name to view
            TempData["StudentName"] = student.FirstName;
            ViewData["UtdId"] = student.UtdId;


            //finally we can send the quiz to the view
            return View("Take", quiz); // "Take.cshtml" is your quiz view
        }



        

        [HttpGet("/Quiz/Take/{id}")] // this will handle GET requests to /Quiz/Take/{id}, {id} is the quiz ID
        public async Task<IActionResult> Take(int id){ // to retrieve a quiz by its id, including its questions and then render the quiz view
            var quiz = await _context.Quizzes // fetch the quiz, including its related questions and question pool 
                .Include(q => q.Questions) //
                .Include(q => q.QuestionPool)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quiz == null) // if no quiz is found, return a 404, no result found
                return NotFound();
            return View(quiz);
        }
        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id) // This endpoint (at api/Quiz/{id}) returns a single quiz, with related quetion pool and questions as JSON
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
            { // e.g., April 16, 2025
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

    /*
    Hamza Khawaja 4/14/2025
    - Submission method
    - POST action handles quiz submissions
    - It accepts the quiz ID and a list of answers from the form.
    */
    
}
