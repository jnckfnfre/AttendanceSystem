// Created by Nahyan Munawar 4/2/2025
// DTO for creating attendance records

using System;

namespace AttendanceSystem.API.DTOs
{
    public class AttendanceCreateDto
    {
        public string Utd_Id { get; set; }
        public string Course_Id { get; set; }
        public DateTime Session_Date { get; set; }
    }
} 