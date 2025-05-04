/*
    David Sajdak 4/21/2025
    Form where professor can choose to create new question bank
*/
using System.Text.Json;
using System.Text;
using System.Net;

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
    private async void createQuestionBankButton_Click(object sender, EventArgs args) {
        // open create question bank form only if text box and dropdown filled
        string poolName = poolNameTextBox.Text.Trim();
        string course = courseDropdown.Text.Trim();
        string courseID = course?.Split(' ')[0]; // use only id not name of course

        // validate
        if (string.IsNullOrEmpty(poolName) | string.IsNullOrEmpty(courseID))
        {
            MessageBox.Show("Please enter a question pool name and course id before continuing.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // create question pool before navigating to next page to add questions
        try {
            string apiBase = "http://localhost:5257/api";

            using (HttpClient client = new HttpClient())
            {
                // Create Question Pool
                var poolPayload = new
                {
                    PoolName = poolName,
                    Course_Id = courseID
                };

                // serialize question pool
                var poolContent = new StringContent(
                    JsonSerializer.Serialize(poolPayload),
                    Encoding.UTF8,
                    "application/json"
                );

                // post question pool
                HttpResponseMessage poolResponse = await client.PostAsync($"{apiBase}/questionpool", poolContent);

                if (poolResponse.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show("This course already has a question pool.");
                    return;
                }


                if (!poolResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Failed to create question pool.");
                    return;
                }

                // Get pool id from response
                string poolJson = await poolResponse.Content.ReadAsStringAsync();
                int poolId = JsonDocument.Parse(poolJson).RootElement.GetProperty("poolId").GetInt32();

                CreateQuestionBankForm createQBForm = new CreateQuestionBankForm(poolId);
                createQBForm.Show();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error creating question pool: {ex.Message}");
        }

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
