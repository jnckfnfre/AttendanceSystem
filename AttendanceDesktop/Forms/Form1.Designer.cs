namespace AttendanceDesktop;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Button viewAttendanceButton;
    private Button configButton;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

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
        titleLabel.ForeColor = Color.FromArgb(0, 133, 66);
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        titleLabel.AutoSize = true;
        titleLabel.Anchor = AnchorStyles.None;

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
        this.viewAttendanceButton.FlatAppearance.BorderSize = 1;
        this.viewAttendanceButton.Click += new EventHandler(this.viewAttendanceButton_Click);

        /* 
            David Sajdak 4/21/2025
            Configuration button for database info and actions,
            classes, and more misc info
        */
        this.configButton = new Button();
        this.configButton.Text = "Configuration";
        this.configButton.Size = new Size(300, 90);
        this.configButton.BackColor = Color.FromArgb(199, 91, 18);
        this.configButton.ForeColor = Color.White;
        this.configButton.FlatStyle = FlatStyle.Flat;
        this.configButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.configButton.FlatAppearance.BorderSize = 0;
        this.configButton.Click += new EventHandler(this.configButton_Click);

        /*
            David Sajdak 4/16/2025
            Horizontal panel for buttons
        */
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
        buttonPanel.FlowDirection = FlowDirection.LeftToRight; // want buttons side by side
        buttonPanel.AutoSize = true;
        buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        buttonPanel.Anchor = AnchorStyles.None;
        buttonPanel.Controls.Add(viewAttendanceButton);
        buttonPanel.Controls.Add(configButton);

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