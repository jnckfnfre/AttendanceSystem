// Eduardo Zamora 4/27/2025
// FUnctionality to open the create quiz questions form
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AttendanceDesktop
{
    public partial class createQuizQuestionsForm : Form
    {
        public createQuizQuestionsForm()
        {
            InitializeComponent();
            LoadCourses(); // Load courses when form initializes
            generatePasswordButton.Click += generatePasswordButton_Click; // Attach event handler
            sessionTextBox.GotFocus += SessionTextBox_GotFocus;
            sessionTextBox.LostFocus += SessionTextBox_LostFocus;
            searchQuestionsButton.Click += SearchQuestionsButton_Click;

            // Set initial placeholder
            sessionTextBox.Text = "Example: 2025-04-15";
            sessionTextBox.ForeColor = System.Drawing.Color.Gray;
        }

        // Event handler for the session text box to clear placeholder text when focused
        // and restore it when focus is lost if the text box is empty
        private void SessionTextBox_GotFocus(object sender, EventArgs e)
        {
            if (sessionTextBox.Text == "Example: 2025-04-15")
            {
                sessionTextBox.Text = "";
                sessionTextBox.ForeColor = System.Drawing.Color.Black;
            }
        }

        // Event handler for the session text box to restore placeholder text when focus is lost
        private void SessionTextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sessionTextBox.Text))
            {
                sessionTextBox.Text = "Example: 2025-04-15";
                sessionTextBox.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // Event handler for the search questions button click event
        // Validates the input fields and shows a message box if all fields are valid
        private void SearchQuestionsButton_Click(object sender, EventArgs e)
        {
            if (courseDropdown.SelectedItem == null || 
                string.IsNullOrWhiteSpace(sessionTextBox.Text) || 
                string.IsNullOrWhiteSpace(passwordTextBox.Text) ||
                sessionTextBox.Text == "Example: 2025-04-15")
            {
                MessageBox.Show("Please enter course, session, and generate a password.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if session matches yyyy-MM-dd format
            string sessionInput = sessionTextBox.Text.Trim(); // holds the session input
            if (!Regex.IsMatch(sessionInput, @"^\d{4}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Session must be in the format YYYY-MM-DD.", "Invalid Session Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"All fields are valid!\n\nSession entered: {sessionInput}", "Validation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        // Method to load courses from the API
        private async void LoadCourses()
        {
            try
            {
                string apiUrl = "http://localhost:5257/api/courses";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);

                    var courses = doc.RootElement.EnumerateArray()
                                    .Select(element => new Course
                                    {
                                        CourseId = element.GetProperty("course_Id").GetString(),
                                        CourseName = element.GetProperty("course_Name").GetString()
                                    })
                                    .ToList();

                    courseDropdown.Invoke((MethodInvoker)delegate {
                        courseDropdown.Items.Clear();
                        foreach (var course in courses)
                        {
                            courseDropdown.Items.Add(course);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching courses: {ex.Message}");
            }
        }
        
        // Event handler for the generate password button click event
        private void generatePasswordButton_Click(object sender, EventArgs e)
        {
            passwordTextBox.Text = GenerateRandomPassword(6);
        }

        // Method to generate a random password of specified length
        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            return new string(password);
        }
    }

    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public override string ToString()
        {
            return $"{CourseId} - {CourseName}";
        }
    }
}