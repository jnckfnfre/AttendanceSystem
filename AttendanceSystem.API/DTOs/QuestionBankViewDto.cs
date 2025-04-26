/*
    Maha Shaikh 4/23/2025
    DTO for viewing question banks with associated pool information
    This DTO includes relevant display fields such as question text,
    associated pool name, and creation date for UI rendering
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

        // When the question was created (for sorting/filtering purposes)
        public DateTime CreatedAt { get; set; }
    }
}
