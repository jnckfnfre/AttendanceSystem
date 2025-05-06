/*
    David Sajdak 5/4/2025
    User control for entering one course (at a time).
    Contains relevant course fields.
    Designed to be used in a dynamic layout within AddCoursesForm.
*/
using System.Windows.Forms;

namespace AttendanceDesktop;

public partial class CourseEntryControl : UserControl
{
    // Read-only accessors used in AddCoursesForm
    public TextBox CourseIdTextBox => courseIdTextBox;
    public TextBox CourseNameTextBox => courseNameTextBox;
    public DateTimePicker StartTimePicker => startTimePicker;
    public DateTimePicker EndTimePicker => endTimePicker;

    private TextBox courseIdTextBox;
    private TextBox courseNameTextBox;
    private DateTimePicker startTimePicker;
    private DateTimePicker endTimePicker;

    public CourseEntryControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // general
        this.Width = 800;
        this.Height = 200;
        this.Margin = new Padding(10);

        // table layout for vertical alignment
        var layout = new TableLayoutPanel();
        layout.Dock = DockStyle.Fill;
        layout.ColumnCount = 2;
        layout.RowCount = 4;
        layout.AutoSize = true;

        // text boxes to fill in id and name
        courseIdTextBox = new TextBox() { Width = 300 };
        courseNameTextBox = new TextBox() { Width = 300 };

        // time pickers to set start time and end time
        startTimePicker = new DateTimePicker()
        {
            Format = DateTimePickerFormat.Time,
            ShowUpDown = true,
            Width = 300
        };

        endTimePicker = new DateTimePicker()
        {
            Format = DateTimePickerFormat.Time,
            ShowUpDown = true,
            Width = 300
        };

        // add labels and controls to layout
        layout.Controls.Add(new Label() { Text = "Course ID:", AutoSize = true }, 0, 0);
        layout.Controls.Add(courseIdTextBox, 1, 0);

        layout.Controls.Add(new Label() { Text = "Course Name:", AutoSize = true }, 0, 1);
        layout.Controls.Add(courseNameTextBox, 1, 1);

        layout.Controls.Add(new Label() { Text = "Start Time:", AutoSize = true }, 0, 2);
        layout.Controls.Add(startTimePicker, 1, 2);

        layout.Controls.Add(new Label() { Text = "End Time:", AutoSize = true }, 0, 3);
        layout.Controls.Add(endTimePicker, 1, 3);

        this.Controls.Add(layout);
    }
}