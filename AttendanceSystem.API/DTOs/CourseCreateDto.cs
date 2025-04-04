/*
    David Sajdak Started 4/4/2025
    This is the DTO (data transfer object) for creating a new course.
    Validates and ransfers data between the frontend and backend.
    Acts as extra layer of security between the frontend and backend.
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs {
    public class CourseCreateDto {
        // Course ID of the course to be created, can't be null
        [Required(ErrorMessage = "Course ID is required")]
        public string Course_Id { get; set; }

        // Name of the course to be created, can't be null
        [Required(ErrorMessage = "Course Name is required")]
        public string Course_Name { get; set; }

        // Start time of the course to be created, can't be null
        [Required(ErrorMessage = "Start Time is required")]
        public TimeSpan Start_Time { get; set; }

        // End time of the course to be created, can't be null
        [Required(ErrorMessage = "End Time is required")]
        public TimeSpan End_Time { get; set; }
    }
}