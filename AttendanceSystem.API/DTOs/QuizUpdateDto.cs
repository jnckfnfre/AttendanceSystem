/*
    Maha Shaikh 4/4/2025
    DTO for updating existing quizzes
    This DTO includes the QuizId for identifying the quiz and allows updating fields like DueDate and PoolId
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class QuizUpdateDto
    {
        // The unique identifier of the quiz to be updated
        [Required]
        public int QuizId { get; set; }

        // The updated due date for the quiz
        // This is optional and can be null if the quiz has no due date
        public DateTime? DueDate { get; set; }

        // The ID of the question pool associated with this quiz
        // This is required to maintain the quiz-pool relationship
        [Required]
        public int PoolId { get; set; }
    }
}
