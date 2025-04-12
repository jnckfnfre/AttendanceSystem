namespace AttendanceDesktop;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private Button uploadButton;
    private Button viewAttendanceButton;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Main Page";

        /*
            David Sajdak 4/9/2025
            button to upload professor csv
        */
        this.uploadButton = new Button();
        this.uploadButton.Text = "Upload CSV";
        this.uploadButton.Location = new Point(200, 200);
        this.uploadButton.Size = new Size(150, 50);
        this.uploadButton.Click += new EventHandler(this.uploadButton_Click);
        this.Controls.Add(this.uploadButton);

        /* 
            Eduardo Zamora 4/10/2025
            Button to view attendance
        */
        this.viewAttendanceButton = new Button();
        this.viewAttendanceButton.Text = "View Attendance";
        this.viewAttendanceButton.Location = new Point(200, 250);
        this.viewAttendanceButton.Size = new Size(150, 30);
        this.viewAttendanceButton.Click += new EventHandler(this.viewAttendanceButton_Click);
        this.Controls.Add(this.viewAttendanceButton);
    }

    #endregion
}
