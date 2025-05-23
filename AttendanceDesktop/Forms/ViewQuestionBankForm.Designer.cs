//Maha Shaikh 4/23/2025

namespace AttendanceDesktop
{
    partial class ViewQuestionBankForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView questionBankGrid;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.PictureBox loadingPictureBox;
        private System.Windows.Forms.ComboBox classFilterComboBox;
        private System.Windows.Forms.Label classFilterLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
//Maha Shaikh 4/30/2025 
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewQuestionBankForm));
            this.questionBankGrid = new System.Windows.Forms.DataGridView();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.classFilterComboBox = new System.Windows.Forms.ComboBox();
            this.classFilterLabel = new System.Windows.Forms.Label();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.loadingPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.questionBankGrid)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPictureBox)).BeginInit();
            this.SuspendLayout();
           
            // Define colors for consistency
            System.Drawing.Color primaryColor = System.Drawing.Color.FromArgb(199, 91, 18);  // Orange
            System.Drawing.Color secondaryColor = System.Drawing.Color.FromArgb(0, 133, 66); // Green
           
            //
            // headerPanel
            //
            this.headerPanel.BackColor = secondaryColor;
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 60;
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Name = "headerPanel";
           
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = false;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Text = "Question Bank";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleLabel.Name = "titleLabel";
           
            //
            // searchPanel
            //
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Height = 100; // Increased height to accommodate class filter
            this.searchPanel.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.searchPanel.Controls.Add(this.classFilterLabel);
            this.searchPanel.Controls.Add(this.classFilterComboBox);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
           
            //
            // classFilterLabel
            //
            this.classFilterLabel.AutoSize = true;
            this.classFilterLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.classFilterLabel.ForeColor = primaryColor;
            this.classFilterLabel.Location = new System.Drawing.Point(1, 15);
            this.classFilterLabel.Text = "Courses:";
            this.classFilterLabel.Name = "classFilterLabel";
           
            //
            // classFilterComboBox
            //
            this.classFilterComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.classFilterComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.classFilterComboBox.Location = new System.Drawing.Point(130, 12);
            this.classFilterComboBox.Size = new System.Drawing.Size(385, 30);
            this.classFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classFilterComboBox.Name = "classFilterComboBox";
            this.classFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.classFilterComboBox_SelectedIndexChanged);
           
            //
            // questionBankGrid
            //
            this.questionBankGrid.AllowUserToAddRows = false;
            this.questionBankGrid.AllowUserToDeleteRows = false;
            this.questionBankGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.questionBankGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questionBankGrid.BackgroundColor = System.Drawing.Color.White;
            this.questionBankGrid.ReadOnly = true;
            this.questionBankGrid.Name = "questionBankGrid";
            this.questionBankGrid.TabIndex = 0;
            this.questionBankGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.questionBankGrid.MultiSelect = false;
            this.questionBankGrid.RowHeadersVisible = false;
           
            //
            // ViewQuestionBankForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.questionBankGrid);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.statusPanel);
            this.Name = "ViewQuestionBankForm";
            this.Text = "View Question Banks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            ((System.ComponentModel.ISupportInitialize)(this.questionBankGrid)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPictureBox)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
