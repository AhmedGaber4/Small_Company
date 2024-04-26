using Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }
        public string Adreses { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool Isactive { get; set; }
        public DateTime HiringDate { get; set; } = DateTime.Now;
        public int DepartmentId { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
