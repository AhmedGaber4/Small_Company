using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class DepartmentViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name Is Required")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Department code Is Required")]
        public string Code { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}
