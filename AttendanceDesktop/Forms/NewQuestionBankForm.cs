/*
    David Sajdak 4/21/2025
    Form where professor can choose to create new question bank
*/

namespace AttendanceDesktop;


public partial class NewQuestionBankForm : Form
{
    public NewQuestionBankForm()
    {
        InitializeComponent();
    }

    /*
        David Sajdak 4/30/2025
        Handles click of create question bank button,
        Directs to new create question bank form
    */
    private void createQuestionBankButton_Click(object sender, EventArgs args) {
        // open create question bank form only if text box filled
        string poolName = poolNameTextBox.Text.Trim();

        if (string.IsNullOrEmpty(poolName))
        {
            MessageBox.Show("Please enter a question pool name before continuing.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        CreateQuestionBankForm createQBForm = new CreateQuestionBankForm(poolName);
        createQBForm.Show();
    }
}
