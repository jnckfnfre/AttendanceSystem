namespace AttendanceDesktop
{
    public partial class databaseInfoForm : Form
    {
        public databaseInfoForm()
        {
            InitializeComponent();
            LoadDatabaseInfo();
        }

        private void LoadDatabaseInfo()
        {
            try
            {
                // This will look in the same directory as your executable
                string filePath = "DatabaseInfo.txt";
                
                // For debugging - shows where it's looking
                string absolutePath = Path.GetFullPath(filePath);
                string currentDir = Directory.GetCurrentDirectory();
                
                if (File.Exists(filePath))
                {
                    infoTextBox.Text = File.ReadAllText(filePath);
                }
                else
                {
                    infoTextBox.Text = $"File not found at: {absolutePath}\n" +
                                    $"Current directory: {currentDir}\n" +
                                    $"Is file in output directory?";
                }
            }
            catch (Exception ex)
            {
                infoTextBox.Text = $"Error loading database information: {ex.Message}";
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}