/*
    David Sajdak 4/21/2025
    Designer for Configuration table form
 */

namespace AttendanceDesktop;

partial class ConfigurationTableForm
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView configTableGridView;
    
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
        this.Text = "Configuration Table";
        this.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.BackColor = Color.White;

        // grid view for config table
        this.configTableGridView = new DataGridView();
        this.configTableGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.configTableGridView.Location = new System.Drawing.Point(50, 50);
        this.configTableGridView.Size = new System.Drawing.Size(this.ClientSize.Width - 100, this.ClientSize.Height - 250); // Adjusts dynamically
        this.configTableGridView.Size = new System.Drawing.Size(700, 350);
        this.configTableGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.configTableGridView.BorderStyle = BorderStyle.Fixed3D;
        this.configTableGridView.BackgroundColor = Color.FromArgb(169, 169, 169); // grey for now: maybe change?
        this.configTableGridView.GridColor = Color.FromArgb(199, 91, 18);;
        this.configTableGridView.DefaultCellStyle.BackColor = Color.White;
        this.configTableGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 235, 224); // subtle orange highlight
        this.configTableGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
        this.configTableGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.configTableGridView.ReadOnly = true;  // Make grid non-editable
        this.Controls.Add(this.configTableGridView);
    }

    #endregion
}