using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AttendanceSystem.API.Models;


// Nahyan Munawar 3/21/2025
// Model which maps to the Submissions table
[Table("Submissions")]
public class Submission
{
    [Key]
    [Column("SUBMISSION_ID")]
    public int Submission_Id { get; set; }

    [Column("COURSE_ID")]
    public string Course_Id { get; set; }

    [Column("SESSION_DATE")]
    public DateTime Session_Date { get; set; }

    [Column("UTD_ID")]
    public string Utd_Id { get; set; }

    [Column("QUIZ_ID")]
    public int Quiz_Id { get; set; }

    [Column("IP_ADDRESS")]
    public string Ip_Address { get; set; }

    [Column("SUBMISSION_TIME")]
    public DateTime Submission_Time { get; set; }

    [Column("ANSWER1")]
    public string Answer_1 { get; set; }

    [Column("ANSWER2")]
    public string Answer_2 { get; set; }

    [Column("ANSWER3")]
    public string Answer_3 { get; set; }

    [Column("STATUS")]
    public string Status { get; set; }

    [ForeignKey("UtdId")]
    public Student Student { get; set; }

    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; }

    [ForeignKey("CourseId, SessionDate")]
    public ClassSession ClassSession { get; set; }
}
