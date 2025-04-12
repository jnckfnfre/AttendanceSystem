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
    public int QuizId { get; set; }  // Primary key for each quiz

    [Column("TITLE")]
    public string Title {get; set;} // to display title of quiz in view

    [Column("DUE_DATE")]
    public DateTime? DueDate { get; set; } // Optional due date for the quiz

    [Column("POOL_ID")]
    public int PoolId { get; set; } // Foreign key linking to Question_Pool

    [JsonIgnore]
    [ForeignKey("PoolId")]
    public QuestionPool QuestionPool { get; set; }

    [JsonIgnore]
    public ICollection<Question> Questions { get; set; }

    [JsonIgnore]
    public ICollection<ClassSession> Sessions { get; set; }

    [JsonIgnore]
    public ICollection<Submission> Submissions { get; set; }
  }  
}

