using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the Course table

// This tells EF Core that this model maps to "Course" table of the database
[Table("Course")]
public class Course
{
    // Primary key for the course, maps to "COURSE_ID" in the database
    [Key]
    [Column("COURSE_ID")]
    public string CourseId { get; set; }

    [Column("COURSE_NAME")]
    public string CourseName { get; set; }  // Name of the course

    [Column("START_TIME")]
    public TimeSpan StartTime { get; set; } // Time the class starts

    [Column("END_TIME")]
    public TimeSpan EndTime { get; set; }   // Time the class ends

    // Navigation properties
    public ICollection<ClassSession> Sessions { get; set; }
    public ICollection<Submission> Submissions { get; set; }
    public ICollection<AttendedBy> AttendanceRecords { get; set; }
}
