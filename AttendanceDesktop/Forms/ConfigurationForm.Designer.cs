/*
    David Sajdak 4/21/2025
    Designer for Configuration form
 */

namespace AttendanceDesktop;

partial class ConfigurationForm
{
    private System.ComponentModel.IContainer components = null;
    private Button databaseInfo;
    private Button configTable;
    private Button newQuestionBank;
    private Button viewQuestionBank;

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
        this.Text = "Configuration and Management";
        this.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.Sizable;

        // title message
        Label titleLabel = new Label();
        titleLabel.Text = "Configuration and Management";
        titleLabel.Font = new Font("Segoe UI", 40, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(0, 133, 66);
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        titleLabel.AutoSize = true;
        titleLabel.Anchor = AnchorStyles.None;

        // button to keep track of database info
        this.databaseInfo = new Button();
        this.databaseInfo.Text = "Database Info";
        this.databaseInfo.Size = new Size(400, 150);
        this.databaseInfo.BackColor = Color.FromArgb(0, 133, 66);
        this.databaseInfo.ForeColor = Color.White;
        this.databaseInfo.FlatStyle = FlatStyle.Flat;
        this.databaseInfo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.databaseInfo.FlatAppearance.BorderSize = 0;
        // this.databaseInfo.Click += new EventHandler(this.databaseInfo_Click);

        // button for configuration table
        this.configTable = new Button();
        this.configTable.Text = "Configuration Table";
        this.configTable.Size = new Size(400, 150);
        this.configTable.BackColor = Color.White;
        this.configTable.ForeColor = Color.FromArgb(199, 91, 18);
        this.configTable.FlatStyle = FlatStyle.Flat;
        this.configTable.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.configTable.FlatAppearance.BorderSize = 1;
        this.configTable.Click += new EventHandler(this.configTableButton_Click);

        // button for creating a new question bank (will have 2 options: upload or create)
        this.newQuestionBank = new Button();
        this.newQuestionBank.Text = "New Question Bank";
        this.newQuestionBank.Size = new Size(400, 150);
        this.newQuestionBank.BackColor = Color.FromArgb(199, 91, 18);
        this.newQuestionBank.ForeColor = Color.White;
        this.newQuestionBank.FlatStyle = FlatStyle.Flat;
        this.newQuestionBank.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.newQuestionBank.FlatAppearance.BorderSize = 0;
        // this.newQuestionBank.Click += new EventHandler(this.newQuestionBank_Click);

        // button to view existing question banks (can filter for bank upon click)
        this.viewQuestionBank = new Button();
        this.viewQuestionBank.Text = "View Question Banks";
        this.viewQuestionBank.Size = new Size(400, 150);
        this.viewQuestionBank.BackColor = Color.FromArgb(0, 133, 66);
        this.viewQuestionBank.ForeColor = Color.White;
        this.viewQuestionBank.FlatStyle = FlatStyle.Flat;
        this.viewQuestionBank.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        this.viewQuestionBank.FlatAppearance.BorderSize = 0;

        // helps with alignment of buttons
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
        buttonPanel.FlowDirection = FlowDirection.LeftToRight; // want buttons side by side
        buttonPanel.AutoSize = true;
        buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        buttonPanel.Anchor = AnchorStyles.None;
        buttonPanel.Controls.Add(databaseInfo);
        buttonPanel.Controls.Add(configTable);
        buttonPanel.Controls.Add(newQuestionBank);
        buttonPanel.Controls.Add(viewQuestionBank);

        // aligns everything
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