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
}
