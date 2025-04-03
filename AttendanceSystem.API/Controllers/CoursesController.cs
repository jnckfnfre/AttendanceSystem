/*
    David Sajdak Started: 3/26/2025
    Controller for the Course model
    Get, put, post, delete methods for the Course model
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem.API.Data;
using AttendanceSystem.API.Models;

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
    public async Task<IActionResult> AddCourse([FromBody] Course course) {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCourses), new { id = course.Course_Id }, course);
    }

    // PUT: api/Courses/{id}
    // Updates a course
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(string id, [FromBody] Course updated) {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) {
            return NotFound();
        }

        course.Course_Name = updated.Course_Name;
        course.Start_Time = updated.Start_Time;
        course.End_Time = updated.End_Time;

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