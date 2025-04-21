/*
    David Sajdak 4/21/2025
    Form that displays configuration table for professor
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Data;

namespace AttendanceDesktop;

public partial class ConfigurationTableForm : Form
{
    public ConfigurationTableForm()
    {
        InitializeComponent();
        this.Load += load_data; // loads data immediately after form loaded
    }

    // dynamically load data into data grid view
    // as soon as form opened
    private async void load_data(object sender, EventArgs e)
    {
        // Load data
        await getConfigData();
        
    }

    // get data needed for configuration table
    private async Task getConfigData() {
        using (HttpClient client = new HttpClient()) {
            try {
                // use endpoint from class session controller in api to grab data
                string apiUrl = "http://localhost:5257/api/ClassSession/WithConfigData";
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // get json string
                string json = await response.Content.ReadAsStringAsync();
                
                // parse json
                var doc = JsonDocument.Parse(json);
                var rows = doc.RootElement.EnumerateArray()
                    .Select(e => new
                    {
                        Course_Id = e.GetProperty("course_Id").GetString(),
                        Course_Name = e.GetProperty("course_Name").GetString(),
                        SessionDate = e.GetProperty("sessionDate").GetDateTime(),
                        Start_Time = TimeSpan.Parse(e.GetProperty("start_Time").GetString()),
                        End_Time = TimeSpan.Parse(e.GetProperty("end_Time").GetString()),
                        Password = e.GetProperty("password").GetString(),
                        DueDate = e.TryGetProperty("dueDate", out var due) ? due.GetDateTime() : (DateTime?)null,
                        PoolId = e.GetProperty("poolId").GetInt32()
                    })
                    .ToList();

                // add rows to table
                configTableGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching sessions: " + ex.Message);
            }
        }
    }
}