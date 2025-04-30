/*
    David Sajdak 4/30/2025
    Form where professor can manually create new QB
*/

namespace AttendanceDesktop;


public partial class CreateQuestionBankForm : Form
{
    private string poolName;

    public CreateQuestionBankForm(string poolName)
    {
        InitializeComponent();
        this.poolName = poolName; // getting pool name from text box in new question bank form
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

    // posts each new question to database on click
    private void SaveAllButton_Click(object sender, EventArgs e)
    {
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

                // Validate and insert into database using stored poolId
            }
        }

        MessageBox.Show("All questions saved successfully.");
    }

}
