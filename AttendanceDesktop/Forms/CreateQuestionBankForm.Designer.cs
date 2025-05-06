/*
    David Sajdak 4/30/2025
    Designer for create question bank form
 */

namespace AttendanceDesktop;

partial class CreateQuestionBankForm
{
    private System.ComponentModel.IContainer components = null;
    private FlowLayoutPanel questionPanel;
    private Button addQuestionButton;
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
        this.Text = "Create Question Bank";
        this.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.Sizable;

        // title label
        Label titleLabel = new Label();
        titleLabel.Text = "Add Questions";
        titleLabel.Font = new Font("Segoe UI", 25, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(0, 133, 66);
        titleLabel.AutoSize = true;
        titleLabel.Anchor = AnchorStyles.None;

        // questionPanel setup
        this.questionPanel = new FlowLayoutPanel();
        this.questionPanel.Dock = DockStyle.Top;
        this.questionPanel.FlowDirection = FlowDirection.TopDown;
        this.questionPanel.AutoScroll = true;
        this.questionPanel.WrapContents = false;
        this.questionPanel.AutoSize = true;

        // addQuestionButton
        this.addQuestionButton = new Button();
        this.addQuestionButton.Text = "+ Add Question";
        this.addQuestionButton.BackColor = Color.SteelBlue;
        this.addQuestionButton.ForeColor = Color.White;
        this.addQuestionButton.Width = 200;
        this.addQuestionButton.Height = 50;
        this.addQuestionButton.Click += new EventHandler(this.AddQuestionButton_Click);

        // saveAllButton
        this.saveAllButton = new Button();
        this.saveAllButton.Text = "Save All";
        this.saveAllButton.BackColor = Color.FromArgb(0, 133, 66);
        this.saveAllButton.ForeColor = Color.White;
        this.saveAllButton.Width = 200;
        this.saveAllButton.Height = 50;
        this.saveAllButton.Click += new EventHandler(this.SaveAllButton_Click);

        // buttonRow setup
        FlowLayoutPanel buttonRow = new FlowLayoutPanel();
        buttonRow.Dock = DockStyle.Top;
        buttonRow.AutoSize = true;
        buttonRow.Controls.Add(this.addQuestionButton);
        buttonRow.Controls.Add(this.saveAllButton);

        // mainLayout setup
        FlowLayoutPanel mainLayout = new FlowLayoutPanel();
        mainLayout.Dock = DockStyle.Fill;
        mainLayout.FlowDirection = FlowDirection.TopDown;
        mainLayout.WrapContents = false;
        mainLayout.AutoScroll = true;
        mainLayout.Controls.Add(titleLabel);
        mainLayout.Controls.Add(this.questionPanel);
        mainLayout.Controls.Add(buttonRow);

        // add to form
        this.Controls.Add(mainLayout);

    }

    #endregion
}