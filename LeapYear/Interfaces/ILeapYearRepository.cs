namespace LeapYear.Interfaces;
using LeapYear.Models;

public interface ILeapYearRepository
{
    void addToDatabase(LeapYearPerson leapYear);
    List<LeapYearPerson> getAll();
    List<LeapYearPerson> GetEntriesFromToday();
}