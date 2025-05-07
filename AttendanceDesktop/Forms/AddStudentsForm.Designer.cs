/*
    David Sajdak 5/5/2025
    Designer for add students form
 */

namespace AttendanceDesktop;

partial class AddStudentsForm
{
    private System.ComponentModel.IContainer components = null;
    private Button uploadButton;
    private Label courseLabel;
    private System.Windows.Forms.ComboBox courseDropdown;

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
        // general form setting
        this.WindowState = FormWindowState.Maximized;
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.Text = "Add Students";
        this.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.Sizable;

        // title message
        Label titleLabel = new Label();
        titleLabel.Text = "Upload Student Roster";
        titleLabel.Font = new Font("Segoe UI", 25, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(0, 133, 66);
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        titleLabel.AutoSize = true;
        titleLabel.Anchor = AnchorStyles.None;

        // // Label for course
        courseLabel = new Label();
        courseLabel.Text = "Choose Course:";
        courseLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        courseLabel.AutoSize = true;
        courseLabel.TextAlign = ContentAlignment.MiddleCenter;
        courseLabel.Anchor = AnchorStyles.None;

        // // Course Dropwdown
        this.courseDropdown = new ComboBox();
        this.courseDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.courseDropdown.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.courseDropdown.FormattingEnabled = true;
        this.courseDropdown.Location = new System.Drawing.Point(150, 45);
        this.courseDropdown.Name = "courseDropdown";
        this.courseDropdown.Size = new System.Drawing.Size(300, 25);
        this.courseDropdown.Width = 500;
        this.courseDropdown.TabIndex = 1;
        this.courseDropdown.Anchor = AnchorStyles.None;
        this.courseDropdown.SelectedIndexChanged += new EventHandler(this.courseDropdown_SelectedIndexChanged);

        // button for professor to add students
        this.uploadButton = new Button();
        this.uploadButton.Text = "Upload CSV";
        this.uploadButton.Size = new Size(300, 100);
        this.uploadButton.BackColor = Color.FromArgb(199, 91, 18); // orange
        this.uploadButton.ForeColor = Color.White;
        this.uploadButton.FlatStyle = FlatStyle.Flat;
        this.uploadButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.uploadButton.FlatAppearance.BorderSize = 0;
        this.uploadButton.Click += new EventHandler(this.uploadButton_Click);

        // // helps with alignment of buttons
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
        buttonPanel.FlowDirection = FlowDirection.LeftToRight; // want buttons side by side
        buttonPanel.AutoSize = true;
        buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        buttonPanel.Anchor = AnchorStyles.None;
        buttonPanel.Controls.Add(uploadButton);

        // // aligns everything
        TableLayoutPanel contentLayout = new TableLayoutPanel();
        contentLayout.AutoSize = true;
        contentLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        contentLayout.Anchor = AnchorStyles.None;
        contentLayout.Dock = DockStyle.None;
        contentLayout.ColumnCount = 1;
        contentLayout.RowCount = 4;
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for title
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for course label
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for course entry
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for buttons
        contentLayout.Controls.Add(titleLabel, 0, 0);
        contentLayout.Controls.Add(courseLabel, 0, 1);
        contentLayout.Controls.Add(this.courseDropdown, 0, 2);
        contentLayout.Controls.Add(buttonPanel, 0, 3);

        // // wrapper
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
    }

    #endregion
}