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
        public int QuestionId { get; set; }

        // The actual question text
        public string Text { get; set; }

        // Name of the question pool this question belongs to
        public string PoolName { get; set; }

        public string Course_Id { get; set; } // Foreign key to the Course - Eduardo Zamora 4/28/2025   
    }
}
