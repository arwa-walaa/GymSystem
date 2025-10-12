using GymSystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels
{
    internal class TrainerToUpdateViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Photo { get; set; } = null!;

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Foramt")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must be between 5 and 100 characters.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required!")]
        [EmailAddress(ErrorMessage = "Invalid Phone Foramt")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "You Must Enter Egyption Number Foramt")]
        public string? Phone { get; set; } = null!;

        [Required(ErrorMessage = "Required!")]
        [Range(1, 9000, ErrorMessage = "Building Number Must be greater than 0")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Required!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Streey Must be between 2 and 30 chars .")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Required!")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Specialites is required!")]
        public Specialist Specialites { get; set; }
    }
}
