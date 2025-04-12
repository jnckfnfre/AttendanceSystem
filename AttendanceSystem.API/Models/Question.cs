using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the Questions table
namespace AttendanceSystem.API.Models{ //Hamza Khawaja 4/11/2025 - Fixed some of the namespace issues. 
    public class Question{
    [Key]
    [Column("QUESTIONS_ID")]
    
    public int QuestionId { get; set; }

    [Column("TEXT")]
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

    [Column("QUIZ_ID")]
    public int QuizId { get; set; }

    [Column("POOL_ID")]
    public int PoolId { get; set; }

    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; }

    [ForeignKey("PoolId")]
    public QuestionPool QuestionPool { get; set; }  
    }
}


