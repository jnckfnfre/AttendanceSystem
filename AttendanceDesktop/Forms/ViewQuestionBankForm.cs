//Maha Shaikh 4/24/2025

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

namespace AttendanceDesktop
{
    public partial class ViewQuestionBankForm : Form
    {
        public ViewQuestionBankForm()
        {
            InitializeComponent();
            Load += ViewQuestionBankForm_Load;
        }

        private async void ViewQuestionBankForm_Load(object sender, EventArgs e)
        {
            // Setup UI Columns (always show headers)
            questionBankGrid.Columns.Clear();
            questionBankGrid.Columns.Add("QuestionId", "Question ID");
            questionBankGrid.Columns.Add("Text", "Text");
            questionBankGrid.Columns.Add("PoolName", "Pool Name");
            questionBankGrid.Columns.Add("CreatedAt", "Created At");

            // Try loading API data
            await LoadDataFromApiAsync();
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
                    var questions = JsonSerializer.Deserialize<List<QuestionBankViewDto>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Fill DataGridView
                    foreach (var q in questions)
                    {
                        questionBankGrid.Rows.Add(q.QuestionId, q.Text, q.PoolName, q.CreatedAt.ToString("g"));
                    }
                }
                catch
                {
                    MessageBox.Show(
                        "Failed to connect to the API. Table will remain empty.\nMake sure the API is running on http://localhost:5257.",
                        "Connection Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
        }

        // DTO used to deserialize API response
        private class QuestionBankViewDto
        {
            public int QuestionId { get; set; }
            public string Text { get; set; }
            public string PoolName { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
