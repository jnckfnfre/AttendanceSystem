// Created by Maha Shaikh 4/1/2025
// Edited by Nahyan Munawar 4/2/2025
// Controller for managing attendance records

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;

namespace AttendanceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceDbContext _context;

        public AttendanceController(AttendanceDbContext context)
        {
            _context = context;
        }

        // Created by Nahyan Munawar 4/2/2025
        // Get all attendance records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendedBy>>> GetAllAttendance()
        {
            var attendance = await _context.AttendedBy.ToListAsync();
            return Ok(attendance);
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

        // Created by Nahyan Munawar 4/2/2025
        // Create a new attendance record
        [HttpPost]
        public async Task<ActionResult<AttendedBy>> CreateAttendance(AttendanceCreateDto attendanceDto)
        {
            var attendance = new AttendedBy
            {
                UtdId = attendanceDto.UtdId,
                Course_Id = attendanceDto.Course_Id,
                SessionDate = attendanceDto.SessionDate
            };

            _context.AttendedBy.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllAttendance), new { id = attendance.AttendanceId }, attendance);
        }

        // Created by Nahyan Munawar 4/2/2025
        // Delete an attendance record
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _context.AttendedBy.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.AttendedBy.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Created by Nahyan Munawar 4/2/2025
        // Update an attendance record
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, AttendanceCreateDto attendanceDto)
        {
            var attendance = await _context.AttendedBy.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            attendance.UtdId = attendanceDto.UtdId;
            attendance.Course_Id = attendanceDto.Course_Id;
            attendance.SessionDate = attendanceDto.SessionDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AttendanceExists(int id)
        {
            return _context.AttendedBy.Any(e => e.AttendanceId == id);
        }
    }
}
