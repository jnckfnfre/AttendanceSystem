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

            //check if utdId is in the database
           var student = await _context.Students
           .FirstOrDefaultAsync(u => u.UtdId == utdId); //basically, we are checking if the utdId is in the database
            if (student == null)
            {
                ViewBag.ErrorMessage = "Invalid UTD ID.";
                return View("Index");
            }   

            // For demo purposes, using a simple password check
            if (password == "csdemo123")
            {
                // store their name for next view
                TempData["StudentName"] = $"{student.LastName}, {student.FirstName}";

                // Redirect to quiz view
                return RedirectToAction("Index");

            }

            ViewBag.ErrorMessage = "Invalid UTD ID or password.";
            return View("Index");
        }
    }
} 