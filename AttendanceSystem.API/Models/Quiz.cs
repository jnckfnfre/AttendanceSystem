using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the Quiz table
[Table("Quiz")]
public class Quiz
{
    [Key]
    [Column("QUIZ_ID")]
    public int QuizId { get; set; }  // Primary key for each quiz

    [Column("DUE_DATE")]
    public DateTime? DueDate { get; set; } // Optional due date for the quiz

    [Column("POOL_ID")]
    public int PoolId { get; set; } // Foreign key linking to Question_Pool
}
