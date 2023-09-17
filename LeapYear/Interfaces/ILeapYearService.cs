namespace LeapYear.Interfaces;
using LeapYear.Models;
using LeapYear.ViewModels;

public interface ILeapYearService
{
    string AddRecord(LeapYearPerson leapYearComponent, System.Security.Claims.ClaimsPrincipal user);
    ListOfLeapYearListVM GetAllEntries();
    ListOfLeapYearListVM GetTodayEntries();

    //IQueryable<LeapYearPerson> GetEntriesFromToday();

}
