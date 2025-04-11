/*
    Eduardo Zamora 4/10/2025
    View attendance form to view attendance records by specifying class and session.
    Right now it is hardcoded to show attendance records for two students.
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "View Attendance Page";

            // Filter Panel
            this.filterPanel = new Panel();
            this.filterPanel.Location = new System.Drawing.Point(50, 10);
            this.filterPanel.Size = new System.Drawing.Size(700, 40);
            this.Controls.Add(this.filterPanel);

            // Filter Label
            this.filterLabel = new Label();
            this.filterLabel.Text = "Filter By:";
            this.filterLabel.Location = new System.Drawing.Point(0, 10);
            this.filterPanel.Controls.Add(this.filterLabel);

            // Filter ComboBox
            this.filterComboBox = new ComboBox();
            this.filterComboBox.Location = new System.Drawing.Point(70, 5);
            this.filterComboBox.Size = new System.Drawing.Size(200, 30);
            this.filterComboBox.Items.AddRange(new string[]
            {
                "Missed 3 classes in a row",
                "More than X absences"
            });
            this.filterPanel.Controls.Add(this.filterComboBox);

            // Filter TextBox
            this.filterTextBox = new TextBox();
            this.filterTextBox.Location = new System.Drawing.Point(280, 5);
            this.filterTextBox.Size = new System.Drawing.Size(100, 30);
            this.filterPanel.Controls.Add(this.filterTextBox);

            // Apply Filter Button
            this.applyFilterButton = new Button();
            this.applyFilterButton.Text = "Apply Filter";
            this.applyFilterButton.Location = new System.Drawing.Point(390, 5);
            this.applyFilterButton.Click += new EventHandler(this.ApplyFilterButton_Click);
            this.filterPanel.Controls.Add(this.applyFilterButton);

            // Class Label
            this.classLabel = new Label();
            this.classLabel.Text = "Class:";
            this.classLabel.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(this.classLabel);

            // Class ComboBox
            this.classComboBox = new ComboBox();
            this.classComboBox.Location = new System.Drawing.Point(150, 50);
            this.classComboBox.Size = new System.Drawing.Size(200, 30);
            this.classComboBox.SelectedIndexChanged += new EventHandler(this.ClassComboBox_SelectedIndexChanged);
            this.Controls.Add(this.classComboBox);

            // Session Label
            this.sessionLabel = new Label();
            this.sessionLabel.Text = "Class Session:";
            this.sessionLabel.Location = new System.Drawing.Point(50, 100);
            this.Controls.Add(this.sessionLabel);

            // Session ComboBox
            this.sessionComboBox = new ComboBox();
            this.sessionComboBox.Location = new System.Drawing.Point(150, 100);
            this.sessionComboBox.Size = new System.Drawing.Size(200, 30);
            this.Controls.Add(this.sessionComboBox);

            // View Attendance Button
            this.viewAttendanceButton = new Button();
            this.viewAttendanceButton.Text = "View Attendance";
            this.viewAttendanceButton.Location = new System.Drawing.Point(150, 150);
            this.viewAttendanceButton.Click += new EventHandler(this.ViewAttendanceButton_Click);
            this.Controls.Add(this.viewAttendanceButton);

            // Attendance DataGridView
            this.attendanceDataGridView = new DataGridView();
            this.attendanceDataGridView.Location = new System.Drawing.Point(50, 200);
            this.attendanceDataGridView.Size = new System.Drawing.Size(700, 350);
            this.attendanceDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(this.attendanceDataGridView);

            // Context Menu for DataGridView
            this.attendanceContextMenu = new ContextMenuStrip();
            this.attendanceContextMenu.Items.Add("Change Status", null, this.ChangeStatus_Click);

            // Attach the context menu to the DataGridView
            this.attendanceDataGridView.ContextMenuStrip = this.attendanceContextMenu;

            // Load classes into the classComboBox
            LoadClasses();
        }

        private void LoadClasses()
        {
            // Simulate loading classes (replace with actual data fetching logic)
            var classes = new List<string> { "Math 101", "Physics 201", "Chemistry 301" };
            this.classComboBox.Items.AddRange(classes.ToArray());
        }

        private void ClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Simulate loading sessions for the selected class (replace with actual data fetching logic)
            this.sessionComboBox.Items.Clear();
            if (this.classComboBox.SelectedItem != null)
            {
                var selectedClass = this.classComboBox.SelectedItem.ToString();
                var sessions = new List<string>();

                if (selectedClass == "Math 101")
                    sessions = new List<string> { "Session 1", "Session 2", "Session 3" };
                else if (selectedClass == "Physics 201")
                    sessions = new List<string> { "Session A", "Session B" };
                else if (selectedClass == "Chemistry 301")
                    sessions = new List<string> { "Session X", "Session Y", "Session Z" };

                this.sessionComboBox.Items.AddRange(sessions.ToArray());
            }
        }

        private void ApplyFilterButton_Click(object sender, EventArgs e)
        {
            if (this.filterComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a filter criteria.");
                return;
            }
        
            string selectedFilter = this.filterComboBox.SelectedItem.ToString();
            string filterValue = this.filterTextBox.Text;
        
            // Get the current data source (list of AttendanceRecord)
            var attendanceRecords = this.attendanceDataGridView.DataSource as List<AttendanceRecord>;
            if (attendanceRecords == null) return;
        
            // Apply the selected filter
            List<AttendanceRecord> filteredRecords = new List<AttendanceRecord>();
            if (selectedFilter == "Missed 3 classes in a row")
            {
                // Simulate filtering logic (replace with actual logic)
                filteredRecords = attendanceRecords.FindAll(record => record.TotalAbsences >= 3);
            }
            else if (selectedFilter == "More than X absences")
            {
                if (int.TryParse(filterValue, out int absencesThreshold))
                {
                    filteredRecords = attendanceRecords.FindAll(record => record.TotalAbsences > absencesThreshold);
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for absences.");
                    return;
                }
            }
        
            // Update the DataGridView with the filtered records
            this.attendanceDataGridView.DataSource = filteredRecords;
        }

        private void ViewAttendanceButton_Click(object sender, EventArgs e)
        {
            // Validate that both class and session are selected
            if (this.classComboBox.SelectedItem == null || this.sessionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both a class and a class session.");
                return;
            }

            // Simulate fetching attendance records (replace with actual data fetching logic)
            var attendanceRecords = new List<AttendanceRecord>
            {
                new AttendanceRecord { StudentId = "12345", Name = "John Doe", Status = "Present" },
                new AttendanceRecord { StudentId = "67890", Name = "Jane Smith", Status = "Absent" }
            };

            // Update TotalAbsences based on the initial Status
            foreach (var record in attendanceRecords)
            {
                if (record.Status == "Absent")
                {
                    record.TotalAbsences++; // Increment TotalAbsences if the student is absent
                }
            }

            // Bind the attendance records to the DataGridView
            this.attendanceDataGridView.DataSource = attendanceRecords;
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


        // Simulated attendance record class (replace with actual model class!!)
        private class AttendanceRecord
        {
            public string StudentId { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public int TotalAbsences { get; set; }
        }
    }
}