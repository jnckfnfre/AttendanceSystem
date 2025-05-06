/*
    David Sajdak 5/4/2025
    Designer for add course form
*/

/*
    David Sajdak 4/21/2025
    Designer for Configuration form
 */

namespace AttendanceDesktop;

partial class AddCoursesForm
{
    private System.ComponentModel.IContainer components = null;
    private FlowLayoutPanel coursePanel;
    private Button addCourseButton;
    private Button saveAllButton;

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
        this.Text = "Add Courses";
        this.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.Sizable;

        // title label
        Label titleLabel = new Label();
        titleLabel.Text = "Add Courses";
        titleLabel.Font = new Font("Segoe UI", 25, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(0, 133, 66);
        titleLabel.AutoSize = true;
        titleLabel.Anchor = AnchorStyles.None;

        // coursePanel
        this.coursePanel = new FlowLayoutPanel();
        this.coursePanel.Dock = DockStyle.Top;
        this.coursePanel.FlowDirection = FlowDirection.TopDown;
        this.coursePanel.AutoScroll = true;
        this.coursePanel.WrapContents = false;
        this.coursePanel.AutoSize = true;

        // addCourseButton
        this.addCourseButton = new Button();
        this.addCourseButton.Text = "+ Add Course";
        this.addCourseButton.BackColor = Color.SteelBlue;
        this.addCourseButton.ForeColor = Color.White;
        this.addCourseButton.Width = 200;
        this.addCourseButton.Height = 50;
        this.addCourseButton.Click += new EventHandler(this.AddCourseButton_Click);

        // saveAllButton
        this.saveAllButton = new Button();
        this.saveAllButton.Text = "Save All";
        this.saveAllButton.BackColor = Color.FromArgb(0, 133, 66);
        this.saveAllButton.ForeColor = Color.White;
        this.saveAllButton.Width = 200;
        this.saveAllButton.Height = 50;
        this.saveAllButton.Click += new EventHandler(this.SaveAllButton_Click);

        // button row
        FlowLayoutPanel buttonRow = new FlowLayoutPanel();
        buttonRow.Dock = DockStyle.Top;
        buttonRow.AutoSize = true;
        buttonRow.Controls.Add(this.addCourseButton);
        buttonRow.Controls.Add(this.saveAllButton);

        // main layout
        FlowLayoutPanel mainLayout = new FlowLayoutPanel();
        mainLayout.Dock = DockStyle.Fill;
        mainLayout.FlowDirection = FlowDirection.TopDown;
        mainLayout.WrapContents = false;
        mainLayout.AutoScroll = true;
        mainLayout.Controls.Add(titleLabel);
        mainLayout.Controls.Add(this.coursePanel);
        mainLayout.Controls.Add(buttonRow);

        this.Controls.Add(mainLayout);
    }

    #endregion
}
