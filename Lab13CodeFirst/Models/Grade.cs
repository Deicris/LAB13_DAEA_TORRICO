using System.ComponentModel.DataAnnotations;

namespace Lab13CodeFirst.Models
{
    public class Grade
    {
        [Key]
        public int idGrade { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(45)]
        public string Description { get; set; } = string.Empty;

        // Para eliminación lógica (Punto 6)
        public bool Active { get; set; } = true;
    }
}