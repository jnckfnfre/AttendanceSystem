/* 
    David Sajdak 5/5/2025
    Form for professor to upload student rosters
*/
using System.Text;
using System.Text.Json;
using System.Net;

namespace AttendanceDesktop;


public partial class AddStudentsForm : Form
{
    private string courseId;

    public AddStudentsForm()
    {
        InitializeComponent();
        LoadCourses();
    }

    /*
        David Sajdak 5/6/205
        saves selected course id for later use
    */
    private void courseDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (courseDropdown.SelectedItem is Course selectedCourse)
        {
            this.courseId = selectedCourse.CourseId;
        }
    }

    /*
        David Sajdak Started: 04/09/2025 Moved to this form from Form.cs 5/5/2025
        Handles Upload CSV button click, opens file dialog
        and passing selected file name to CSVToMySQL function
    */
    private async void uploadButton_Click(object sender, EventArgs e) {
        using (OpenFileDialog ofd = new OpenFileDialog()) {
            // filter for csv/tsv
            ofd.Filter = "All files|*.*|CSV Files (*.csv)|*.csv|TSV Files (*.tsv)|*.tsv|TXT Files (*.txt)|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK) {
                string path = ofd.FileName; // grab file name
                CSVToMySQL(path);
            }
        }
    }

    /*
        David Sajdak Started: 04/09/2025 Moved to this form from Form.cs 5/5/2025
        Revised 5/6/2025 to include upload to CourseStudents table
        Takes file path, reads file and sends batch upload
        request to api which interacts with database to insert
        new rows to students table
    */
    private async void CSVToMySQL(string path) {
        // api endpoint
        string studentUrl = "http://localhost:5257/api/students/batch-upload"; // for posting to students table
        string courseStudentUrl = "http://localhost:5257/api/CourseStudents/batch-upload"; // for posting to CourseStudents table

        // fields don't match headers from sample file so need to map
        var headerToFieldMap = new Dictionary<string, string>
        {
            { "Student ID", "Utd_Id" },
            { "First Name", "First_Name" },
            { "Last Name", "Last_Name" },
            { "Username", "Net_Id" }
        };

        // define list of students to uplaod
        var students = new List<Dictionary<string, string>>();
        var courseLinks = new List<Dictionary<string, string>>(); // list for course student links 
        
        using (var reader = new StreamReader(path))
        {
            string headerLine = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(headerLine))
            {
                MessageBox.Show("CSV is empty.");
                return;
            }

            // tab delimited so split by tab
            var headers = headerLine.Split('\t');

            // read line by line and add each student to list
            while (!reader.EndOfStream)
            {
                // get line, but make sure not keeping empty lines at end of file
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) 
                    continue;

                var values = line.Split('\t');  // split by tab
                var student = new Dictionary<string, string>();

                // map headers
                for (int i = 0; i < headers.Length; i++)
                {
                    string csvHeader = headers[i];
                    if (headerToFieldMap.ContainsKey(csvHeader))
                    {
                        string apiField = headerToFieldMap[csvHeader];
                        student[apiField] = values[i];
                    }
                }

                // add course links to list
                if (student.ContainsKey("Utd_Id") && !string.IsNullOrEmpty(courseId))
                {
                    courseLinks.Add(new Dictionary<string, string>
                    {
                        { "Utd_Id", student["Utd_Id"] },
                        { "Course_Id", courseId }
                    });
                }

                students.Add(student);
            }
        }

        using (HttpClient client = new HttpClient())
        {
            // convert students list to json
            var json = JsonSerializer.Serialize(students);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // send request to client
            var response = await client.PostAsync(studentUrl, content);

            // check for successful response and send message
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Upload successful!");
            }
            else
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Upload failed: " + errorMsg);
            }

            // serialize list of course-student links
            var linkJson = JsonSerializer.Serialize(courseLinks);
            var linkContent = new StringContent(linkJson, Encoding.UTF8, "application/json");

            // post to database
            var linkResponse = await client.PostAsync(courseStudentUrl, linkContent);

            // validate success
            if (linkResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Students and course associations uploaded successfully.");
                this.Close();
            }
            else
            {
                string err = await linkResponse.Content.ReadAsStringAsync();
                MessageBox.Show("Students uploaded, but course-student upload failed: " + err);
            }
        }
    }

    /* 
        David Sajdak 5/5/2025
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
