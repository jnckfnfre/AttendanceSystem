/*
    David Sajdak 4/21/2025
    Form where professor can choose to create new question bank
*/
using System.Text.Json;

namespace AttendanceDesktop;


public partial class NewQuestionBankForm : Form
{
    public NewQuestionBankForm()
    {
        InitializeComponent();
        LoadCourses();
    }

    /*
        David Sajdak 4/30/2025
        Handles click of create question bank button,
        Directs to new create question bank form
    */
    private void createQuestionBankButton_Click(object sender, EventArgs args) {
        // open create question bank form only if text box and dropdown filled
        string poolName = poolNameTextBox.Text.Trim();
        string courseID = courseDropdown.Text.Trim();

        if (string.IsNullOrEmpty(poolName) | string.IsNullOrEmpty(courseID))
        {
            MessageBox.Show("Please enter a question pool name and course id before continuing.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        CreateQuestionBankForm createQBForm = new CreateQuestionBankForm(poolName, courseID);
        createQBForm.Show();
    }

    /* 
        David Sajdak 4/30/2025
        load courses for dropdown
    */
    private async void LoadCourses()
    {
        try
        {
            string apiUrl = "http://localhost:5257/api/courses";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);

                var courses = doc.RootElement.EnumerateArray()
                                .Select(element => new Course
                                {
                                    CourseId = element.GetProperty("course_Id").GetString(),
                                    CourseName = element.GetProperty("course_Name").GetString()
                                })
                                .ToList();

                courseDropdown.Invoke((MethodInvoker)delegate {
                    courseDropdown.Items.Clear();
                    foreach (var course in courses)
                    {
                        courseDropdown.Items.Add(course);
                    }
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error fetching courses: {ex.Message}");
        }
    }
}
