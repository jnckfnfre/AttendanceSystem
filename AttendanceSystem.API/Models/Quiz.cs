using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// Nahyan Munawar 3/21/2025
// Model which maps to the Quiz table
namespace AttendanceSystem.API.Models
{
  [Table("Quiz")]
  public class Quiz{
    [Key]
    [Column("QUIZ_ID")]
    public int Quiz_Id { get; set; }  // Primary key for each quiz

    [Column("DUE_DATE")]
    public DateTime? Due_Date { get; set; } // Optional due date for the quiz

    [Column("POOL_ID")]
    public int Pool_Id { get; set; } // Foreign key linking to Question_Pool

    [JsonIgnore]
    [ForeignKey("Pool_Id")]
    public QuestionPool QuestionPool { get; set; }

    [JsonIgnore]
    public ICollection<Question> Questions { get; set; }

    [JsonIgnore]
    public ICollection<ClassSession> Sessions { get; set; }

    [JsonIgnore]
    public ICollection<Submission> Submissions { get; set; }
  }  
}

