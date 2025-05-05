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
    private async void SaveAllButton_Click(object sender, EventArgs e)
    {
        try
        {
            // endpoint url
            string apiUrl = "http://localhost:5257/api/Courses/batch-upload";

            var courses = new List<object>();

            // gather course info from input
            foreach (var control in coursePanel.Controls)
            {
                if (control is CourseEntryControl c)
                {
                    string courseId = c.CourseIdTextBox.Text.Trim();
                    string courseName = c.CourseNameTextBox.Text.Trim();
                    string startTime = c.StartTimePicker.Value.ToString("HH:mm:ss");
                    string endTime = c.EndTimePicker.Value.ToString("HH:mm:ss");

                    // validate
                    if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(courseName))
                    {
                        MessageBox.Show("Each course must have an ID, name, and times set.");
                        continue;
                    }

                    // add course to list
                    courses.Add(new
                    {
                        Course_Id = courseId,
                        Course_Name = courseName,
                        Start_Time = startTime,
                        End_Time = endTime
                    });
                }
            }

            // serialize list of courses
            var content = new StringContent(
                JsonSerializer.Serialize(courses),
                Encoding.UTF8,
                "application/json"
            );

            using (HttpClient client = new HttpClient())
            {
                // post to database
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Some or all courses failed to save. Status code: {response}");
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
