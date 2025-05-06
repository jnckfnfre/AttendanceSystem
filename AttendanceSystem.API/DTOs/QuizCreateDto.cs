/*
    Nahyan Munawar 4/2/2025
    DTO for creating new quizzes
    This DTO matches the structure of the Quiz model but excludes navigation properties
    and auto-generated fields like QuizId
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class QuizCreateDto
    {
        // The date when the quiz is due
        // This is optional as some quizzes might not have a due date
        public DateTime? Due_Date { get; set; }

        // The ID of the question pool this quiz belongs to
        // This is required as every quiz must be associated with a question pool
        [Required]
        public int Pool_Id { get; set; }
    }
} 