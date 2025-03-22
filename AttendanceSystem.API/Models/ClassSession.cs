using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the class_session table
[Table("class_session")]
public class ClassSession
{
    [Column("SESSION_DATE")]
    public DateTime SessionDate { get; set; }

    [Column("COURSE_ID")]
    public string CourseId { get; set; }

    [Column("PASSWORD")]
    public string Password { get; set; }

    [Column("QUIZ_ID")]
    public int QuizId { get; set; }

    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; }

    public ICollection<Submission> Submissions { get; set; }
    public ICollection<AttendedBy> AttendanceRecords { get; set; }
}
