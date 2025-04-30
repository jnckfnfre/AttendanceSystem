using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the Questions table
namespace AttendanceSystem.API.Models{ //Hamza Khawaja 4/11/2025 - Fixed some of the namespace issues. 
    public class Question{
    [Key]
    [Column("QUESTIONS_ID")]
    
    public int QuestionId { get; set; }

    //[Required] // Removed because we column name is TEXT and not QUESTION_TEXT in the database.
    //public string QuestionText { get; set; } = string.Empty; //Maha Shaikh 4/24/2025 

    [Column("TEXT")] // Hamza Khawaja 4/14/2025 - Question model was missing Text field
    public string Text {get; set;} // Hamza Khawaja 4/11/2025 - added this because the actual question prompt was missing from the model

    [Column("OPTION_A")]
    public string OptionA { get; set; }

    [Column("OPTION_B")]
    public string OptionB { get; set; }

    [Column("OPTION_C")]
    public string OptionC { get; set; }

    [Column("OPTION_D")]
    public string OptionD { get; set; }

    [Column("CORRECT_ANSWER")]
    public string CorrectAnswer { get; set; }
    //public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Removed becuase we do not have this column in  question table

    [Column("QUIZ_ID")]
    public int? QuizId { get; set; } // Nullable to allow for questions not associated with a quiz

    [Column("POOL_ID")]
    public int PoolId { get; set; }

    [ForeignKey("QuizId")]
    public Quiz? Quiz { get; set; } // Nullable to allow for questions not associated with a quiz

    [ForeignKey("PoolId")]
    public QuestionPool QuestionPool { get; set; }  
    }
}


