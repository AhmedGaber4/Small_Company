using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Employee:TheBase
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

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public string ImageUrl { get; set; }
     

    }
}
