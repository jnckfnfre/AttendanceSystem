/*
    Eduardo Zamora 4/10/2025
    View attendance form to view attendance records by specifying class and session.
    Right now it is hardcoded to show attendance records for two students.
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.Json;

namespace AttendanceDesktop
{
    public partial class ViewAttendanceForm : Form
    {
        private Panel filterPanel;
        private Label filterLabel;
        private ComboBox filterComboBox;
        private TextBox filterTextBox;
        private Button applyFilterButton;
        private Label classLabel;
        private ComboBox classComboBox;
        private Label sessionLabel;
        private ComboBox sessionComboBox;
        private Button viewAttendanceButton;
        private DataGridView attendanceDataGridView;
        private ContextMenuStrip attendanceContextMenu;

        public ViewAttendanceForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WindowState = FormWindowState.Maximized;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "View Attendance Page";
            this.Font = new System.Drawing.Font("Segoe UI", 10F);

            Color primaryColor = Color.FromArgb(199, 91, 18);  // (199, 91, 18)
            Color secondaryColor = Color.FromArgb(0, 133, 66); // (0, 133, 66)

            // Filter Panel
            this.filterPanel = new Panel();
            this.filterPanel.Dock = DockStyle.Top;
            this.filterPanel.Height = 60;  // Keep this the same
            this.filterPanel.BackColor = secondaryColor;
            this.filterPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.filterPanel);

            // Filter Label
            this.filterLabel = new Label();
            this.filterLabel.Text = "Filter By:";
            this.filterLabel.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.filterLabel.ForeColor = Color.White;
            this.filterLabel.Location = new System.Drawing.Point(10, 15);  // Keep position
            this.filterLabel.AutoSize = true;
            this.filterPanel.Controls.Add(this.filterLabel);

            // Filter ComboBox 
            this.filterComboBox = new ComboBox();
            this.filterComboBox.Location = new System.Drawing.Point(120, 10);  // Keep position
            this.filterComboBox.Size = new System.Drawing.Size(300, 25);  // Keep size
            this.filterComboBox.SelectedIndexChanged += new EventHandler(this.FilterComboBox_SelectedIndexChanged);
            this.filterPanel.Controls.Add(this.filterComboBox);

            // Filter TextBox 
            this.filterTextBox = new TextBox();
            this.filterTextBox.Location = new System.Drawing.Point(440, 10);  // Moved 10px right
            this.filterTextBox.Size = new System.Drawing.Size(120, 25);  // Increased width to 100
            this.filterPanel.Controls.Add(this.filterTextBox);

            // Apply Filter Button 
            this.applyFilterButton = new Button();
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.BackColor = primaryColor;
            this.applyFilterButton.ForeColor = Color.White;
            this.applyFilterButton.FlatStyle = FlatStyle.Flat;
            this.applyFilterButton.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.applyFilterButton.Location = new System.Drawing.Point(580, 10);  // Moved further right
            this.applyFilterButton.Size = new System.Drawing.Size(100, 35);  // Increased width to 100
            this.applyFilterButton.Click += new EventHandler(this.ApplyFilterButton_Click);
            this.filterPanel.Controls.Add(this.applyFilterButton);

            // Class Label
            this.classLabel = new Label();
            this.classLabel.Text = "Class:";
            this.classLabel.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.classLabel.ForeColor = primaryColor;
            this.classLabel.Location = new System.Drawing.Point(50, 70);
            this.classLabel.AutoSize = true;
            this.Controls.Add(this.classLabel);

            // Class ComboBox
            this.classComboBox = new ComboBox();
            this.classComboBox.Location = new System.Drawing.Point(150, 70);
            this.classComboBox.Size = new System.Drawing.Size(300, 25);
            //this.classComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.classComboBox.SelectedIndexChanged += new EventHandler(this.ClassComboBox_SelectedIndexChanged);
            this.Controls.Add(this.classComboBox);

            // Session Label
            this.sessionLabel = new Label();
            this.sessionLabel.Text = "Session:";
            this.sessionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.sessionLabel.ForeColor = primaryColor;
            this.sessionLabel.Location = new System.Drawing.Point(50, 110);
            this.sessionLabel.AutoSize = true;
            this.Controls.Add(this.sessionLabel);

            // Session ComboBox
            this.sessionComboBox = new ComboBox();
            this.sessionComboBox.Location = new System.Drawing.Point(150, 110);
            this.sessionComboBox.Size = new System.Drawing.Size(300, 25);
            this.Controls.Add(this.sessionComboBox);

            // View Attendance Button
            this.viewAttendanceButton = new Button();
            this.viewAttendanceButton.Text = "View Attendance";
            this.viewAttendanceButton.BackColor = secondaryColor;
            this.viewAttendanceButton.ForeColor = Color.White;
            this.viewAttendanceButton.FlatStyle = FlatStyle.Flat;
            this.viewAttendanceButton.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.viewAttendanceButton.Location = new System.Drawing.Point(150, 150);
            this.viewAttendanceButton.Size = new System.Drawing.Size(200, 45);
            this.viewAttendanceButton.Click += new EventHandler(this.ViewAttendanceButton_Click);
            this.Controls.Add(this.viewAttendanceButton);

            // Attendance DataGridView
            this.attendanceDataGridView = new DataGridView();
            this.attendanceDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.attendanceDataGridView.Location = new System.Drawing.Point(50, 200);
            this.attendanceDataGridView.Size = new System.Drawing.Size(this.ClientSize.Width - 100, this.ClientSize.Height - 250); // Adjusts dynamically
            this.attendanceDataGridView.Size = new System.Drawing.Size(700, 350);
            this.attendanceDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.attendanceDataGridView.BorderStyle = BorderStyle.Fixed3D;
            this.attendanceDataGridView.BackgroundColor = Color.FromArgb(169, 169, 169);
            this.attendanceDataGridView.GridColor = primaryColor;
            this.attendanceDataGridView.DefaultCellStyle.BackColor = Color.White;
            this.attendanceDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 235, 224); // subtle orange highlight
            this.attendanceDataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.attendanceDataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.attendanceDataGridView.ReadOnly = true;  // Make grid non-editable
            this.Controls.Add(this.attendanceDataGridView);

            // Context Menu for DataGridView
            this.attendanceContextMenu = new ContextMenuStrip();
            this.attendanceContextMenu.Items.Add("Change Status", null, this.ChangeStatus_Click);
            this.attendanceDataGridView.ContextMenuStrip = this.attendanceContextMenu;
            this.attendanceDataGridView.ContextMenuStrip = this.attendanceContextMenu;

            filterComboBox.Items.AddRange(new string[] {
                "Status: Present",
                "Status: Absent",
                "Same IP Address",
                "By UTD ID",
                "Missing 3 Classes in a Row",
                "Absences >= N"
            });

            // Load class list
            LoadClasses();
        }

        /* 
            David Sajdak & Eduardo Zamora 4/15/25
            Function uses api request to load classes for reporting functionality
        */
        private async void LoadClasses()
        {
            try
            {
                string apiUrl = "http://localhost:5257/api/courses";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    // MessageBox.Show("Json to deserialize: " + json);

                    var doc = JsonDocument.Parse(json);
                    // MessageBox.Show(doc.RootElement.ToString());

                    var classes = doc.RootElement.EnumerateArray()
                                    .Select(element => new Course
                                    {
                                        CourseId = element.GetProperty("course_Id").GetString(),
                                        CourseName = element.GetProperty("course_Name").GetString()
                                    })
                                    .ToList();

                    classComboBox.Items.Clear();
                    foreach (var course in classes)
                    {
                        classComboBox.Items.Add(course);
                    }

                    classComboBox.DisplayMember = "CourseName";
                    classComboBox.ValueMember = "CourseId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching classes: {ex.Message}");
            }
        }

        // Eduardo Zamora 4/16/2025
        // Function uses api request to load class sessions for reporting functionality
        // This function is called when the selected class changes in the classComboBox.
        private async void ClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.classComboBox.SelectedItem is Course selectedCourse)
            {
                string courseId = selectedCourse.CourseId;
                string apiUrl = "http://localhost:5257/api/ClassSession";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        var doc = JsonDocument.Parse(json);

                        var filteredSessions = doc.RootElement.EnumerateArray()
                            .Where(element =>
                                element.TryGetProperty("course_Id", out var cidProp) &&
                                cidProp.GetString() == courseId &&
                                element.TryGetProperty("sessionDate", out _)) //&&
                                //element.TryGetProperty("quizId", out _))
                            .Select(element =>
                                $"{element.GetProperty("sessionDate").GetString()}")
                            .ToList();


                        this.sessionComboBox.Items.Clear();
                        this.sessionComboBox.Items.AddRange(filteredSessions.ToArray());

                        if (filteredSessions.Count == 0)
                        {
                            MessageBox.Show($"No sessions found for course: {courseId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching sessions: " + ex.Message);
                    }
                }
            }
        }

        // Store original data source for filtering
        private List<Submission> originalDataSource = new List<Submission>(); 

        // Eduardo Zamora 4/18/2025
        // Function to apply filter based on user selection
        // This function is called when the "Apply" button is clicked.
        private async void ApplyFilterButton_Click(object sender, EventArgs e)
        {
            // Check if there's any data in the DataGridView
            if (attendanceDataGridView.DataSource == null)
            {
                MessageBox.Show("No attendance data loaded. Please view attendance first.");
                return;
            }

            // Get the current filter selection
            if (filterComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a filter option.");
                return;
            }

            string selectedFilter = filterComboBox.SelectedItem.ToString();

            // Apply filter based on selection
            List<Submission> filteredData = new List<Submission>();

            var selectedCourse = (Course)classComboBox.SelectedItem;
            var allSubmissions = await GetAllSubmissions();
            string[] columnsToHide = { "quiz_Id", "submission_Time", "answer_1", "answer_2", "answer_3" };

            switch (selectedFilter)
            {
                case "Status: Present":
                    if (sessionComboBox.SelectedItem == null || classComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a class or class session first.");
                        return;
                    }
                    filteredData = originalDataSource.Where(s => 
                        s.status != null && s.status.Equals("present", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Status: Absent":
                    if (sessionComboBox.SelectedItem == null || classComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a class or class session first.");
                        return;
                    }
                    filteredData = originalDataSource.Where(s => 
                        s.status != null && s.status.Equals("absent", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Same IP Address":
                    filteredData = FindStudentsWithSameIP(originalDataSource);
                    if (sessionComboBox.SelectedItem == null || classComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a class or class session first.");
                        return;
                    }
                    if (filteredData.Count == 0)
                    {
                        MessageBox.Show("All IP addresses are unique - no students share the same IP address.");
                        return; // Exit without changing the DataGridView
                    }
                    break;
                case "By UTD ID":
                    if (string.IsNullOrWhiteSpace(filterTextBox.Text))
                    {
                        MessageBox.Show("Please enter a UTD ID in the filter box");
                        return;
                    }

                    if (classComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a class first");
                        return;
                    }

                    // Clear session selection
                    sessionComboBox.SelectedIndex = -1;
                    sessionComboBox.Text = string.Empty;
                    
                    string searchId = filterTextBox.Text.Trim();

                    filteredData = allSubmissions
                        .Where(s => s.utd_Id.Equals(searchId, StringComparison.OrdinalIgnoreCase) &&
                        s.course_Id == selectedCourse.CourseId) // Add course filter
                        .OrderBy(s => s.sessionDate)
                        .ToList();
                    
                    if (filteredData.Count == 0)
                    {
                        MessageBox.Show($"No records found for UTD ID: {searchId} in {selectedCourse.CourseName}");
                        return;
                    }
                    
                    // Configure grid to show history view
                    foreach (DataGridViewColumn column in attendanceDataGridView.Columns)
                    {
                        column.Visible = true; // Show all columns initially
                    }
                    
                    // Hide columns we don't need
                    
                    foreach (var colName in columnsToHide)
                    {
                        if (attendanceDataGridView.Columns[colName] != null)
                        {
                            attendanceDataGridView.Columns[colName].Visible = false;
                        }
                    }
                    
                    // Format date column
                    if (attendanceDataGridView.Columns["sessionDate"] != null)
                    {
                        attendanceDataGridView.Columns["sessionDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                    }
                    break;
                case "Missing 3 Classes in a Row":
                    if (classComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a class first");
                        return;
                    }
                    
                    // Clear session selection
                    sessionComboBox.SelectedIndex = -1;
                    sessionComboBox.Text = string.Empty;
                    
                    filteredData = FindStudentsWith3ConsecutiveAbsences(allSubmissions, selectedCourse.CourseId);
                    
                    if (filteredData.Count == 0)
                    {
                        MessageBox.Show("No students with 3+ consecutive absences found");
                        return;
                    }
                    
                    // Configure grid for history view
                    foreach (DataGridViewColumn column in attendanceDataGridView.Columns)
                    {
                        column.Visible = true; // Show all columns initially
                    }
                    
                    // Hide columns we don't need
                    foreach (var colName in columnsToHide)
                    {
                        if (attendanceDataGridView.Columns[colName] != null)
                        {
                            attendanceDataGridView.Columns[colName].Visible = false;
                        }
                    }
                    
                    // Format date column
                    if (attendanceDataGridView.Columns["sessionDate"] != null)
                    {
                        attendanceDataGridView.Columns["sessionDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                    }
                    
                    // Add visual indicator for absences
                    attendanceDataGridView.DefaultCellStyle.BackColor = Color.White;
                    attendanceDataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    attendanceDataGridView.RowsDefaultCellStyle.BackColor = Color.White;
                    
                    foreach (DataGridViewRow row in attendanceDataGridView.Rows)
                    {
                        var submission = row.DataBoundItem as Submission;
                        if (submission?.status?.Equals("absent", StringComparison.OrdinalIgnoreCase) == true)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                    break;
                case "Absences >= N":
                    if (classComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a class first");
                        return;
                    }
                    
                    if (!int.TryParse(filterTextBox.Text, out int minAbsences) || minAbsences < 1)
                    {
                        MessageBox.Show("Please enter a valid number (1 or higher)");
                        return;
                    }
                    
                    // Clear session selection
                    sessionComboBox.SelectedIndex = -1;
                    sessionComboBox.Text = string.Empty;
                    
                    filteredData = GetAbsentSessionsForStudentsWithNAbsences(allSubmissions, selectedCourse.CourseId, minAbsences);
                    
                    if (filteredData.Count == 0)
                    {
                        MessageBox.Show($"No students with {minAbsences}+ absent sessions found");
                        return;
                    }
                    
                    // Configure grid
                    foreach (DataGridViewColumn column in attendanceDataGridView.Columns)
                    {
                        column.Visible = true; // Show all columns initially
                    }
                    
                    // Hide columns we don't need
                    foreach (var colName in columnsToHide)
                    {
                        if (attendanceDataGridView.Columns[colName] != null)
                        {
                            attendanceDataGridView.Columns[colName].Visible = false;
                        }
                    }
                    
                    // Format date column
                    if (attendanceDataGridView.Columns["sessionDate"] != null)
                    {
                        attendanceDataGridView.Columns["sessionDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                    }
                    break;
                default:
                    // If no specific filter matches, show all data
                    filteredData = originalDataSource.ToList();
                    break;
            }

            // Update the DataGridView with filtered data
            attendanceDataGridView.DataSource = filteredData;

            // Show message if no records found for the filter
            if (filteredData.Count == 0)
            {
                MessageBox.Show($"No {selectedFilter.ToLower()} students found for this session.");
            }
        }

        // Eduardo Zamora 4/18/2025
        // Function to find students with the same IP address
        private List<Submission> FindStudentsWithSameIP(List<Submission> submissions)
        {
            // Group submissions by IP address and only keep groups with more than one student
            var ipGroups = submissions
                .Where(s => !string.IsNullOrEmpty(s.ip_Address))
                .GroupBy(s => s.ip_Address)
                .Where(g => g.Count() > 1)
                .ToList();

            // Flatten the groups into a single list
            var result = new List<Submission>();
            foreach (var group in ipGroups)
            {
                result.AddRange(group);
            }

            // Sort by IP address for better readability
            return result.OrderBy(s => s.ip_Address).ToList();
        }

        // Eduardo Zamora 4/19/2025
        // Function to view full history of a student
        private async void ViewFullHistory_Click(object sender, EventArgs e)
        {
            if (attendanceDataGridView.SelectedRows.Count == 0) return;
            
            var selectedRow = attendanceDataGridView.SelectedRows[0];
            var submission = selectedRow.DataBoundItem as Submission;
            
            if (submission == null) return;

            try 
            {
                // Get all submissions for this student
                var allSubmissions = await GetAllSubmissions();
                var studentHistory = allSubmissions
                    .Where(s => s.utd_Id == submission.utd_Id)
                    .OrderBy(s => s.sessionDate)
                    .ToList();
                
                if (studentHistory.Count == 0)
                {
                    MessageBox.Show("No additional attendance records found for this student");
                    return;
                }

                // Configure grid for history view
                ConfigureHistoryView();
                
                // Bind the data
                attendanceDataGridView.DataSource = studentHistory;
                
                // Store the original data for returning
                originalDataSource = studentHistory;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student history: {ex.Message}");
            }
        }

        // Eduardo Zamora 4/19/2025
        // Function to get all submissions from the API
        // This function is used to load all submissions for filtering and history view
        private async Task<List<Submission>> GetAllSubmissions()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync("http://localhost:5257/api/Submissions/WithStudent");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Submission>>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading submissions: {ex.Message}");
                return new List<Submission>();
            }
        }


        // Eduardo Zamora 4/19/2025
        // Function to configure the DataGridView for history view
        private void ConfigureHistoryView()
        {
            // First reset all columns to visible
            foreach (DataGridViewColumn column in attendanceDataGridView.Columns)
            {
                column.Visible = true;
            }

            // Then hide the ones we don't want
            string[] columnsToHide2 = { "quiz_Id", "submission_Time", "answer_1", "answer_2", "answer_3" };
            foreach (var colName in columnsToHide2)
            {
                if (attendanceDataGridView.Columns[colName] != null)
                {
                    attendanceDataGridView.Columns[colName].Visible = false;
                }
            }

            // Format the date column
            if (attendanceDataGridView.Columns["sessionDate"] != null)
            {
                attendanceDataGridView.Columns["sessionDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the filter textbox when a new filter option is selected
            filterTextBox.Clear();
            
            // Optionally, enable/disable the textbox based on the selected filter
            string selectedFilter = filterComboBox.SelectedItem?.ToString();
            if (selectedFilter == "By UTD ID" || selectedFilter == "Absences >= N")
            {
                filterTextBox.Enabled = true;
            }
            else
            {
                filterTextBox.Enabled = false;
            }
        }

        /* 
            Eduardo Zamora
            Revised by David Sajdak 4/17/2025
            Displays attendance information by class and session date
        */
        private async void ViewAttendanceButton_Click(object sender, EventArgs e)
        {
            // Validate that both class and session are selected
            if (this.classComboBox.SelectedItem == null || this.sessionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both a class and a class session.");
                return;
            }

            var selectedCourse = this.classComboBox.SelectedItem as Course;
            var selectedSession = this.sessionComboBox.SelectedItem.ToString();

            if (selectedCourse == null || string.IsNullOrEmpty(selectedSession))
            {
                MessageBox.Show("Invalid class or session selection.");
                return;
            }

            try
            {
                string submissionsApiUrl = "http://localhost:5257/api/Submissions/WithStudent";
                List<Submission> allSubmissions;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(submissionsApiUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show(json);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    allSubmissions = JsonSerializer.Deserialize<List<Submission>>(json, options);
                }

                //MessageBox.Show($"Selected Course ID: {selectedCourse.CourseId}, Selected Session: {selectedSession}");

                // Filter for selected course and session
                var filteredSubmissions = allSubmissions
                    .Where(s => s.course_Id == selectedCourse.CourseId &&
                                s.sessionDate.ToString("yyyy-MM-dd") == DateTime.Parse(selectedSession).ToString("yyyy-MM-dd"))
                    .ToList();

                // Clear any previous filters when view attendance is clicked
                filterComboBox.Text = string.Empty;
                filterTextBox.Text = string.Empty;
                

                if (filteredSubmissions.Count == 0)
                {
                    MessageBox.Show("No attendance records found for the selected class and session.");
                    return;
                }

                // Store the original data for filtering
                originalDataSource = filteredSubmissions;
                 // Set full data first
                this.attendanceDataGridView.DataSource = filteredSubmissions;

                // hide the columns don't want to see
                foreach (DataGridViewColumn column in attendanceDataGridView.Columns)
                {
                    if (
                        column.Name != "submission_Id" &&
                        column.Name != "course_Id" &&
                        column.Name != "utd_Id" &&
                        column.Name != "status" &&
                        column.Name != "ip_Address" &&
                        column.Name != "student_Name")
                    {
                        column.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching attendance: " + ex.Message);
            }
        }

        // Eduardo Zamora 4/19/2025
        // Function to find students with 3+ consecutive absences
        private List<Submission> FindStudentsWith3ConsecutiveAbsences(List<Submission> submissions, string courseId)
        {
            var result = new List<Submission>();
            
            // Group by student
            var byStudent = submissions
                .Where(s => s.course_Id == courseId)
                .GroupBy(s => s.utd_Id);
            
            foreach (var studentGroup in byStudent)
            {
                // Order by session date
                var orderedSessions = studentGroup.OrderBy(s => s.sessionDate).ToList();
                int consecutiveAbsences = 0;
                
                for (int i = 0; i < orderedSessions.Count; i++)
                {
                    if (orderedSessions[i].status?.Equals("absent", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        consecutiveAbsences++;
                        
                        // If we find 3+ consecutive absences
                        if (consecutiveAbsences >= 3)
                        {
                            // Add all sessions in this consecutive absence streak
                            for (int j = i - consecutiveAbsences + 1; j <= i; j++)
                            {
                                if (!result.Contains(orderedSessions[j]))
                                {
                                    result.Add(orderedSessions[j]);
                                }
                            }
                        }
                    }
                    else
                    {
                        consecutiveAbsences = 0; // Reset counter if present
                    }
                }
            }
            
            return result.OrderBy(s => s.student_Name)
                        .ThenBy(s => s.sessionDate)
                        .ToList();
        }

        // Eduardo Zamora 4/19/2025
        // Function to get students with N absences
        // This function filters the submissions to find students with N or more absences
        private List<Submission> GetAbsentSessionsForStudentsWithNAbsences(List<Submission> allSubmissions, string courseId, int minAbsences)
        {
            var result = new List<Submission>();
            
            // Group by student and count absences
            var byStudent = allSubmissions
                .Where(s => s.course_Id == courseId)
                .GroupBy(s => new { s.utd_Id, s.student_Name });
            
            foreach (var studentGroup in byStudent)
            {
                var absentSessions = studentGroup
                    .Where(s => s.status?.Equals("absent", StringComparison.OrdinalIgnoreCase) == true)
                    .OrderBy(s => s.sessionDate)
                    .ToList();
                
                if (absentSessions.Count >= minAbsences)
                {
                    result.AddRange(absentSessions);
                }
            }
            
            return result.OrderBy(s => s.student_Name)
                        .ThenBy(s => s.sessionDate)
                        .ToList();
        }

        // method to chanhge status of attendance record
        private void ChangeStatus_Click(object sender, EventArgs e)
        {
            if (this.attendanceDataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = this.attendanceDataGridView.SelectedRows[0];
                var record = selectedRow.DataBoundItem as AttendanceRecord;
        
                if (record != null)
                {
                    // Open a dialog to change the status
                    string[] statuses = { "Present", "Absent"};
                    string newStatus = PromptForSelection("Change Status", "Select a new status:", statuses);
        
                    if (!string.IsNullOrEmpty(newStatus))
                    {
                        // Update TotalAbsences if the status is changed to "Absent"
                        if (newStatus == "Absent" && record.Status != "Absent")
                        {
                            record.TotalAbsences++; // Increment TotalAbsences
                        }
                        else if (newStatus == "Present" && record.Status == "Absent")
                        {
                            record.TotalAbsences--; // Decrement TotalAbsences if switching back to Present
                        }

                        // Update the status
                        record.Status = newStatus;

                        // Refresh the DataGridView to reflect changes
                        this.attendanceDataGridView.Refresh();

                        MessageBox.Show($"Changed {record.Name}'s status to {newStatus}.");
                    }
                }
            }
        }

        // method to prompt user for selection from a list of options
        // This method creates a simple form with a ComboBox and an OK button.
        private string PromptForSelection(string title, string prompt, string[] options)
        {
            using (Form promptForm = new Form())
            {
                promptForm.Text = title;
                promptForm.Size = new System.Drawing.Size(300, 200);
        
                Label textLabel = new Label() { Left = 20, Top = 20, Text = prompt, AutoSize = true };
                ComboBox comboBox = new ComboBox() { Left = 20, Top = 50, Width = 240 };
                comboBox.Items.AddRange(options);
                Button confirmation = new Button() { Text = "OK", Left = 20, Width = 100, Top = 100, DialogResult = DialogResult.OK };
        
                promptForm.Controls.Add(textLabel);
                promptForm.Controls.Add(comboBox);
                promptForm.Controls.Add(confirmation);
                promptForm.AcceptButton = confirmation;
        
                if (promptForm.ShowDialog() == DialogResult.OK && comboBox.SelectedItem != null)
                {
                    return comboBox.SelectedItem.ToString();
                }
            }
        
            return null;
        }

        public class Course
        {
            public string CourseId { get; set; }
            public string CourseName { get; set; }
            public override string ToString()
            {
                return CourseName; // Ensures the name shows in the dropdown
            }
        }

        public class Submission
        {
            public int submission_Id { get; set; }
            public string course_Id { get; set; }
            public DateTime sessionDate { get; set; }
            public string utd_Id { get; set; }
            public string student_Name { get; set; }
            public int quiz_Id { get; set; }
            public string ip_Address { get; set; }
            public DateTime submission_Time { get; set; }
            public string answer_1 { get; set; }
            public string answer_2 { get; set; }
            public string answer_3 { get; set; }
            public string status { get; set; }
        }

        private class Student
        {
            public string utd_Id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
        }



        // Simulated attendance record class (replace with actual model class!!)
        private class AttendanceRecord
        {
            public string StudentId { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string IPAddress { get; set; }

            public DateTime SessionDate { get; set; }

            public int TotalAbsences { get; set; } // New property to track total absences
        }
    }
}