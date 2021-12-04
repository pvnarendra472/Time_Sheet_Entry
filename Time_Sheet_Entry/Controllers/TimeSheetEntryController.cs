using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Time_Sheet_Entry.Data.Services;
using Time_Sheet_Entry.Models;

namespace Time_Sheet_Entry.Controllers
{
    public class TimeSheetEntryController : Controller
    {

        private readonly ITimeSheetEntryService _timeSheetEntryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TimeSheetEntryController(ITimeSheetEntryService timeSheetEntryService, UserManager<ApplicationUser> userManager)
        {
            _timeSheetEntryService = timeSheetEntryService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult EnterTime()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EnterTime(TimeSheetEntry timeEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(timeEntry);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            timeEntry.SubmitDate = DateTime.Now;
            timeEntry.Approved = false;
            timeEntry.EmployeeId = user.Id;
            timeEntry.EmployeeName = user.FullName;
            timeEntry.ManagerId = "";

            await _timeSheetEntryService.AddAsync(timeEntry);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



        public async Task<IActionResult> ApproveTime()
        {
            var timeSheetEntries = await _timeSheetEntryService.GetAllAsync();

            return View(timeSheetEntries.Where(c => c.Approved == false).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> ApproveTime(List<TimeSheetEntry> timeEntries)
        {
            foreach (var timeEntry in timeEntries)
            {
                if (!ModelState.IsValid)
                {
                    return View(timeEntry);
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);

                timeEntry.SubmitDate = DateTime.Now;
                timeEntry.Approved = true;
                timeEntry.ManagerId = user.Id;

                await _timeSheetEntryService.UpdateAsync(timeEntry.Id, timeEntry);


            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
