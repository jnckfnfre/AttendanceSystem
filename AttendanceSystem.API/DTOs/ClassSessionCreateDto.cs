/*
    Eduardo Zamora 3/30/2025
    Data Transfer Object (DTO) for creating a new class session.
    This DTO contains all the necessary information to create a new class session.
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class ClassSessionCreateDto
    {
        /// The ID of the course for which the class session is being created.
        [Required(ErrorMessage = "CourseId is required")]
        public string Course_Id { get; set; }

        /// The date when the class session will take place.
        [Required(ErrorMessage = "SessionDate is required")]
        public DateTime SessionDate { get; set; }

        /// The password required for students to access the class session.
        [Required(ErrorMessage = "Password is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be exactly 6 characters long")]
        public string Password { get; set; }

        /// The ID of the quiz associated with this class session.
        [Required(ErrorMessage = "QuizId is required")]
        public int Quiz_Id { get; set; }
    }
} 

