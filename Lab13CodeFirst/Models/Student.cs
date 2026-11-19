using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab13CodeFirst.Models
{
    public class Student
    {
        [Key]
        public int idStudent { get; set; }

        [Required]
        [MaxLength(45)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(45)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(45)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(45)]
        public string Email { get; set; } = string.Empty;

        // Llave foránea hacia Grade
        public int Grade_idGrade { get; set; }

        [ForeignKey("Grade_idGrade")]
        public Grade? Grade { get; set; }
    }
}
