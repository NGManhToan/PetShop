﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PetShop.Models.Login
{
    public class ChangePassModel
    {
        [Required, Display(Name = "Current password")]
        public string CurrentPassword { get; set; }
        [Required, Display(Name = "New password")]
        public string NewPassword { get; set; }
        [Required, Display(Name = "Confirm new password")]
        public string ConfirmNewPassword { get; set; }

    }
}
