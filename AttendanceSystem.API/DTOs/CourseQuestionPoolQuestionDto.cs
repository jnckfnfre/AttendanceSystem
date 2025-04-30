// Eduardo Zamora 4/28/2025
// DTO for Course, QuestionPool, and Question info
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    // DTO that combines Course, QuestionPool, and Question info
    public class CourseQuestionPoolQuestionDto
    {
        // Course info
        public string Course_Id { get; set; }
        public string Course_Name { get; set; }
        public TimeSpan Start_Time { get; set; }
        public TimeSpan End_Time { get; set; }

        // Question Pool info
        public int PoolId { get; set; }
        public string PoolName { get; set; }

        // Question info
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }

        // Quiz info
        public int? QuizId { get; set; } // Nullable to allow for questions not associated with a quiz
    }
}
