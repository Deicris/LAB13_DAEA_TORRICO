using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab13CodeFirst.Models
{
    public class Enrollment
    {
        [Key]
        public int idEnrollment { get; set; }

        public DateTime Date { get; set; }

        // Llave foránea hacia Student
        public int Student_idStudent { get; set; }

        [ForeignKey("Student_idStudent")]
        public Student? Student { get; set; }

        // Llave foránea hacia Course
        public int Course_idCourse { get; set; }

        [ForeignKey("Course_idCourse")]
        public Course? Course { get; set; }
    }
}