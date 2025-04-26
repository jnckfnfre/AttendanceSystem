// Eduardo Zamora 4/22/2025
/// Handles click of database info button, directs to database info form
using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AttendanceDesktop
{
    public partial class databaseInfoForm : Form
    {
        public databaseInfoForm()
        {
            InitializeComponent();
            GenerateDatabaseInfo(); // add dtabase info to file
            LoadDatabaseInfo(); // Load database info into the text box
        }

        // Eduardo Zamora 4/25/2025
        // worked on writing the database info to (serve, password, etc) to the end of the file
        private void GenerateDatabaseInfo()
        {
            try
            {
                // Dynamically find path to AttendanceSystem.API/appsettings.json
                string projectRoot = Directory.GetParent(AppContext.BaseDirectory) // bin/
                                        .Parent                                  // Debug/
                                        .Parent                                  // net8.0-windows/
                                        .Parent                                  // AttendanceDesktop/
                                        .Parent                                  // AttendanceSystem/
                                        .FullName;
                                        
                // Construct the path to appsettings.json
                string configPath = Path.Combine(projectRoot, "AttendanceSystem.API", "appsettings.json");
                
                // Check if the file exists
                var config = new ConfigurationBuilder()
                    .AddJsonFile(configPath)
                    .Build();

                string connStr = config.GetConnectionString("DefaultConnection");

                var builder = new MySqlConnectionStringBuilder(connStr);
                string server = builder.Server;
                string port = builder.Port.ToString();
                string database = builder.Database;
                string user = builder.UserID;
                string password = builder.Password;

                // Note: Password is not stored in the connection string for security reasons
                string output = $"\n\nDATABASE INFO:\n" +
                                $"===========\n" +
                                $"Server: {server}\n" +
                                $"Port: {port}\n" +
                                $"Database Name: {database}\n" +
                                $"User ID: {user}\n" +
                                $"Password: {password}: Your Password for Local Connection in Mysql Workbench\n";

                string filePath = "DatabaseInfo.txt";

                // Only write if "DATABASE INFO" is not already in the file
                if (!File.Exists(filePath) || !File.ReadAllText(filePath).Contains("DATABASE INFO:"))
                {
                    File.AppendAllText(filePath, output);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating database info: {ex.Message}");
            }
        }

        private void LoadDatabaseInfo()
        {
            try
            {
                // This will look in the same directory as your executable
                string filePath = "DatabaseInfo.txt";
                // for debugging purposes, get the absolute path
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
        // button click event to close the form
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}