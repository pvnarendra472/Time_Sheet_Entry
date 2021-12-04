using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Time_Sheet_Entry.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
