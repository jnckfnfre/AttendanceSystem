/*
    David Sajdak Started 4/8/2025
    This is the DTO (data transfer object) for creating a new student.
    Validates and transfers data between the frontend and backend.
    Acts as extra layer of security between the frontend and backend.
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs {
    public class StudentCreateDto {
        // UTD id of the student to be created, can't be null
        [Required(ErrorMessage = "UTD ID is required")]
        public string Utd_Id { get; set; }

        // First name of the student to be created, can't be null
        [Required(ErrorMessage = "First Name is required")]
        public string First_Name { get; set; }

        // Last name of the student to be created, can't be null
        [Required(ErrorMessage = "Last Name is required")]
        public string Last_Name { get; set; }

        // UTD NetID of the student to be created, optional
        public string? Net_Id { get; set; }
    }
}
