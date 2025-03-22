using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the Question_Pool table
[Table("Question_Pool")]
public class QuestionPool
{
    [Key]
    [Column("POOL_ID")]
    public int PoolId { get; set; }  // Primary key for the pool

    [Column("POOL_NAME")]
    public string PoolName { get; set; } // Name of the question pool

    public ICollection<Quiz> Quizzes { get; set; }
    public ICollection<Question> Questions { get; set; }
}
