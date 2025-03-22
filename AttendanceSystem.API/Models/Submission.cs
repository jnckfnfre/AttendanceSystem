using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the Submissions table
[Table("Submissions")]
public class Submission
{
    [Key]
    [Column("SUBMISSION_ID")]
    public int SubmissionId { get; set; }

    [Column("COURSE_ID")]
    public string CourseId { get; set; }

    [Column("SESSION_DATE")]
    public DateTime SessionDate { get; set; }

    [Column("UTD_ID")]
    public string UtdId { get; set; }

    [Column("QUIZ_ID")]
    public int QuizId { get; set; }

    [Column("IP_ADDRESS")]
    public string IpAddress { get; set; }

    [Column("SUBMISSION_TIME")]
    public DateTime SubmissionTime { get; set; }

    [Column("ANSWER1")]
    public string Answer1 { get; set; }

    [Column("ANSWER2")]
    public string Answer2 { get; set; }

    [Column("ANSWER3")]
    public string Answer3 { get; set; }

    [Column("STATUS")]
    public string Status { get; set; }

    [ForeignKey("UtdId")]
    public Student Student { get; set; }

    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; }

    [ForeignKey("CourseId, SessionDate")]
    public ClassSession ClassSession { get; set; }
}
