// Eduardo Zamora & David Sajdak 05/05/2025
// Dto to link studets to courses
using System;

namespace AttendanceSystem.API.DTOs
{
    public class CourseStudentsCreateDto
    {
        public string Utd_Id { get; set; }
        public string Course_Id { get; set; }
    }
} 