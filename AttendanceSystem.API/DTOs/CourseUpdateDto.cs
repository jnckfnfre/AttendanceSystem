/*
    David Sajdak Started 4/8/2025
    This is the DTO (data transfer object) for updating a course.
    Validates and transfers data between the frontend and backend.
    Acts as extra layer of security between the frontend and backend.
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs {
    public class CourseUpdateDto {
        // skip course id as it is not allowed to be updated

        // Name of the course to be updated, can't be null
        [Required(ErrorMessage = "Course Name is required")]
        public string Course_Name { get; set; }

        // Start time of the course to be updated, can't be null
        [Required(ErrorMessage = "Start Time is required")]
        public TimeSpan Start_Time { get; set; }

        // End time of the course to be updated, can't be null
        [Required(ErrorMessage = "End Time is required")]
        public TimeSpan End_Time { get; set; }
    }
}
