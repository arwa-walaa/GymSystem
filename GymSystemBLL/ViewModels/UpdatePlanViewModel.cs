using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels
{
    public class UpdatePlanViewModel
    {
        public string PlanName { get; set; } = null!;
        [Required(ErrorMessage ="Required !")]
        [StringLength(200,MinimumLength =5,ErrorMessage ="You must Enter Description in Range 5 : 200 chars !")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Required !")]
        [Range(1, 1000, ErrorMessage = "Price Must be Greater than 1 !")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Required !")]
        [Range(1,  365, ErrorMessage = "Days must be Greater than 1 and less than 365 !")]
        public int DurationDays { get; set; }
    }
}
