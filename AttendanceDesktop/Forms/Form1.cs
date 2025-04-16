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

    /*
        David Sajdak Started: 04/09/2025
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

    // Eduardo Zamora 4/10/2025
    // Button to view attendance
    private void viewAttendanceButton_Click(object sender, EventArgs e)
    {
        // Open the new form
        ViewAttendanceForm attendanceForm = new ViewAttendanceForm();
        attendanceForm.Show();
    }

    /*
        David Sajdak Started: 04/09/2025
        Takes file path, reads file and sends batch upload
        request to api which interacts with database to insert
        new rows to students table
    */
    private async void CSVToMySQL(string path) {
        // api endpoint
        string apiUrl = "http://localhost:5257/api/students/batch-upload";

        // fields don't match headers from sample file so need to map
        var headerToFieldMap = new Dictionary<string, string>
        {
            { "Student ID", "utdId" },
            { "First Name", "firstName" },
            { "Last Name", "lastName" },
            { "Username", "Net_Id" }
        };

        // define list of students to uplaod
        var students = new List<Dictionary<string, string>>();
        
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

                students.Add(student);
            }
        }

        using (HttpClient client = new HttpClient())
        {
            // convert students list to json
            var json = JsonSerializer.Serialize(students);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Log the JSON before sending, for debugging only
            MessageBox.Show("Sending JSON data: " + json);

            // send request to client
            var response = await client.PostAsync(apiUrl, content);

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
        }
    }
}
