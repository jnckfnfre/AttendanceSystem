// Nahyan Munawar
// Revised by David Sajdak 4/9/2025, see belowe comments for further details
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AttendanceDbContext _context;

    public StudentsController(AttendanceDbContext context)
    {
        _context = context;
    }

    // GET: api/Students
    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    // Eduardo Zamora 5/1/2025
    // GET: api/Students/StudentsByCourse
    [HttpGet("by-course")]
    public IActionResult GetStudentsByCourse([FromQuery] string courseId)
    {
        if (string.IsNullOrEmpty(courseId))
            return BadRequest("Missing courseId");

        var students = _context.AttendedBy
            .Where(e => e.Course_Id == courseId)
            .Select(e => new
            {
                UTDId = e.Student.Utd_Id 
            })
            .ToList();

        return Ok(students);
    }

    // Eduardo Zamora 5/1/2025
    // GET: api/Students/students-by-course-session
    [HttpGet("students-by-course-session")]
    public IActionResult GetStudentsByCourseSession([FromQuery] string courseId, [FromQuery] DateTime sessionDate)
    {
        var students = _context.AttendedBy
            .Where(a => a.Course_Id == courseId && a.Session_Date == sessionDate)
            .Select(a => new
            {
                UTDId = a.Utd_Id
            })
            .Distinct()
            .ToList();

        return Ok(students);
    }



    // POST: api/Students
    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] StudentCreateDto studentCreateDto)
    {
        // convert DTO to Student object
        var student = new Student {
            Utd_Id = studentCreateDto.Utd_Id,
            First_Name = studentCreateDto.First_Name,
            Last_Name = studentCreateDto.Last_Name,
            Net_Id = studentCreateDto.Net_Id
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudents), new { id = student.Utd_Id }, student);
    }

    /* 
        David Sajdak Started 4/9/2025
        Batch insert students, created to help with uploading CSV
        functionality in desktop application
    */
    // POST: api/Students/batch-upload
    [HttpPost("batch-upload")]
    public async Task<IActionResult> BatchUploadStudents([FromBody] List<StudentCreateDto> studentDtos)
    {
        if (studentDtos == null || studentDtos.Count == 0)
            return BadRequest("No students provided.");

        // get Utd_Ids from incoming request
        var incomingIds = studentDtos.Select(s => s.Utd_Id).ToList();

        // find which ones already exist
        var existingIds = await _context.Students
            .Where(s => incomingIds.Contains(s.Utd_Id))
            .Select(s => s.Utd_Id)
            .ToListAsync();

        // filter out exitsing students
        var newStudents = studentDtos
            .Where(s => !existingIds.Contains(s.Utd_Id))
            .Select(dto => new Student
            {
                Utd_Id = dto.Utd_Id,
                First_Name = dto.First_Name,
                Last_Name = dto.Last_Name,
                Net_Id = dto.Net_Id
            })
            .ToList();

        await _context.Students.AddRangeAsync(newStudents);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            inserted = newStudents.Count,
            skipped = existingIds.Count
        });
    }

    // PUT: api/Students/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(string id, [FromBody] StudentUpdateDto studentUpdateDto)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        student.First_Name = studentUpdateDto.First_Name;
        student.Last_Name = studentUpdateDto.Last_Name;
        student.Net_Id = studentUpdateDto.Net_Id;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Students/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(string id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
