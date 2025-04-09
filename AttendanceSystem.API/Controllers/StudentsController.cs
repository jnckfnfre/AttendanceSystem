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

    // POST: api/Students
    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] StudentCreateDto studentCreateDto)
    {
        // convert DTO to Student object
        var student = new Student {
            UtdId = studentCreateDto.UTDId,
            FirstName = studentCreateDto.FirstName,
            LastName = studentCreateDto.LastName,
            Net_Id = studentCreateDto.Net_Id
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudents), new { id = student.UtdId }, student);
    }

    // PUT: api/Students/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(string id, [FromBody] StudentUpdateDto studentUpdateDto)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();

        student.FirstName = studentUpdateDto.FirstName;
        student.LastName = studentUpdateDto.LastName;
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
