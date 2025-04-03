/*
    Nahyan Munawar 4/2/2025
    DTO for updating existing submissions
    Only includes fields that should be modifiable after creation
    Foreign keys and timestamps are not updatable
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class SubmissionUpdateDto
    {
        // Answer fields can be modified to correct submission errors
        [Required]
        public string Answer_1 { get; set; } = string.Empty;

        [Required]
        public string Answer_2 { get; set; } = string.Empty;

        [Required]
        public string Answer_3 { get; set; } = string.Empty;

        // Status can be updated (e.g., from "Submitted" to "Graded")
        [Required]
        public string Status { get; set; } = string.Empty;
    }
} 