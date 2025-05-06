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
        public int Pool_Id { get; set; }
        public string Pool_Name { get; set; }

        // Question info
        public int Question_Id { get; set; }
        public string Text { get; set; }
        public string Option_A { get; set; }
        public string Option_B { get; set; }
        public string Option_C { get; set; }
        public string Option_D { get; set; }
        public string Correct_Answer { get; set; }

        // Quiz info
        public int? Quiz_Id { get; set; } // Nullable to allow for questions not associated with a quiz
    }
}
