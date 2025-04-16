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
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.Sizable;
        this.WindowState = FormWindowState.Maximized;

        /*
            David Sajdak 4/16/2025
            Title formatting
        */
        Label titleLabel = new Label();
        titleLabel.Text = "Attendance Manager";
        titleLabel.Font = new Font("Segoe UI", 40, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(0, 133, 66); // green
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        titleLabel.AutoSize = true;
        titleLabel.Anchor = AnchorStyles.None;

        /*
            David Sajdak 4/9/2025
            button to upload professor csv
        */
        this.uploadButton = new Button();
        this.uploadButton.Text = "Upload CSV";
        this.uploadButton.Size = new Size(300, 90);
        this.uploadButton.BackColor = Color.FromArgb(199, 91, 18);
        this.uploadButton.ForeColor = Color.White;
        this.uploadButton.FlatStyle = FlatStyle.Flat;
        this.uploadButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.uploadButton.FlatAppearance.BorderSize = 0;
        this.uploadButton.Click += new EventHandler(this.uploadButton_Click);

        /* 
            Eduardo Zamora 4/10/2025
            Button to view attendance
        */
        this.viewAttendanceButton = new Button();
        this.viewAttendanceButton.Text = "View Attendance";
        this.viewAttendanceButton.Size = new Size(300, 90);
        this.viewAttendanceButton.BackColor = Color.FromArgb(0, 133, 66);
        this.viewAttendanceButton.ForeColor = Color.White;
        this.viewAttendanceButton.FlatStyle = FlatStyle.Flat;
        this.viewAttendanceButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.viewAttendanceButton.FlatAppearance.BorderSize = 0;
        this.viewAttendanceButton.Click += new EventHandler(this.viewAttendanceButton_Click);

        /*
            David Sajdak 4/16/2025
            Horizontal panel for buttons
        */
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
        buttonPanel.FlowDirection = FlowDirection.LeftToRight; // want buttons side by side
        buttonPanel.AutoSize = true;
        buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        buttonPanel.Anchor = AnchorStyles.None;
        buttonPanel.Controls.Add(uploadButton);
        buttonPanel.Controls.Add(viewAttendanceButton);

        /* 
            David Sajdak 4/16/2025
            Main layout to add everything
        */
        TableLayoutPanel contentLayout = new TableLayoutPanel();
        contentLayout.AutoSize = true;
        contentLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        contentLayout.Anchor = AnchorStyles.None;
        contentLayout.Dock = DockStyle.None;
        contentLayout.ColumnCount = 1;
        contentLayout.RowCount = 2;
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for title
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for buttons
        contentLayout.Controls.Add(titleLabel, 0, 0);
        contentLayout.Controls.Add(buttonPanel, 0, 1);

        /*
            David Sajdak 4/16/2025
            Wrapper layout to truly center the content 
        */
        TableLayoutPanel outer = new TableLayoutPanel();
        outer.Dock = DockStyle.Fill;
        outer.ColumnCount = 3;
        outer.RowCount = 3;
        outer.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        outer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        outer.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        outer.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        outer.Controls.Add(contentLayout, 1, 1); // place contentLayout in the center
        this.Controls.Add(outer);

        this.Controls.Add(outer);
    }

    #endregion
}