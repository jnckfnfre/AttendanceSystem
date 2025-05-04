/*
    David Sajdak 4/21/2025
    Form for professor to see things like
    class session password, courses, quiz question banks,
    and configuring things like quiz question banks as well
*/

namespace AttendanceDesktop;


public partial class ConfigurationForm : Form
{
    public ConfigurationForm()
    {
        InitializeComponent();
    }

    /*
        David Sajdak 4/21/2025
        Hanles click of congifuration table button,
        Directs to configuration table form
    */
    private void configTableButton_Click(object sender, EventArgs args) {
        // Open configuration form
        ConfigurationTableForm configTableForm = new ConfigurationTableForm();
        configTableForm.Show();
    }

    // Eduardo Zamora 4/22/2025
    // Handles click of database info button,
    // Directs to database info form
    private void databaseInfo_Click(object sender, EventArgs e)
    {
        // Create and show the database info form
        databaseInfoForm infoForm = new databaseInfoForm();
        infoForm.ShowDialog(); // ShowDialog makes it modal (user must close it before returning to main form)
    }

    /*
        David Sajdak 4/23/2025
        Handles click of new question bank button,
        Directs to new question bank form
    */
    private void newQuestionBankButton_Click(object sender, EventArgs args) {
        // open new question bank form
        NewQuestionBankForm newQBForm = new NewQuestionBankForm();
        newQBForm.Show();
    }

    /*
        Maha Shaikh 4/24/2025

    */
    private void viewQuestionBankButton_Click(object sender, EventArgs e)
    {
    ViewQuestionBankForm viewQBForm = new ViewQuestionBankForm();
    viewQBForm.Show();
    }

    // Eduardo Zamora 4/26/2025
    // Handles click of create quiz questions button,
    // Directs to create quiz questions form
    private void createQuizQuestionsButton_Click(object sender, EventArgs args) {
        // open new question bank form
        createQuizQuestionsForm createQZForm = new createQuizQuestionsForm();
        createQZForm.Show();
    }

    // David Sajdak 5/4/2025
    // Handles click of add courses button,
    // Directs to form for professor to add courses to database
    private void addCoursesButton_Click(object sender, EventArgs args) {
        // Open configuration form
        AddCoursesForm addCoursesForm = new AddCoursesForm();
        addCoursesForm.Show();
    }
}