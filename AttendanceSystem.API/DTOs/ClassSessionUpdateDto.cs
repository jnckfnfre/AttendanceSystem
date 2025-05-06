/*
    Eduardo Zamora 3/30/2025
    Data Transfer Object (DTO) for updating an existing class session.
    This DTO contains only the fields that can be modified after creation.
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class ClassSessionUpdateDto
    {
        /// The new password for the class session.
        [Required(ErrorMessage = "Password is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be exactly 6 characters long")]
        public string Password { get; set; }

        /// The new quiz ID to be associated with the class session.
        public int QuizId { get; set; }
    }
} 