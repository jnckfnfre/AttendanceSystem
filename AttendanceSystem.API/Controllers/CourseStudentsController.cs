/*
    Eduardo Zamora & David Sajdak 05/05/2025
    Controller for the CourseStudents model
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;

[ApiController]
[Route("api/[controller]")]

public class CourseStudentsController : ControllerBase {
    // database context for accessing the database
    // handles all database operations for courses & students
    private readonly AttendanceDbContext _context;

    // constructor for the CourseStudentsController
    // injects the database context into the controller
    public CourseStudentsController(AttendanceDbContext context) {
        _context = context;
    }

    // GET: api/CourseStudents
    // Gets all courses & students relationships
    [HttpGet]
    public async Task<IActionResult> GetCourseStudents() {
        var courseStudents = await _context.CourseStudents.ToListAsync();
        return Ok(courseStudents);
    }

    // GET: api/CourseStudents/{courseId}/{utd_Id}
    [HttpGet("{courseId}/{Utd_Id}")]
    public async Task<ActionResult<CourseStudents>> GetCourseStudents(string courseId, string Utd_Id)
    {
        var CourseStudents = await _context.CourseStudents
            .FirstOrDefaultAsync(cs => cs.Course_Id == courseId && cs.Utd_Id == Utd_Id);

        if (CourseStudents == null)
        {
            return NotFound();
        }

        return CourseStudents;
    }

    // GET: api/CourseStudents/by-course/{courseId}
    // Gets all students in a course
    [HttpGet("by-course/{courseId}")]
    public async Task<IActionResult> GetStudentsByCourse(string courseId)
    {
        var students = await _context.CourseStudents
            .Where(cs => cs.Course_Id == courseId)
            .Select(cs => new {
                cs.Utd_Id
            })
            .ToListAsync();

        return Ok(students);
    }


    // GET: api/Courses/{id}
    // Gets a course by id
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetCourse(string id) {
    //     var course = await _context.Courses.FindAsync(id);
    //     if (course == null) {
    //         return NotFound();
    //     }
        
    //     return Ok(course);
    // }

    // POST: api/Courses
    // Adds a course
    [HttpPost]
    public async Task<IActionResult> AddCourseStudents([FromBody] CourseStudentsCreateDto courseStudentDto) {
        // convert DTO to Course object
        var courseStudent = new CourseStudents {
            Course_Id = courseStudentDto.Course_Id,
            Utd_Id = courseStudentDto.Utd_Id,
        };

        _context.CourseStudents.Add(courseStudent);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCourseStudents), new { id = courseStudent.Course_Id, Utd_Id = courseStudent.Utd_Id }, courseStudent);
    }

    // POST: api/CourseStudents/batch-upload
    // batch upload students with their associated course
    [HttpPost("batch-upload")]
    public async Task<IActionResult> BatchUploadCourseStudents([FromBody] List<CourseStudentsCreateDto> dtoList)
    {
        if (dtoList == null || dtoList.Count == 0)
        {
            return BadRequest("No student-course links provided.");
        }

        var courseStudents = dtoList.Select(dto => new CourseStudents
        {
            Utd_Id = dto.Utd_Id,
            Course_Id = dto.Course_Id
        }).ToList();

        await _context.CourseStudents.AddRangeAsync(courseStudents);
        await _context.SaveChangesAsync();

        return Ok(new { inserted = courseStudents.Count });
    }

    // PUT: api/Courses/{id}
    // Updates a course
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateCourse(string id, [FromBody] CourseUpdateDto courseUpdateDto) {
    //     var course = await _context.Courses.FindAsync(id);
    //     if (course == null) {
    //         return NotFound();
    //     }

    //     // update non primary key fields in DTO
    //     course.Course_Name = courseUpdateDto.Course_Name;
    //     course.Start_Time = courseUpdateDto.Start_Time;
    //     course.End_Time = courseUpdateDto.End_Time;

    //     await _context.SaveChangesAsync();
    //     return NoContent();
    // }

    // DELETE: api/Courses/{id}
    // Deletes a course
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteCourse(string id) {
    //     var course = await _context.Courses.FindAsync(id);
    //     if (course == null) {
    //         return NotFound();
    //     }
        
    //     _context.Courses.Remove(course);
    //     await _context.SaveChangesAsync();
    //     return NoContent();
    // }
}