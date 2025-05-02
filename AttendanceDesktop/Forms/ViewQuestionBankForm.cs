// Maha Shaikh 4/24/2025
// Maha Shaikh 4/30/2025

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttendanceDesktop
{
    public partial class ViewQuestionBankForm : Form
    {
        private readonly Color primaryColor = Color.FromArgb(199, 91, 18);  // Orange
        private readonly Color secondaryColor = Color.FromArgb(0, 133, 66); // Green
        private readonly Color lightGrayColor = Color.FromArgb(240, 240, 240);
        private readonly Color darkTextColor = Color.FromArgb(50, 50, 50);

        private bool isLoading = false;
        private string statusMessage = "";

        private List<QuestionBankViewDto> allQuestions = new List<QuestionBankViewDto>();
        private List<ClassSection> classSections = new List<ClassSection>();

        public ViewQuestionBankForm()
        {
            InitializeComponent();
            Load += ViewQuestionBankForm_Load;
        }

        private async void ViewQuestionBankForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            UpdateStatus("Loading class sections...", true);
            await LoadClassSectionsAsync();
            UpdateStatus("Loading question bank data...", true);
            await LoadDataFromApiAsync();
            UpdateStatus("Ready", false);
        }

        private void SetupDataGridView()
        {
            questionBankGrid.Columns.Clear();
            questionBankGrid.Columns.Add("QuestionId", "Question ID");
            questionBankGrid.Columns.Add("Text", "Question Text");
            questionBankGrid.Columns.Add("PoolName", "Pool Name");
            questionBankGrid.Columns.Add("CourseId", "Course ID");

            questionBankGrid.Columns["QuestionId"].Width = 100;
            questionBankGrid.Columns["Text"].Width = 350;
            questionBankGrid.Columns["PoolName"].Width = 150;
            questionBankGrid.Columns["CourseId"].Width = 120;

            questionBankGrid.EnableHeadersVisualStyles = false;
            questionBankGrid.ColumnHeadersDefaultCellStyle.BackColor = secondaryColor;
            questionBankGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            questionBankGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            questionBankGrid.ColumnHeadersHeight = 40;

            questionBankGrid.RowsDefaultCellStyle.BackColor = Color.White;
            questionBankGrid.AlternatingRowsDefaultCellStyle.BackColor = lightGrayColor;
            questionBankGrid.RowsDefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            questionBankGrid.RowsDefaultCellStyle.ForeColor = darkTextColor;
            questionBankGrid.RowTemplate.Height = 35;

            questionBankGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 235, 224);
            questionBankGrid.DefaultCellStyle.SelectionForeColor = darkTextColor;

            questionBankGrid.BorderStyle = BorderStyle.None;
            questionBankGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            questionBankGrid.GridColor = Color.FromArgb(224, 224, 224);
        }

        private async Task LoadClassSectionsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "http://localhost:5257/api/courses";
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);

                    classSections = doc.RootElement.EnumerateArray()
                        .Select(element => new ClassSection
                        {
                            CourseId = element.GetProperty("COURSE_ID").GetString(),
                            CourseName = element.GetProperty("COURSE_NAME").GetString()
                        })
                        .ToList();

                    classSections.Insert(0, new ClassSection { CourseId = "all", CourseName = "All Sections" });

                    classFilterComboBox.Items.Clear();
                    foreach (var section in classSections)
                    {
                        classFilterComboBox.Items.Add(section);
                    }

                    classFilterComboBox.DisplayMember = "CourseName";
                    classFilterComboBox.ValueMember = "CourseId";
                    classFilterComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading class sections: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDataFromApiAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "http://localhost:5257/api/questions/view-question-banks";
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    allQuestions = JsonSerializer.Deserialize<List<QuestionBankViewDto>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    ApplyFilters();
                    UpdateStatus($"Loaded {allQuestions.Count} questions successfully", false);
                }
                catch (Exception ex)
                {
                    UpdateStatus("Connection Error", false);
                    MessageBox.Show(
                        "Failed to connect to the API. Table will remain empty.\nMake sure the API is running on http://localhost:5257.",
                        "Connection Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
        }

        private void ApplyFilters()
        {
            string selectedCourseId = "all";
            if (classFilterComboBox.SelectedItem is ClassSection selectedSection)
            {
                selectedCourseId = selectedSection.CourseId;
            }

            string searchTerm = searchTextBox.Text.Trim().ToLower();
            var filteredQuestions = allQuestions;

            if (selectedCourseId != "all")
            {
                filteredQuestions = filteredQuestions
                    .Where(q => q.CourseId == selectedCourseId)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredQuestions = filteredQuestions
                    .Where(q =>
                        q.Text.ToLower().Contains(searchTerm) ||
                        q.PoolName.ToLower().Contains(searchTerm))
                    .ToList();
            }

            questionBankGrid.Rows.Clear();
            foreach (var q in filteredQuestions)
            {
                questionBankGrid.Rows.Add(q.QuestionId, q.Text, q.PoolName, q.CourseId);
            }

            UpdateStatus($"Showing {filteredQuestions.Count} of {allQuestions.Count} questions", false);
        }

        private void UpdateStatus(string message, bool loading)
        {
            statusMessage = message;
            isLoading = loading;
            statusLabel.Text = message;
            statusLabel.ForeColor = loading ? primaryColor : secondaryColor;
            refreshButton.Enabled = !loading;
            searchButton.Enabled = !loading;
            classFilterComboBox.Enabled = !loading;
            loadingPictureBox.Visible = loading;
            this.Refresh();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            UpdateStatus("Refreshing question bank data...", true);
            await LoadDataFromApiAsync();
        }

        private void classFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private class QuestionBankViewDto
        {
            public int QuestionId { get; set; }
            public string Text { get; set; }
            public string PoolName { get; set; }
            public string CourseId { get; set; }
        }

        private class ClassSection
        {
            public string CourseId { get; set; }
            public string CourseName { get; set; }

            public override string ToString()
            {
                return CourseName;
            }
        }
    }
}
