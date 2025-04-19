/* 
    Hamza Khawaja 4/10/2025
    Purpose: Controller for the QuizLogin view
    Routes:
    GET    /QuizLogin                     - Render login form view
    POST   /QuizLogin/Authenticate       - Validate UTD ID and password, redirect to quiz if valid
    Notes:
    - Validates student UTD ID against the database
    - Shows personalized welcome message after successful login using TempData
*/

using Microsoft.AspNetCore.Mvc;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.API.Controllers
{
    public class QuizLoginController : Controller
    {
        private readonly AttendanceDbContext _context;

        public QuizLoginController(AttendanceDbContext context)
        {
            _context = context; //Our bridge to the database
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string utdId, string password)
        {
            if (string.IsNullOrEmpty(utdId) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Please enter both UTD ID and password.";
                return View("Index");
            }

            // check if utdId is in the database
            var student = await _context.Students
                .FirstOrDefaultAsync(u => u.UtdId == utdId);
            if (student == null)
            {
                ViewBag.ErrorMessage = "Invalid UTD ID.";
                return View("Index");
            }   

            // Find active class session with matching password for today
            var classSession = await _context.ClassSessions
                .Include(cs => cs.Quiz) // Include the quiz relationship
                .FirstOrDefaultAsync(cs => 
                    cs.Password == password && 
                    cs.SessionDate.Date == DateTime.Today);
            
            if (classSession == null)
            {
                ViewBag.ErrorMessage = "Invalid or expired session password.";
                return View("Index");
            }

            // Store info for the quiz view
            TempData["StudentName"] = $"{student.LastName}, {student.FirstName}";
            ViewData["UtdId"] = student.UtdId;

            // Redirect to take the quiz associated with this session
            return RedirectToAction("Take", "Quiz", new { 
                id = classSession.QuizId
            });
        }
    }
} 