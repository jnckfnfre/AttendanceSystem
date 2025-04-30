/*
    David Sajdak 4/30/2025
    User control for entering one question (at a time).
    Contains relevant question fields.
    Designed to be used in a dynamic layout within CreateQuestionBankForm.
*/

using System.Windows.Forms;

namespace AttendanceDesktop
{
    public partial class QuestionEntryControl : UserControl
    {
        // read-only accessors for all input fields
        // used in the create question bank form to extract user input
        public TextBox QuestionTextBox => questionTextBox;
        public TextBox OptionATextBox => optionATextBox;
        public TextBox OptionBTextBox => optionBTextBox;
        public TextBox OptionCTextBox => optionCTextBox;
        public TextBox OptionDTextBox => optionDTextBox;
        public ComboBox CorrectAnswerComboBox => correctAnswerComboBox;

        // Constructor
        public QuestionEntryControl()
        {
            InitializeComponent();
        }

        private TextBox questionTextBox;
        private TextBox optionATextBox;
        private TextBox optionBTextBox;
        private TextBox optionCTextBox;
        private TextBox optionDTextBox;
        private ComboBox correctAnswerComboBox;

        private void InitializeComponent()
        {
            // Set the default size and padding for the entire control
            this.Width = 800;
            this.Height = 400;
            this.Margin = new Padding(10);

            // Use a table layout for vertical alignment
            var layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 2;
            layout.RowCount = 6;
            layout.AutoSize = true;
            layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Create and configure all the input fields
            questionTextBox = new TextBox() { Width = 400, Multiline = true, Height = 60 };
            optionATextBox = new TextBox() { Width = 400 };
            optionBTextBox = new TextBox() { Width = 400 };
            optionCTextBox = new TextBox() { Width = 400 };
            optionDTextBox = new TextBox() { Width = 400 };
            correctAnswerComboBox = new ComboBox() { Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };
            correctAnswerComboBox.Items.AddRange(new string[] { "A", "B", "C", "D" });

            // Add labels and controls to layout
            layout.Controls.Add(new Label() { Text = "Question Text:", AutoSize = true }, 0, 0);
            layout.Controls.Add(questionTextBox, 1, 0);
            layout.Controls.Add(new Label() { Text = "Option A:", AutoSize = true }, 0, 1);
            layout.Controls.Add(optionATextBox, 1, 1);
            layout.Controls.Add(new Label() { Text = "Option B:", AutoSize = true }, 0, 2);
            layout.Controls.Add(optionBTextBox, 1, 2);
            layout.Controls.Add(new Label() { Text = "Option C:", AutoSize = true }, 0, 3);
            layout.Controls.Add(optionCTextBox, 1, 3);
            layout.Controls.Add(new Label() { Text = "Option D:", AutoSize = true }, 0, 4);
            layout.Controls.Add(optionDTextBox, 1, 4);
            layout.Controls.Add(new Label() { Text = "Correct Answer:", AutoSize = true }, 0, 5);
            layout.Controls.Add(correctAnswerComboBox, 1, 5);

            // Add layout panel to this control
            this.Controls.Add(layout);
        }
    }
}