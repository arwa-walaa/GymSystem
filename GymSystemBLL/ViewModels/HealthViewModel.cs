using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels
{
    public class HealthViewModel
    {
        [Required(ErrorMessage = "Required!")]
        [Range(1, 500, ErrorMessage = "Weight Must be between 1 cm and 500 cm")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Range(1, 300, ErrorMessage = "Height Must be between 1 cm and 300 cm")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "Required!")]
        [StringLength(3,  ErrorMessage = "Blood Type Must 3 chars or less .")]
        public string BloodType { get; set; }

        public string? Note { get; set; }
    }
}
