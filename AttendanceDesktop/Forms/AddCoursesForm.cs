/* 
    David Sajdak 5/4/2024
    Form for professor to add all his courses to database
*/
using System.Text;
using System.Text.Json;
using System.Net;

namespace AttendanceDesktop;


public partial class AddCoursesForm : Form
{
    public AddCoursesForm()
    {
        InitializeComponent();
        AddCourseControl(); // initialize with one course to fill
    }

    // calls helper function on click
    private void AddCourseButton_Click(object sender, EventArgs e)
    {
        AddCourseControl();
    }

    // adds coure
    private void AddCourseControl()
    {
        var courseEntry = new CourseEntryControl();
        coursePanel.Controls.Add(courseEntry);
    }

    // saves course to database
    // NOT DONE
    private async void SaveAllButton_Click(object sender, EventArgs e)
    {
        try
        {
            string apiBase = "http://localhost:5257/api";

            var courses = new List<object>();

            foreach (var control in coursePanel.Controls)
            {
                if (control is CourseEntryControl c)
                {
                    string courseId = c.CourseIdTextBox.Text.Trim();
                    string courseName = c.CourseNameTextBox.Text.Trim();
                    string startTime = c.StartTimePicker.Value.ToString("HH:mm:ss");
                    string endTime = c.EndTimePicker.Value.ToString("HH:mm:ss");

                    if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(courseName))
                    {
                        MessageBox.Show("Each course must have an ID and name.");
                        continue;
                    }

                    courses.Add(new
                    {
                        CourseId = courseId,
                        CourseName = courseName,
                        StartTime = startTime,
                        EndTime = endTime
                    });
                }
            }

            var content = new StringContent(
                JsonSerializer.Serialize(courses),
                Encoding.UTF8,
                "application/json"
            );

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"{apiBase}/courses/batch", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Some or all courses failed to save.");
                    return;
                }

                MessageBox.Show("Courses saved successfully.");
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving courses: {ex.Message}");
        }
    }
}
