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
        {
            return BadRequest("No students provided for upload.");
        }

        var students = studentDtos.Select(dto => new Student
        {
            UtdId = dto.UTDId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Net_Id = dto.Net_Id
        }).ToList();

        await _context.Students.AddRangeAsync(students);
        await _context.SaveChangesAsync();

        return Ok(new { inserted = students.Count });
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
