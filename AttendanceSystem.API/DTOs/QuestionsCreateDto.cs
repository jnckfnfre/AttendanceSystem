/*
    Nahyan Munawar 4/2/2025
    DTO for creating new questions
    This DTO matches the structure of the Questions model but excludes navigation properties
    and auto-generated fields like QuestionsId
    Used when creating new questions for quizzes or question pools
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class QuestionsCreateDto
    {
        // Text for the question
        // required for all questions
        [Required]
        public string Text { get; set; } = string.Empty;
        
        // Option A for the multiple choice question
        // This is required as all questions must have at least two options
        [Required]
        public string OptionA { get; set; } = string.Empty;

        // Option B for the multiple choice question
        // This is required as all questions must have at least two options
        [Required]
        public string OptionB { get; set; } = string.Empty;

        // Option C for the multiple choice question
        // This is optional for true/false questions
        public string? OptionC { get; set; }

        // Option D for the multiple choice question
        // This is optional for true/false questions
        public string? OptionD { get; set; }

        // The correct answer for the question (A, B, C, or D)
        // This is required and must match one of the provided options
        [Required]
        public string CorrectAnswer { get; set; } = string.Empty;

        // The ID of the quiz this question belongs to
        public int? QuizId { get; set; }

        // The ID of the question pool this question belongs to
        // This is required as every question must be associated with a pool
        [Required]
        public int PoolId { get; set; }
    }
} 