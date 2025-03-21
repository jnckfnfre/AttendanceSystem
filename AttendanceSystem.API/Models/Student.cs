using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// Nahyan Munawar 3/21/2025
// Model which maps to the Student table
// This tells EF Core that this model maps to "Student" table of the database
[Table("Student")]
public class Student
{
    // Marks UtdId as the primary key of the table.
    // It maps to the "UTD_ID" column in the Database
    [Key]
    [Column("UTD_ID")]
    public string UtdId { get; set; }

    // EF Core automatically matches property names to column names if they are the same, otherwise we can use
    // [Column()] as below

    [Column("FIRST_NAME")]
    public string First_Name { get; set; }  // Student's first name

    [Column("LAST_NAME")]
    public string Last_Name { get; set; }   // Student's last name

    [Column("NET_ID")]
    public string Net_Id { get; set; }      // Student's UTD NetID used for login
}
