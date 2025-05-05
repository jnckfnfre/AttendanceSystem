using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AttendanceDesktop;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    // Eduardo Zamora 4/10/2025
    // Button to view attendance
    private void viewAttendanceButton_Click(object sender, EventArgs e)
    {
        // Open the new form
        ViewAttendanceForm attendanceForm = new ViewAttendanceForm();
        attendanceForm.Show();
    }

    /*
        David Sajdak 4/21/2025
        Hanles click of congifuration button,
        Directs to configuration form
    */
    private void configButton_Click(object sender, EventArgs args) {
        // Open configuration form
        ConfigurationForm configForm = new ConfigurationForm();
        configForm.Show();
    }
}
