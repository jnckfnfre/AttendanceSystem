// Created by Nahyan Munawar 2025-04-02T00:00:00
// Model which maps to the Submissions table

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AttendanceSystem.API.Models;

[Table("Submissions")]
public class Submissions
{
    [Key]
    [Column("SUBMISSION_ID")]
    public int SubmissionId { get; set; }

    [Column("COURSE_ID")]
    public string Course_Id { get; set; }

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
    public char? Answer1 { get; set; }

    [Column("ANSWER2")]
    public char? Answer2 { get; set; }

    [Column("ANSWER3")]
    public char? Answer3 { get; set; }

    [Column("STATUS")]
    public string Status { get; set; }

    [JsonIgnore]
    [ForeignKey("UtdId")]
    public Student Student { get; set; }

    [JsonIgnore]
    [ForeignKey("Course_Id, SessionDate")]
    public ClassSession ClassSession { get; set; }

    [JsonIgnore]
    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; }
}

// Add this to your DbContext's OnModelCreating method:
// modelBuilder.Entity<Submissions>()
//     .HasIndex(s => new { s.Course_Id, s.SessionDate, s.UtdId, s.QuizId })
//     .IsUnique(); 