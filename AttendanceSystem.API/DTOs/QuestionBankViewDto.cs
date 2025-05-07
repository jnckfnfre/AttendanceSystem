/*
    Maha Shaikh 4/23/2025
    DTO for viewing question banks with associated pool information
    This DTO includes relevant display fields such as question text
    and associated pool name for UI rendering
*/

namespace AttendanceSystem.API.DTOs
{
    public class QuestionBankViewDto
    {
        // Unique ID of the question
        public int Question_Id { get; set; }

        // The actual question text
        public string Text { get; set; }

        // Option A for the multiple choice question
        public string Option_A { get; set; } = string.Empty;

        // Option B for the multiple choice question
        public string Option_B { get; set; } = string.Empty;

        // Option C for the multiple choice question
        // This is optional for true/false questions
        public string? Option_C { get; set; }

        // Option D for the multiple choice question
        // This is optional for true/false questions
        public string? Option_D { get; set; }

        // The correct answer for the question (A, B, C, or D)
        public string Correct_Answer { get; set; } = string.Empty;

        // Name of the question pool this question belongs to
        public string Pool_Name { get; set; }

        public string Course_Id { get; set; } // Foreign key to the Course - Eduardo Zamora 4/28/2025   
    }
}
