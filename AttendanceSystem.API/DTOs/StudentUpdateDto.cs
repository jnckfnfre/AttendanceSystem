/*
    David Sajdak Started 4/8/2025
    This is the DTO (data transfer object) for updating a student.
    Validates and transfers data between the frontend and backend.
    Acts as extra layer of security between the frontend and backend.
*/

using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.API.DTOs {
    public class StudentUpdateDto {
        // UTD id can't be updated, skip

        // First name of the student to be updated, can't be null
        [Required(ErrorMessage = "First Name is required")]
        public string First_Name { get; set; }

        // Last name of the student to be updated, can't be null
        [Required(ErrorMessage = "Last Name is required")]
        public string Last_Name { get; set; }

        // UTD NetID of the student to be updated, optional
        public string? Net_Id { get; set; }
    }
}
