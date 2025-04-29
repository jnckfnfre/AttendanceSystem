// Eduardo Zamora 4/26/2025
// Designer generated code for the create quiz questions form
// This form is used to create quiz questions with consistent styling

namespace AttendanceDesktop
{
    partial class createQuizQuestionsForm
    {
        private System.ComponentModel.IContainer components = null;

        // Control declarations
        private System.Windows.Forms.ComboBox courseDropdown;
        private System.Windows.Forms.TextBox sessionTextBox;
        private System.Windows.Forms.Button generatePasswordButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button searchQuestionsButton;
        private System.Windows.Forms.DataGridView questionsDataGridView;
        private System.Windows.Forms.Button createQuizButton;
        private System.Windows.Forms.Label courseLabel;
        private System.Windows.Forms.Label sessionLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel mainContainer;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            
            // Color definitions
            System.Drawing.Color primaryColor = System.Drawing.Color.FromArgb(199, 91, 18);
            System.Drawing.Color secondaryColor = System.Drawing.Color.FromArgb(0, 133, 66);
            
            this.mainContainer = new System.Windows.Forms.Panel();
            this.courseDropdown = new System.Windows.Forms.ComboBox();
            this.sessionTextBox = new System.Windows.Forms.TextBox();
            this.generatePasswordButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.searchQuestionsButton = new System.Windows.Forms.Button();
            this.questionsDataGridView = new System.Windows.Forms.DataGridView();
            this.createQuizButton = new System.Windows.Forms.Button();
            this.courseLabel = new System.Windows.Forms.Label();
            this.sessionLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.questionsDataGridView)).BeginInit();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            
            // Form settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "createQuizQuestionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Quiz Questions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            
            // mainContainer
            this.mainContainer.AutoScroll = true;
            this.mainContainer.Controls.Add(this.createQuizButton);
            this.mainContainer.Controls.Add(this.questionsDataGridView);
            this.mainContainer.Controls.Add(this.searchQuestionsButton);
            this.mainContainer.Controls.Add(this.passwordTextBox);
            this.mainContainer.Controls.Add(this.generatePasswordButton);
            this.mainContainer.Controls.Add(this.passwordLabel);
            this.mainContainer.Controls.Add(this.sessionTextBox);
            this.mainContainer.Controls.Add(this.sessionLabel);
            this.mainContainer.Controls.Add(this.courseDropdown);
            this.mainContainer.Controls.Add(this.courseLabel);
            this.mainContainer.Controls.Add(this.topPanel);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(1000, 700);
            this.mainContainer.TabIndex = 0;
            
            // topPanel
            this.topPanel.BackColor = secondaryColor;
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1000, 40);
            this.topPanel.TabIndex = 0;
            
            // courseLabel
            this.courseLabel.AutoSize = true;
            this.courseLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.courseLabel.ForeColor = primaryColor;
            this.courseLabel.Location = new System.Drawing.Point(50, 50);
            this.courseLabel.Name = "courseLabel";
            this.courseLabel.Size = new System.Drawing.Size(56, 19);
            this.courseLabel.Text = "Course:";
            
            // courseDropdown
            this.courseDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.courseDropdown.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.courseDropdown.FormattingEnabled = true;
            this.courseDropdown.Location = new System.Drawing.Point(150, 45);
            this.courseDropdown.Name = "courseDropdown";
            this.courseDropdown.Size = new System.Drawing.Size(300, 25);
            this.courseDropdown.TabIndex = 1;
            
            // sessionLabel
            this.sessionLabel.AutoSize = true;
            this.sessionLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.sessionLabel.ForeColor = primaryColor;
            this.sessionLabel.Location = new System.Drawing.Point(50, 85);
            this.sessionLabel.Name = "sessionLabel";
            this.sessionLabel.Size = new System.Drawing.Size(62, 19);
            this.sessionLabel.Text = "Session:";
            
            // sessionTextBox
            this.sessionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sessionTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.sessionTextBox.Location = new System.Drawing.Point(150, 80);
            this.sessionTextBox.Name = "sessionTextBox";
            this.sessionTextBox.Size = new System.Drawing.Size(300, 25);
            this.sessionTextBox.TabIndex = 2;
            
            // passwordLabel
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.ForeColor = primaryColor;
            this.passwordLabel.Location = new System.Drawing.Point(50, 120);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(76, 19);
            this.passwordLabel.Text = "Password:";
            
            // generatePasswordButton
            this.generatePasswordButton.BackColor = primaryColor;
            this.generatePasswordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generatePasswordButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.generatePasswordButton.ForeColor = System.Drawing.Color.White;
            this.generatePasswordButton.Location = new System.Drawing.Point(150, 115);
            this.generatePasswordButton.Name = "generatePasswordButton";
            this.generatePasswordButton.Size = new System.Drawing.Size(150, 30);
            this.generatePasswordButton.TabIndex = 3;
            this.generatePasswordButton.Text = "Generate Password";
            this.generatePasswordButton.UseVisualStyleBackColor = false;
            
            // passwordTextBox
            this.passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.passwordTextBox.Location = new System.Drawing.Point(320, 115);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(130, 25);
            this.passwordTextBox.TabIndex = 4;
            
            // searchQuestionsButton
            this.searchQuestionsButton.BackColor = secondaryColor;
            this.searchQuestionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchQuestionsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.searchQuestionsButton.ForeColor = System.Drawing.Color.White;
            this.searchQuestionsButton.Location = new System.Drawing.Point(150, 155);
            this.searchQuestionsButton.Name = "searchQuestionsButton";
            this.searchQuestionsButton.Size = new System.Drawing.Size(300, 35);
            this.searchQuestionsButton.TabIndex = 7;
            this.searchQuestionsButton.Text = "Search Questions";
            this.searchQuestionsButton.UseVisualStyleBackColor = false;
            
            // questionsDataGridView
            this.questionsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.questionsDataGridView.BackgroundColor = Color.FromArgb(169, 169, 169);
            this.questionsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(255, 235, 224);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.questionsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.questionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 235, 224);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.questionsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.questionsDataGridView.GridColor = primaryColor;
            this.questionsDataGridView.Location = new System.Drawing.Point(50, 200);
            this.questionsDataGridView.Name = "questionsDataGridView";
            this.questionsDataGridView.ReadOnly = true;
            this.questionsDataGridView.Size = new System.Drawing.Size(900, 400);
            this.questionsDataGridView.TabIndex = 5;
            
            // createQuizButton
            this.createQuizButton.BackColor = secondaryColor;
            this.createQuizButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createQuizButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.createQuizButton.ForeColor = System.Drawing.Color.White;
            this.createQuizButton.Location = new System.Drawing.Point(775, 615); // or (425, 615) if you want it centered
            this.createQuizButton.Name = "createQuizButton";
            this.createQuizButton.Size = new System.Drawing.Size(150, 45);
            this.createQuizButton.TabIndex = 10;
            this.createQuizButton.Text = "Create Quiz";
            this.createQuizButton.UseVisualStyleBackColor = false;

            // createQuizQuestionsForm
            this.Controls.Add(this.mainContainer);
            ((System.ComponentModel.ISupportInitialize)(this.questionsDataGridView)).EndInit();
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
    }
}