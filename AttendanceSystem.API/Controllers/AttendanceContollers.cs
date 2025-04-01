using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;

namespace AttendanceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceDbContext _context;

        public AttendanceController(AttendanceDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendance/session/{courseId}/{sessionDate}
        [HttpGet("session/{courseId}/{sessionDate}")]
        public async Task<IActionResult> GetAttendanceForSession(string courseId, DateTime sessionDate)
        {
            var attendance = await _context.AttendedBy
                .Include(a => a.Student)
                .Where(a => a.Course_Id == courseId && a.SessionDate == sessionDate)
                .ToListAsync();

            return Ok(attendance);
        }

        // GET: api/Attendance/student/{utdId}
        [HttpGet("student/{utdId}")]
        public async Task<IActionResult> GetAttendanceForStudent(string utdId)
        {
            var attendance = await _context.AttendedBy
                .Include(a => a.ClassSession)
                .Where(a => a.UtdId == utdId)
                .ToListAsync();

            return Ok(attendance);
        }

        // POST: api/Attendance
        [HttpPost]
        public async Task<IActionResult> MarkAttendance([FromBody] AttendedBy record)
        {
            _context.AttendedBy.Add(record);
            await _context.SaveChangesAsync();
            return Ok(record);
        }
    }
}
