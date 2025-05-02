/*
    David Sajdak 4/30/2025
    Form where professor can manually create new QB
*/
using System.Text;
using System.Text.Json;

namespace AttendanceDesktop;


public partial class CreateQuestionBankForm : Form
{
    private string poolName;
    private string courseID;

    public CreateQuestionBankForm(string poolName, string courseID)
    {
        InitializeComponent();
        this.poolName = poolName; // getting pool name from text box in new question bank form
        this.courseID = courseID?.Split(' ')[0]; // getting course id from dropdown in new question bank form, only want id part of string passed not name
        MessageBox.Show(this.courseID);
        AddQuestionControl(); // show one question to fill on initalization
    }

    // calls helper function on click
    private void AddQuestionButton_Click(object sender, EventArgs e)
    {
        AddQuestionControl();
    }

    // adds question
    private void AddQuestionControl()
    {
        var questionEntry = new QuestionEntryControl();
        questionPanel.Controls.Add(questionEntry);
    }

    // creates new question pool in database and then
    // posts each new question (for that question pool) to database on click
    private async void SaveAllButton_Click(object sender, EventArgs e)
    {
        try
        {
            string apiBase = "http://localhost:5257/api";

            using (HttpClient client = new HttpClient())
            {
                // Create Question Pool
                var poolPayload = new
                {
                    PoolName = this.poolName,
                    Course_Id = this.courseID
                };

                // serialize question pool
                var poolContent = new StringContent(
                    JsonSerializer.Serialize(poolPayload),
                    Encoding.UTF8,
                    "application/json"
                );

                // post question pool
                HttpResponseMessage poolResponse = await client.PostAsync($"{apiBase}/questionpool", poolContent);

                if (!poolResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Failed to create question pool.");
                    return;
                }

                // Get pool id from response
                string poolJson = await poolResponse.Content.ReadAsStringAsync();
                int poolId = JsonDocument.Parse(poolJson).RootElement.GetProperty("poolId").GetInt32();

                // collect questions from input
                var questions = new List<object>();

                foreach (var control in questionPanel.Controls)
                {
                    if (control is QuestionEntryControl q)
                    {
                        string text = q.QuestionTextBox.Text.Trim();
                        string a = q.OptionATextBox.Text.Trim();
                        string b = q.OptionBTextBox.Text.Trim();
                        string c = q.OptionCTextBox.Text.Trim();
                        string d = q.OptionDTextBox.Text.Trim();
                        string correct = q.CorrectAnswerComboBox.SelectedItem?.ToString();

                        // validate required fields filled
                        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b) || string.IsNullOrEmpty(correct))
                        {
                            MessageBox.Show("Each question must have text, Option A, Option B, and a correct answer.");
                            continue;
                        }

                        // add question to list
                        questions.Add(new
                        {
                            Text = text,
                            OptionA = a,
                            OptionB = b,
                            OptionC = string.IsNullOrWhiteSpace(c) ? null : c,
                            OptionD = string.IsNullOrWhiteSpace(d) ? null : d,
                            CorrectAnswer = correct,
                            PoolId = poolId
                        });
                    }
                }

                // serialize questions
                var questionsContent = new StringContent(
                    JsonSerializer.Serialize(questions),
                    Encoding.UTF8,
                    "application/json"
                );

                // batch upload questions
                HttpResponseMessage questionsResponse = await client.PostAsync($"{apiBase}/questions/batch-upload", questionsContent);

                if (!questionsResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Some or all questions failed to save.");
                    return;
                }

                // verify
                MessageBox.Show("Question pool and all questions saved successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving question pool: {ex.Message}");
        }
    }

}
