/*
    Eduardo Zamora 3/30/2025
    Data Transfer Object (DTO) for verifying a class session password.
    This DTO contains the information needed to verify if a student has the correct password
*/
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class PasswordVerificationDto
    {
        /// The ID of the course for which the password is being verified.
        [Required(ErrorMessage = "CourseId is required")]
        public string Course_Id { get; set; }

        /// The date of the class session for which the password is being verified.
        [Required(ErrorMessage = "SessionDate is required")]
        public DateTime Session_Date { get; set; }

   
        /// The password to verify against the class session's stored password.
        [Required(ErrorMessage = "Password is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be exactly 6 characters long")]
        public string Password { get; set; }
    }
} 