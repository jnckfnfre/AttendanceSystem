using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AttendanceSystem.API.Models;


// Nahyan Munawar 3/21/2025
// Model which maps to the Attended_By table
[Table("Attended_By")]
public class AttendedBy
{
    [Key]
    [Column("ATTENDANCE_ID")]
    public int AttendanceId { get; set; } // Unique attendance record ID

    [Column("SESSION_DATE")]
    public DateTime SessionDate { get; set; } // Date of the session

    [Column("COURSE_ID")]
    public string CourseId { get; set; } // Course attended

    [Column("UTD_ID")]
    public string UtdId { get; set; } // Student who attended

    [ForeignKey("UtdId")]
    public Student Student { get; set; }

    [ForeignKey("CourseId, SessionDate")]
    public ClassSession ClassSession { get; set; }
}
