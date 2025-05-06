/*
    David Sajdak 4/21/2025
    Used for configuration table in desktop app
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs
{
    public class ClassSessionWithConfigDataDto
    {
        /// The ID of the course for which the class session is being created.
        [Required(ErrorMessage = "CourseId is required")]
        public string Course_Id { get; set; }

        // couse name for class session
        [Required(ErrorMessage = "Course Name is required")]
        public string Course_Name { get; set; }

        /// The date when the class session will take place.
        [Required(ErrorMessage = "SessionDate is required")]
        public DateTime Session_Date { get; set; }

        // start time for session
        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan Start_Time { get; set; }

        // end time for session
        [Required(ErrorMessage = "End time is required")]
        public TimeSpan End_Time { get; set; }

        /// The password required for students to access the class session.
        [Required(ErrorMessage = "Password is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be exactly 6 characters long")]
        public string Password { get; set; }

        // due date for session quiz
        public DateTime? Due_Date { get; set; }

        // question pool id for quiz
         [Required(ErrorMessage = "Pool Id is required")]
        public int Pool_Id { get; set; }
    }
} 