using System.ComponentModel.DataAnnotations;

namespace Lab13CodeFirst.Models
{
    public class Course
    {
        [Key]
        public int idCourse { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; } = string.Empty;

        public int Credit { get; set; }

        // Para eliminación lógica (Punto 7)
        public bool Active { get; set; } = true;
    }
}