using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// Nahyan Munawar 3/21/2025
// Model which maps to the Question_Pool table
namespace AttendanceSystem.API.Models{ //Hamza Khawaja 4/11/2025 - Fixed some of the namespace issues. Wasnt able to reference the Question model in Quiz or quiz pool
    [Table("Question_Pool")]
    public class QuestionPool
    {
        [Key]
        [Column("POOL_ID")]
        public int PoolId { get; set; }  // Primary key for the pool

        [Column("POOL_NAME")]
        public string PoolName { get; set; } // Name of the question pool

        [JsonIgnore]
        public ICollection<Quiz> Quizzes { get; set; }

        [JsonIgnore]
        public ICollection<Question> Questions { get; set; }
    }
}


