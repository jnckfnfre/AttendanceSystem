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

        /*
        Created by Hamza Khawaja, altered by Nahyan Munawar 4/19/2025
        Purpose: Authenticate the student and redirect to the quiz using the Student ID
        and the password for the class session
        */
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

            // Find active class session with matching password for today, include Course for time check
            var classSession = await _context.ClassSessions
                .Include(cs => cs.Quiz) // Include the quiz relationship
                .Include(cs => cs.Course)
                .FirstOrDefaultAsync(cs => 
                    cs.Password == password && 
                    cs.SessionDate.Date == DateTime.Today);
            
            if (classSession == null)
            {
                ViewBag.ErrorMessage = "Invalid or expired session password.";
                return View("Index");
            }

            // Time check
            var now = DateTime.Now.TimeOfDay;
            var start = classSession.Course.Start_Time;
            var end = classSession.Course.End_Time;
            if (now < start || now > end)
            {
                ViewBag.ErrorMessage = "You can only log in during class time.";
                return View("Index");
            }
            bool isEnrolled = await _context.CourseStudents.
                AnyAsync(cs => cs.Course_Id == classSession.Course_Id && cs.Utd_Id == student.UtdId);

            if (!isEnrolled)
            {
                ViewBag.ErrorMessage = "You are not enrolled in this course.";
                return View("Index");
            }

            // Store UtdId in session
            HttpContext.Session.SetString("UtdId", student.UtdId);    

            // Store info for the quiz view
            TempData["StudentName"] = $"{student.LastName}, {student.FirstName}";

            // Redirect to take the quiz associated with this session
            return RedirectToAction("Take", "Quiz", new { 
                id = classSession.QuizId
            });
        }
    }
} 