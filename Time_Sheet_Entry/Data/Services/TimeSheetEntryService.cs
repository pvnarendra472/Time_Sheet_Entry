using Time_Sheet_Entry.Data.Base;
using Time_Sheet_Entry.Models;

namespace Time_Sheet_Entry.Data.Services
{
    public class TimeSheetEntryService : EntityBaseRepository<TimeSheetEntry>, ITimeSheetEntryService
    {
        public TimeSheetEntryService(ApplicationDbContext context) : base(context)
        {

        }

    }
}
