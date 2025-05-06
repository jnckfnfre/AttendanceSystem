/*
    David Sajdak Started: 3/26/2025
    Controller for the Course model
    Get, put, post, delete methods for the Course model
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;
using AttendanceSystem.API.DTOs;

[ApiController]
[Route("api/[controller]")]

public class CoursesController : ControllerBase {
    // database context for accessing the database
    // handles all database operations for courses
    private readonly AttendanceDbContext _context;

    // constructor for the CoursesController
    // injects the database context into the controller
    public CoursesController(AttendanceDbContext context) {
        _context = context;
    }

    // GET: api/Courses
    // Gets all courses
    [HttpGet]
    public async Task<IActionResult> GetCourses() {
        var courses = await _context.Courses.ToListAsync();
        return Ok(courses);
    }

    // GET: api/Courses/{id}
    // Gets a course by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(string id) {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) {
            return NotFound();
        }
        
        return Ok(course);
    }

    // POST: api/Courses
    // Adds a course
    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] CourseCreateDto courseDto) {

        // convert DTO to Course object
        var course = new Course {
            Course_Id = courseDto.Course_Id,
            Course_Name = courseDto.Course_Name,
            Start_Time = courseDto.Start_Time,
            End_Time = courseDto.End_Time
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCourses), new { id = course.Course_Id }, course);
    }

    // POST: api/Courses/batch-upload
    // batch upload courses
    [HttpPost("batch-upload")]
    public async Task<IActionResult> BatchUploadCourses([FromBody] List<CourseCreateDto> courseDtos)
    {
        if (courseDtos == null || courseDtos.Count == 0)
        {
            return BadRequest("No courses provided for upload.");
        }

        var courses = courseDtos.Select(dto => new Course
        {
            Course_Id = dto.Course_Id,
            Course_Name = dto.Course_Name,
            Start_Time = dto.Start_Time,
            End_Time = dto.End_Time
        }).ToList();

        await _context.Courses.AddRangeAsync(courses);
        await _context.SaveChangesAsync();

        return Ok(new { inserted = courses.Count });
    }

    // PUT: api/Courses/{id}
    // Updates a course
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(string id, [FromBody] CourseUpdateDto courseUpdateDto) {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) {
            return NotFound();
        }

        // update non primary key fields in DTO
        course.Course_Name = courseUpdateDto.Course_Name;
        course.Start_Time = courseUpdateDto.Start_Time;
        course.End_Time = courseUpdateDto.End_Time;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Courses/{id}
    // Deletes a course
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(string id) {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) {
            return NotFound();
        }
        
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}