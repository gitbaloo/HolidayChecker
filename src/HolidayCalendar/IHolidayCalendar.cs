using System;
using System.Collections;
using System.Threading.Tasks;

namespace HolidayCalendar;
public interface IHolidayCalendar
{
  Task<bool> IsHoliday(DateTime date);

  Task<ICollection<Holiday>> GetHolidays(DateTime startDate, DateTime endDate);
}
