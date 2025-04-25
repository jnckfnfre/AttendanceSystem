//Maha Shaikh 4/23/2025

namespace AttendanceDesktop
{
    partial class ViewQuestionBankForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView questionBankGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.questionBankGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.questionBankGrid)).BeginInit();
            this.SuspendLayout();

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

            // 
            // ViewQuestionBankForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.questionBankGrid);
            this.Name = "ViewQuestionBankForm";
            this.Text = "View Question Banks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.questionBankGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
