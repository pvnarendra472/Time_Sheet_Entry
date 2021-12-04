using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Time_Sheet_Entry.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="THe {0} must be atleast {2} characters long", MinimumLength = 6)]
        [Display (Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required,Display(Name ="Full Name")]
        public string FullName { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }

        public string RoleSelected { get; set; }
    }
}
