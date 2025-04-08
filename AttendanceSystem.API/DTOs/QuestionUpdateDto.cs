/*
    Maha Shaikh 4/4/2025
    DTO for updating existing questions
    This DTO matches the structure of the Questions model and includes the QuestionsId
    Used when modifying existing questions in quizzes or question pools
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class QuestionsUpdateDto
    {
        // The unique identifier of the question to be updated
        [Required]
        public int QuestionsId { get; set; }

        // Option A for the multiple choice question
        [Required]
        public string OptionA { get; set; } = string.Empty;

        // Option B for the multiple choice question
        [Required]
        public string OptionB { get; set; } = string.Empty;

        // Option C for the multiple choice question
        public string? OptionC { get; set; }

        // Option D for the multiple choice question
        public string? OptionD { get; set; }

        // The correct answer for the question (A, B, C, or D)
        [Required]
        public string CorrectAnswer { get; set; } = string.Empty;

        // The ID of the quiz this question belongs to
        [Required]
        public int QuizId { get; set; }

        // The ID of the question pool this question belongs to
        [Required]
        public int PoolId { get; set; }
    }
}
