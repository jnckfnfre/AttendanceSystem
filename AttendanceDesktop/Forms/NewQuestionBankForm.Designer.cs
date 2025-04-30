/*
    David Sajdak 4/23/2025
    Designer for new 1uestion bank form
 */

namespace AttendanceDesktop;

partial class NewQuestionBankForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox poolNameTextBox;
    private Button uploadQB;
    private Button createQB;

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
        this.Text = "New Question Bank";
        this.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.Sizable;

        // title message
        Label titleLabel = new Label();
        titleLabel.Text = "Enter a New Question Pool Name";
        titleLabel.Font = new Font("Segoe UI", 25, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(0, 133, 66);
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        titleLabel.AutoSize = true;
        titleLabel.Anchor = AnchorStyles.None;

        // TextBox
        this.poolNameTextBox = new TextBox();
        this.poolNameTextBox.Anchor = AnchorStyles.None;
        this.poolNameTextBox.Width = 500;

        // button to upload QB file
        this.uploadQB = new Button();
        this.uploadQB.Text = "Upload File";
        this.uploadQB.Size = new Size(300, 90);
        this.uploadQB.BackColor = Color.FromArgb(0, 133, 66);
        this.uploadQB.ForeColor = Color.White;
        this.uploadQB.FlatStyle = FlatStyle.Flat;
        this.uploadQB.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.uploadQB.FlatAppearance.BorderSize = 0;
        // this.uploadQB.Click += new EventHandler(this.uploadQBButton_Click);

        // button to create QB
        this.createQB = new Button();
        this.createQB.Text = "Create QB";
        this.createQB.Size = new Size(300, 90);
        this.createQB.BackColor = Color.FromArgb(199, 91, 18);
        this.createQB.ForeColor = Color.White;
        this.createQB.FlatStyle = FlatStyle.Flat;
        this.createQB.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.createQB.FlatAppearance.BorderSize = 0;
        this.createQB.Click += new EventHandler(this.createQuestionBankButton_Click);

        // helps with alignment of buttons
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
        buttonPanel.FlowDirection = FlowDirection.LeftToRight; // want buttons side by side
        buttonPanel.AutoSize = true;
        buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        buttonPanel.Anchor = AnchorStyles.None;
        buttonPanel.Controls.Add(uploadQB);
        buttonPanel.Controls.Add(createQB);

        // aligns everything
        TableLayoutPanel contentLayout = new TableLayoutPanel();
        contentLayout.AutoSize = true;
        contentLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        contentLayout.Anchor = AnchorStyles.None;
        contentLayout.Dock = DockStyle.None;
        contentLayout.ColumnCount = 1;
        contentLayout.RowCount = 3;
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for title
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for text box
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // row for buttons
        contentLayout.Controls.Add(titleLabel, 0, 0);
        contentLayout.Controls.Add(poolNameTextBox, 0, 1);
        contentLayout.Controls.Add(buttonPanel, 0, 2);

        // wrapper
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