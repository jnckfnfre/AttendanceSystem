using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Nahyan Munawar 3/21/2025
// Model which maps to the Questions table
namespace AttendanceSystem.API.Models{ //Hamza Khawaja 4/11/2025 - Fixed some of the namespace issues. 
    public class Question{
    [Key]
    [Column("QUESTIONS_ID")]
    
    public int Question_Id { get; set; }

    [Column("TEXT")] // Hamza Khawaja 4/14/2025 - Question model was missing Text field
    public string Text {get; set;} // Hamza Khawaja 4/11/2025 - added this because the actual question prompt was missing from the model

    [Column("OPTION_A")]
    public string Option_A { get; set; }

    [Column("OPTION_B")]
    public string Option_B { get; set; }

    [Column("OPTION_C")]
    public string Option_C { get; set; }

    [Column("OPTION_D")]
    public string Option_D { get; set; }

    [Column("CORRECT_ANSWER")]
    public string Correct_Answer { get; set; }

    [Column("QUIZ_ID")]
    public int Quiz_Id { get; set; }

    [Column("POOL_ID")]
    public int Pool_Id { get; set; }

    [ForeignKey("Quiz_Id")]
    public Quiz Quiz { get; set; }

    [ForeignKey("Pool_Id")]
    public QuestionPool QuestionPool { get; set; }  
    }
}


