using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AttendanceSystem.API.Models;

[Table("Course_Students")]
public class CourseStudents
{
    [Key]
    [Column("COURSE_ID")]
    public string Course_Id { get; set; }

    [Key]
    [Column("UTD_ID")]        
    public string Utd_Id { get; set; }

    // Navigation properties
    [ForeignKey(nameof(Course_Id))]
    public Course Course { get; set; }

    [ForeignKey(nameof(Utd_Id))]
    public Student Student { get; set; }

}