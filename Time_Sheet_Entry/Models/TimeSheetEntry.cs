using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Time_Sheet_Entry.Data.Base;
using Time_Sheet_Entry.Data.Enum;

namespace Time_Sheet_Entry.Models
{
    public class TimeSheetEntry : IEntityBase
    {
        [Key]
        [ValidateNever]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Display(Name = "Employee Id")]
        public string? EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string? EmployeeName { get; set; }
        [Display(Name = "Work Date")]
        [Required(ErrorMessage = "Please enter Work Date")]
        public DateTime WorkDate { get; set; }
        public DateTime SubmitDate { get; set; }
        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "Please enter start time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        [Required(ErrorMessage = "Please enter end time")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Please select workorder")]
        public WorkOrder WorkOrder { get; set; }
        public string? ManagerId { get; set; }
        public bool Approved { get; set; }

    }
}
