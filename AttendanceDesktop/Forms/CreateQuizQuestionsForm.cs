// Eduardo Zamora 4/27/2025
// FUnctionality to open the create quiz questions form
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            createQuizButton.Click += createQuizButton_Click;

            questionsDataGridView.CurrentCellDirtyStateChanged += QuestionsDataGridView_CurrentCellDirtyStateChanged;
            questionsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Select full rows
            questionsDataGridView.MultiSelect = true; // Allow multiple row selection

            // Set initial placeholder
            sessionTextBox.Text = "Example: 2025-04-15";
            sessionTextBox.ForeColor = System.Drawing.Color.Gray;
        }

        private void ResetForm()
        {
            // Reset course selection
            courseDropdown.SelectedItem = null;

            // Reset session date textbox
            sessionTextBox.Text = "Example: 2025-04-15";
            sessionTextBox.ForeColor = System.Drawing.Color.Gray;

            // Reset password textbox
            passwordTextBox.Text = "";

            // Clear DataGridView
            questionsDataGridView.DataSource = null;
            questionsDataGridView.Rows.Clear();
            questionsDataGridView.Columns.Clear();
        }


        // Event handler for the DataGridView to commit edit when checkbox is checked/unchecked
        // This ensures that the checkbox state is updated immediately when clicked
        private void QuestionsDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (questionsDataGridView.IsCurrentCellDirty)
            {
                questionsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
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
        private async void SearchQuestionsButton_Click(object sender, EventArgs e)
        {
            if (courseDropdown.SelectedItem == null || 
                string.IsNullOrWhiteSpace(sessionTextBox.Text) || 
                string.IsNullOrWhiteSpace(passwordTextBox.Text) ||
                sessionTextBox.Text == "Example: 2025-04-15")
            {
                MessageBox.Show("Please enter course, session, and generate a password.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate session input format
            string sessionInput = sessionTextBox.Text.Trim();
            if (!Regex.IsMatch(sessionInput, @"^\d{4}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Session must be in the format YYYY-MM-DD.", "Invalid Session Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedCourse = courseDropdown.SelectedItem as Course;
                if (selectedCourse == null)
                {
                    MessageBox.Show("Please select a valid course.", "Invalid Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string apiUrl = $"http://localhost:5257/api/questions/GetCoursePoolQuestionsByCourseId?courseId={selectedCourse.CourseId}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var questions = JsonSerializer.Deserialize<List<CourseQuestionPoolQuestion>>(json);

                    // Remove duplicates based on QuestionId
                    // This is done to ensure that we only show unique questions in the DataGridView
                    questions = questions
                        .GroupBy(q => q.QuestionId)
                        .Select(g => g.First())
                        .ToList();

                    questionsDataGridView.Invoke((MethodInvoker)delegate
                    {
                        questionsDataGridView.DataSource = questions;

                        // Add checkbox column
                        if (!questionsDataGridView.Columns.Contains("Select"))
                        {
                            DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn();
                            selectColumn.Name = "Select";
                            selectColumn.HeaderText = "Select";
                            selectColumn.Width = 50;
                            questionsDataGridView.Columns.Insert(0, selectColumn); // Insert as first column
                        }

                        questionsDataGridView.ReadOnly = false; // Allow editing
                        questionsDataGridView.Columns["Select"].ReadOnly = false; // Allow only "Select" column to be editable

                        // Make all other columns readonly
                        foreach (DataGridViewColumn column in questionsDataGridView.Columns)
                        {
                            if (column.Name != "Select")
                            {
                                column.ReadOnly = true;
                            }
                        }


                        // Hide course-related columns
                        questionsDataGridView.Columns["CourseId"].Visible = false;
                        questionsDataGridView.Columns["CourseName"].Visible = false;
                        questionsDataGridView.Columns["StartTime"].Visible = false;
                        questionsDataGridView.Columns["EndTime"].Visible = false;
                        if (questionsDataGridView.Columns.Contains("QuizId"))
                        {
                            questionsDataGridView.Columns["QuizId"].Visible = false;
                        }


                        // Rename headers to cleaner names
                        questionsDataGridView.Columns["Text"].HeaderText = "Question Text";
                        questionsDataGridView.Columns["OptionA"].HeaderText = "Choice A";
                        questionsDataGridView.Columns["OptionB"].HeaderText = "Choice B";
                        questionsDataGridView.Columns["OptionC"].HeaderText = "Choice C";
                        questionsDataGridView.Columns["OptionD"].HeaderText = "Choice D";
                        questionsDataGridView.Columns["CorrectAnswer"].HeaderText = "Answer";

                        // ✨ Auto-resize columns to fit text
                        questionsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching questions: {ex.Message}");
            }
        }

        // Event handler for the create quiz button click event 
        private async void createQuizButton_Click(object sender, EventArgs e)
        {
            List<CourseQuestionPoolQuestion> selectedQuestions = new List<CourseQuestionPoolQuestion>();

            foreach (DataGridViewRow row in questionsDataGridView.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value);

                if (isChecked)
                {
                    var question = new CourseQuestionPoolQuestion
                    {
                        PoolId = (int)row.Cells["PoolId"].Value,
                        QuestionId = (int)row.Cells["QuestionId"].Value,
                        Text = row.Cells["Text"].Value?.ToString(),
                        OptionA = row.Cells["OptionA"].Value?.ToString(),
                        OptionB = row.Cells["OptionB"].Value?.ToString(),
                        OptionC = row.Cells["OptionC"].Value?.ToString(),
                        OptionD = row.Cells["OptionD"].Value?.ToString(),
                        CorrectAnswer = row.Cells["CorrectAnswer"].Value?.ToString(),
                        QuizId = row.Cells["QuizId"].Value == DBNull.Value ? null : (int?)row.Cells["QuizId"].Value
                    };
                    selectedQuestions.Add(question);
                }
            }

            if (selectedQuestions.Count == 0)
            {
                MessageBox.Show("Please select at least one question.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedQuestions.Count > 3)
            {
                MessageBox.Show("You can select up to 3 questions only.", "Selection Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int poolId = selectedQuestions.First().PoolId;

                // Get session date
                string sessionDateInput = sessionTextBox.Text.Trim();
                if (!DateTime.TryParse(sessionDateInput, out DateTime dueDate))
                {
                    MessageBox.Show("Invalid session date format. Please use YYYY-MM-DD.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var quizDto = new
                {
                    DueDate = dueDate,
                    PoolId = poolId
                };

                string quizApiUrl = "http://localhost:5257/api/quiz";

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonSerializer.Serialize(quizDto), System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage quizResponse = await client.PostAsync(quizApiUrl, content);
                    if (quizResponse.IsSuccessStatusCode)
                    {
                        var quizJson = await quizResponse.Content.ReadAsStringAsync();
                        int createdQuizId;

                        // Parse manually to find the "quizId"
                        using (JsonDocument doc = JsonDocument.Parse(quizJson))
                        {
                            JsonElement root = doc.RootElement;
                            createdQuizId = root.GetProperty("quizId").GetInt32();

                            // Now proceed to create ClassSession using createdQuizId...
                        }

                        // Assign the created QuizId to each selected question
                        foreach (var question in selectedQuestions)
                        {
                            question.QuizId = createdQuizId;
                        }

                        // Convert selectedQuestions to DTO list
                        var updateDtos = selectedQuestions.Select(q => new
                        {
                            QuestionsId = q.QuestionId,
                            Text = q.Text,
                            OptionA = q.OptionA,
                            OptionB = q.OptionB,
                            OptionC = q.OptionC,
                            OptionD = q.OptionD,
                            CorrectAnswer = q.CorrectAnswer,
                            QuizId = q.QuizId,
                            PoolId = q.PoolId
                        }).ToList();

                        string assignUrl = "http://localhost:5257/api/questions/AssignQuizToQuestions";
                        var assignContent = new StringContent(JsonSerializer.Serialize(updateDtos), System.Text.Encoding.UTF8, "application/json");

                        HttpResponseMessage assignResponse = await client.PostAsync(assignUrl, assignContent);
                        if (!assignResponse.IsSuccessStatusCode)
                        {
                            string error = await assignResponse.Content.ReadAsStringAsync();
                            MessageBox.Show($"Failed to update questions with quiz ID.\n{error}", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Now create the ClassSession
                        var selectedCourse = courseDropdown.SelectedItem as Course;
                        if (selectedCourse == null)
                        {
                            MessageBox.Show("Please select a valid course.", "Invalid Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var classSessionDto = new
                        {
                            Course_Id = selectedCourse.CourseId,
                            SessionDate = dueDate,
                            Password = passwordTextBox.Text.Trim(),
                            QuizId = createdQuizId
                        };

                        string classSessionApiUrl = "http://localhost:5257/api/classsession";

                        var classSessionContent = new StringContent(JsonSerializer.Serialize(classSessionDto), System.Text.Encoding.UTF8, "application/json");

                        HttpResponseMessage sessionResponse = await client.PostAsync(classSessionApiUrl, classSessionContent);
                        if (sessionResponse.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Quiz and Class Session created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();

                        }
                        else
                        {
                            string error = await sessionResponse.Content.ReadAsStringAsync();
                            MessageBox.Show($"Quiz created but failed to create Class Session.\nServer Response: {error}", "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        if (sessionResponse.IsSuccessStatusCode)
                        {
                            // Step 1: Get ALL students (since we don’t have enrollment tracking yet)
                            string studentsUrl = "http://localhost:5257/api/students";
                            var studentsJson = await client.GetStringAsync(studentsUrl);
                            var students = JsonSerializer.Deserialize<List<Student>>(studentsJson);


                            // Step 2: Create attended_by rows
                            var attendanceDtos = students.Select(s => new
                            {
                                UtdId = s.UTDId,
                                Course_Id = selectedCourse.CourseId,
                                SessionDate = dueDate
                            }).ToList();

                            var attendanceContent = new StringContent(JsonSerializer.Serialize(attendanceDtos), Encoding.UTF8, "application/json");
                            var attendanceResponse =  await client.PostAsync("http://localhost:5257/api/attendance/bulk-create", attendanceContent);
                            string attendanceResult = await attendanceResponse.Content.ReadAsStringAsync();

                            //Show the server response in a popup
                            //MessageBox.Show($"Attendance API response:\n{attendanceResult}", "Attendance Endpoint", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Step 3: Now call the students-by-course-session endpoint
                            string sessionStudentsUrl = $"http://localhost:5257/api/students/students-by-course-session?courseId={selectedCourse.CourseId}&sessionDate={dueDate:yyyy-MM-dd}";
                            var sessionStudentsJson = await client.GetStringAsync(sessionStudentsUrl);
                            var sessionStudents = JsonSerializer.Deserialize<List<Student>>(sessionStudentsJson);

                            // Step 4: Create Submissions
                            var submissionDtos = sessionStudents.Select(s => new
                            {
                                Course_Id = selectedCourse.CourseId,
                                SessionDate = dueDate,
                                Utd_Id = s.UTDId,
                                Quiz_Id = createdQuizId,
                                Ip_Address = "0.0.0.0",
                                Submission_Time = DateTime.Parse("1900-01-01T00:00:00"),
                                Answer_1 = "N/A",
                                Answer_2 = "N/A",
                                Answer_3 = "N/A",
                                Status = "absent"
                            }).ToList();

                            var submissionContent = new StringContent(JsonSerializer.Serialize(submissionDtos), Encoding.UTF8, "application/json");
                            await client.PostAsync("http://localhost:5257/api/submissions/bulk-create", submissionContent);


                            MessageBox.Show("Quiz, Class Session, and Absences created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();
                        }
                        else
                        {
                            string error = await sessionResponse.Content.ReadAsStringAsync();
                            MessageBox.Show($"Quiz created but failed to create Class Session.\nServer Response: {error}", "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    else
                    {
                        string error = await quizResponse.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to create quiz.\nServer Response: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating quiz or class session: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

    // Class to represent a course
    // This class is used to populate the course dropdown in the form
    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public override string ToString()
        {
            return $"{CourseId} - {CourseName}";
        }
    }

    // Class to represent a question in the course question pool
    // This class is used to deserialize the JSON response from the API
    public class CourseQuestionPoolQuestion
    {
        [JsonPropertyName("course_Id")]
        public string CourseId { get; set; }

        [JsonPropertyName("course_Name")]
        public string CourseName { get; set; }

        [JsonPropertyName("start_Time")]
        public string StartTime { get; set; } // Because it’s coming as "05:00:00" (a time string)

        [JsonPropertyName("end_Time")]
        public string EndTime { get; set; } // Also a time string

        [JsonPropertyName("poolId")]
        public int PoolId { get; set; }

        [JsonPropertyName("poolName")]
        public string PoolName { get; set; }

        [JsonPropertyName("questionId")]
        public int QuestionId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("optionA")]
        public string OptionA { get; set; }

        [JsonPropertyName("optionB")]
        public string OptionB { get; set; }

        [JsonPropertyName("optionC")]
        public string OptionC { get; set; }

        [JsonPropertyName("optionD")]
        public string OptionD { get; set; }

        [JsonPropertyName("correctAnswer")]
        public string CorrectAnswer { get; set; }

        [JsonPropertyName("quizId")]
        public int? QuizId { get; set; }
    }

    // Class to represent a quiz response from the API
    // This class is used to deserialize the JSON response when creating a quiz
    public class QuizResponse
    {
        public int QuizId { get; set; }
        public DateTime? DueDate { get; set; }
        public int PoolId { get; set; }
    }

    // Class to represent a student
    // This class is used to deserialize the JSON response from the API when fetching students by course
    public class Student
    {
        public string UTDId { get; set; }
    }



}