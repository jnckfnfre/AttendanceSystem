/*
    David Sajdak 4/21/2025
    Form for professor to see things like
    class session password, courses, quiz question banks,
    and configuring things like quiz question banks as well
*/
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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