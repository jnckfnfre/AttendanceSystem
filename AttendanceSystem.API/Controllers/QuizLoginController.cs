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
        public async Task<IActionResult> Authenticate(string Utd_Id, string Password)
        {
            // Debug: Print received values
            Console.WriteLine($"[DEBUG] Utd_Id: '{Utd_Id}', Password: '{Password}'");

            if (string.IsNullOrEmpty(Utd_Id) || string.IsNullOrEmpty(Password))
            {
                ViewBag.ErrorMessage = "Please enter both UTD ID and password.";
                Console.WriteLine("[DEBUG] Returning Index with error: " + ViewBag.ErrorMessage);
                return View("Index");
            }

            // check if utdId is in the database
            var student = await _context.Students
                .FirstOrDefaultAsync(u => u.Utd_Id == Utd_Id);
            if (student == null)
            {
                ViewBag.ErrorMessage = "Invalid UTD ID.";
                Console.WriteLine("[DEBUG] Returning Index with error: " + ViewBag.ErrorMessage);
                return View("Index");
            }   

            // Find active class session with matching password for today, include Course for time check
            var classSession = await _context.ClassSessions
                .Include(cs => cs.Quiz) // Include the quiz relationship
                .Include(cs => cs.Course)
                .FirstOrDefaultAsync(cs => 
                    cs.Password == Password && 
                    cs.Session_Date.Date == DateTime.Today); // Hamza Khawaja 4/20/2025 - Changed SessionDate to Session_Date
            
            if (classSession == null)
            {
                ViewBag.ErrorMessage = "Invalid or expired session password.";
                Console.WriteLine("[DEBUG] Returning Index with error: " + ViewBag.ErrorMessage);
                return View("Index");
            }

            // Time check
            var now = DateTime.Now.TimeOfDay;
            var start = classSession.Course.Start_Time;
            var end = classSession.Course.End_Time;
            if (now < start || now > end)
            {
                ViewBag.ErrorMessage = "You can only log in during class time.";
                Console.WriteLine("[DEBUG] Returning Index with error: " + ViewBag.ErrorMessage);
                return View("Index");
            }
            bool isEnrolled = await _context.CourseStudents.
                AnyAsync(cs => cs.Course_Id == classSession.Course_Id && cs.Utd_Id == student.Utd_Id);

            if (!isEnrolled)
            {
                ViewBag.ErrorMessage = "You are not enrolled in this course.";
                Console.WriteLine("[DEBUG] Returning Index with error: " + ViewBag.ErrorMessage);
                return View("Index");
            }

            // Store UtdId in session
            HttpContext.Session.SetString("Utd_Id", student.Utd_Id);    

            // Store info for the quiz view
            TempData["StudentName"] = $"{student.Last_Name}, {student.First_Name}";

            // Debug: Successful login
            Console.WriteLine("[DEBUG] Login successful, redirecting to quiz.");

            // Redirect to take the quiz associated with this session
            return RedirectToAction("Take", "Quiz", new { 
                id = classSession.Quiz_Id
            });
        }
    }
} 