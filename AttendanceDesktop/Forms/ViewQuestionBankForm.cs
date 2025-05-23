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
using System.Text.Json.Serialization;

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
            questionBankGrid.Columns.Add("Option_A", "Option A");
            questionBankGrid.Columns.Add("Option_B", "Option B");
            questionBankGrid.Columns.Add("Option_C", "Option C");
            questionBankGrid.Columns.Add("Option_D", "Option D");
            questionBankGrid.Columns.Add("Correct_Answer", "Correct Answer");
            questionBankGrid.Columns.Add("PoolName", "Pool Name");
            questionBankGrid.Columns.Add("course_Id", "Course ID");

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
            questionBankGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                            course_Id = element.GetProperty("course_Id").GetString(),
                            CourseName = element.GetProperty("course_Name").GetString()
                        })
                        .ToList();

                    classSections.Insert(0, new ClassSection { course_Id = "all", CourseName = "All Courses" });

                    classFilterComboBox.Items.Clear();
                    foreach (var section in classSections)
                    {
                        classFilterComboBox.Items.Add(section);
                    }

                    // classFilterComboBox.DisplayMember = "CourseName";
                    // classFilterComboBox.ValueMember = "course_Id";
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
            string selectedcourse_Id = "all";
            if (classFilterComboBox.SelectedItem is ClassSection selectedSection)
            {
                selectedcourse_Id = selectedSection.course_Id;
            }

            string searchTerm = searchTextBox.Text.Trim().ToLower();
            var filteredQuestions = allQuestions;

            if (selectedcourse_Id != "all")
            {
                filteredQuestions = filteredQuestions
                    .Where(q => q.course_Id == selectedcourse_Id)
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
                questionBankGrid.Rows.Add(q.QuestionId, q.Text, q.Option_A, q.Option_B, q.Option_C, q.Option_D, q.Correct_Answer, q.PoolName, q.course_Id);
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
            [JsonPropertyName("question_Id")]
            public int QuestionId { get; set; }
            public string Text { get; set; }

            public string Option_A { get; set; } = string.Empty;

            public string Option_B { get; set; } = string.Empty;

            public string? Option_C { get; set; }

            public string? Option_D { get; set; }

            public string Correct_Answer { get; set; } = string.Empty;

            [JsonPropertyName("pool_Name")]
            public string PoolName { get; set; }
            public string course_Id { get; set; }
        }

        private class ClassSection
        {
            public string course_Id { get; set; }
            public string CourseName { get; set; }

            public override string ToString()
            {
                return $"{course_Id} - {CourseName}"; // Ensures the course id and name shows in the dropdown
            }
        }
    }
}
