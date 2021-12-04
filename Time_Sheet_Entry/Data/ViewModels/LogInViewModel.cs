﻿using System.ComponentModel.DataAnnotations;

namespace Time_Sheet_Entry.Data.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember me ?")]
        public bool RememberMe { get; set;}

    }
}
