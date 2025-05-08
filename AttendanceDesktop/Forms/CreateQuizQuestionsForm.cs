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
                string sessionDateInput = sessionTextBox.Text.Trim();

                if (!DateTime.TryParse(sessionDateInput, out DateTime dueDate))
                {
                    MessageBox.Show("Invalid session date format. Please use YYYY-MM-DD.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedCourse = courseDropdown.SelectedItem as Course;
                if (selectedCourse == null)
                {
                    MessageBox.Show("Please select a valid course.", "Invalid Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (HttpClient client = new HttpClient())
                {
                    // Check enrolled students FIRST
                    string studentsUrl = $"http://localhost:5257/api/CourseStudents/by-course/{selectedCourse.CourseId}";
                    var studentsJson = await client.GetStringAsync(studentsUrl);
                    var students = JsonSerializer.Deserialize<List<Student>>(studentsJson);

                    if (students == null || students.Count == 0)
                    {
                        MessageBox.Show("No students are enrolled in this course. Please enroll students before creating a quiz.", 
                                        "No Enrolled Students", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Create quiz
                    var quizDto = new
                    {
                        Due_Date = dueDate,
                        Pool_Id = poolId
                    };

                    string quizApiUrl = "http://localhost:5257/api/quiz";
                    var quizContent = new StringContent(JsonSerializer.Serialize(quizDto), Encoding.UTF8, "application/json");
                    HttpResponseMessage quizResponse = await client.PostAsync(quizApiUrl, quizContent);
                    if (!quizResponse.IsSuccessStatusCode)
                    {
                        string error = await quizResponse.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to create quiz.\nServer Response: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var quizJson = await quizResponse.Content.ReadAsStringAsync();
                    int createdQuizId = JsonSerializer.Deserialize<JsonElement>(quizJson).GetProperty("quiz_Id").GetInt32();

                    bool rollbackNeeded = true;
                    try
                    {
                        // Assign questions to quiz
                        foreach (var question in selectedQuestions)
                            question.QuizId = createdQuizId;

                        var updateDtos = selectedQuestions.Select(q => new
                        {
                            Questions_Id = q.QuestionId,
                            Text = q.Text,
                            Option_A = q.OptionA,
                            Option_B = q.OptionB,
                            Option_C = q.OptionC,
                            Option_D = q.OptionD,
                            Correct_Answer = q.CorrectAnswer,
                            Quiz_Id = q.QuizId,
                            Pool_Id = q.PoolId
                        }).ToList();

                        string assignUrl = "http://localhost:5257/api/questions/AssignQuizToQuestions";
                        var assignContent = new StringContent(JsonSerializer.Serialize(updateDtos), Encoding.UTF8, "application/json");
                        HttpResponseMessage assignResponse = await client.PostAsync(assignUrl, assignContent);
                        if (!assignResponse.IsSuccessStatusCode)
                        {
                            string error = await assignResponse.Content.ReadAsStringAsync();
                            throw new Exception($"Failed to update questions with quiz ID: {error}");
                        }

                        // Create class session
                        var classSessionDto = new
                        {
                            Course_Id = selectedCourse.CourseId,
                            SessionDate = dueDate,
                            Password = passwordTextBox.Text.Trim(),
                            Quiz_Id = createdQuizId
                        };

                        string classSessionApiUrl = "http://localhost:5257/api/classsession";
                        var classSessionContent = new StringContent(JsonSerializer.Serialize(classSessionDto), Encoding.UTF8, "application/json");
                        HttpResponseMessage sessionResponse = await client.PostAsync(classSessionApiUrl, classSessionContent);
                        if (!sessionResponse.IsSuccessStatusCode)
                        {
                            string error = await sessionResponse.Content.ReadAsStringAsync();
                            throw new Exception($"Failed to create Class Session: {error}");
                        }

                        // Create submissions for students
                        var submissionDtos = students.Select(s => new
                        {
                            Course_Id = selectedCourse.CourseId,
                            Session_Date = dueDate,
                            Utd_Id = s.Utd_Id,
                            Quiz_Id = createdQuizId,
                            Ip_Address = "0.0.0.0",
                            Submission_Time = DateTime.Parse("1900-01-01T00:00:00"),
                            Answer_1 = "x",
                            Answer_2 = "x",
                            Answer_3 = "x",
                            Status = "Absent"
                        }).ToList();

                        var submissionContent = new StringContent(JsonSerializer.Serialize(submissionDtos), Encoding.UTF8, "application/json");
                        var submissionResponse = await client.PostAsync("http://localhost:5257/api/submissions/bulk-create", submissionContent);
                        if (!submissionResponse.IsSuccessStatusCode)
                        {
                            string error = await submissionResponse.Content.ReadAsStringAsync();
                            throw new Exception($"Failed to create submissions: {error}");
                        }

                        rollbackNeeded = false;
                        MessageBox.Show("Quiz, Class Session, and Submissions created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error during creation process: {ex.Message}\nRolling back created resources.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (rollbackNeeded)
                        {
                            try
                            {
                                await client.DeleteAsync($"{quizApiUrl}/{createdQuizId}");
                            }
                            catch (Exception rollbackEx)
                            {
                                MessageBox.Show($"Failed to rollback quiz creation: {rollbackEx.Message}\nPlease manually delete quiz ID {createdQuizId}", "Rollback Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
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

        [JsonPropertyName("pool_Id")]
        public int PoolId { get; set; }

        [JsonPropertyName("pool_Name")]
        public string PoolName { get; set; }

        [JsonPropertyName("question_Id")]
        public int QuestionId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("option_A")]
        public string OptionA { get; set; }

        [JsonPropertyName("option_B")]
        public string OptionB { get; set; }

        [JsonPropertyName("option_C")]
        public string OptionC { get; set; }

        [JsonPropertyName("option_D")]
        public string OptionD { get; set; }

        [JsonPropertyName("correct_Answer")]
        public string CorrectAnswer { get; set; }

        [JsonPropertyName("quiz_Id")]
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
        [JsonPropertyName("utd_Id")]
        public string Utd_Id { get; set; }
    }
}