using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Department : TheBase
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
