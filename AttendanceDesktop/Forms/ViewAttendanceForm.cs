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
            this.filterPanel.Controls.Add(this.filterComboBox);

            // Filter TextBox 
            this.filterTextBox = new TextBox();
            this.filterTextBox.Location = new System.Drawing.Point(440, 10);  // Moved 10px right
            this.filterTextBox.Size = new System.Drawing.Size(70, 25);  // Increased width to 100
            this.filterPanel.Controls.Add(this.filterTextBox);

            // Apply Filter Button 
            this.applyFilterButton = new Button();
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.BackColor = primaryColor;
            this.applyFilterButton.ForeColor = Color.White;
            this.applyFilterButton.FlatStyle = FlatStyle.Flat;
            this.applyFilterButton.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.applyFilterButton.Location = new System.Drawing.Point(525, 10);  // Moved further right
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

            filterComboBox.Items.AddRange(new string[] {
                "IP Address",
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


        private List<AttendanceRecord> allAttendanceRecords = new List<AttendanceRecord>(); // populate from your API

        private void ApplyFilterButton_Click(object sender, EventArgs e)
        {
            if (this.filterComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a filter criteria.");
                return;
            }

            string selectedFilter = this.filterComboBox.SelectedItem.ToString();
            string filterValue = this.filterTextBox.Text;
            IEnumerable<AttendanceRecord> filteredRecords = allAttendanceRecords;

            switch (selectedFilter)
            {
                case "IP Address":
                    filteredRecords = allAttendanceRecords
                        .Where(r => r.IPAddress == filterValue);
                    break;

                case "Missing 3 Classes in a Row":
                    filteredRecords = FilterMissingThreeInARow(allAttendanceRecords);
                    break;

                case "Absences >= N":
                    if (int.TryParse(filterValue, out int threshold))
                    {
                        filteredRecords = allAttendanceRecords
                            .GroupBy(r => r.StudentId)
                            .Where(g => g.Count(r => r.Status == "Absent") >= threshold)
                            .SelectMany(g => g);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number for absences.");
                        return;
                    }
                    break;

                default:
                    MessageBox.Show("Unknown filter type.");
                    return;
            }

            attendanceDataGridView.DataSource = filteredRecords.ToList();
        }

        private IEnumerable<AttendanceRecord> FilterMissingThreeInARow(List<AttendanceRecord> records)
        {
            var result = new List<AttendanceRecord>();

            var groupedByStudent = records
                .GroupBy(r => r.StudentId);

            foreach (var group in groupedByStudent)
            {
                var ordered = group.OrderBy(r => r.SessionDate).ToList();
                int consecutiveAbsences = 0;

                for (int i = 0; i < ordered.Count; i++)
                {
                    if (ordered[i].Status == "Absent")
                    {
                        consecutiveAbsences++;
                        if (consecutiveAbsences == 3)
                        {
                            result.AddRange(ordered.Skip(i - 2).Take(3));
                            break;
                        }
                    }
                    else
                    {
                        consecutiveAbsences = 0;
                    }
                }
            }

            return result;
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
                    MessageBox.Show(json);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    allSubmissions = JsonSerializer.Deserialize<List<Submission>>(json, options);
                }

                MessageBox.Show($"Selected Course ID: {selectedCourse.CourseId}, Selected Session: {selectedSession}");

                // Filter for selected course and session
                var filteredSubmissions = allSubmissions
                    .Where(s => s.course_Id == selectedCourse.CourseId &&
                                s.sessionDate.ToString("yyyy-MM-dd") == DateTime.Parse(selectedSession).ToString("yyyy-MM-dd"))
                    .ToList();

                if (filteredSubmissions.Count == 0)
                {
                    MessageBox.Show("No attendance records found for the selected class and session.");
                    return;
                }

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