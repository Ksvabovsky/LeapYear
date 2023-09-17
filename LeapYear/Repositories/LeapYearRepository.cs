namespace LeapYear.Repositories;
using LeapYear.Data;
using LeapYear.Interfaces;
using LeapYear.Models;

public class LeapYearRepository : ILeapYearRepository
{
    private readonly LeapYearContext _context;
    public LeapYearRepository(LeapYearContext context)
    {
        _context = context;
    }
    public void addToDatabase(LeapYearPerson leapYear)
    {
        _context.LeapYearPerson.Add(leapYear);
        _context.SaveChanges();
    }
    public List<LeapYearPerson> getAll()
    {
        return _context.LeapYearPerson.OrderByDescending(x => x.TimeOfWrite).Take(20).ToList();
    }

    public List<LeapYearPerson> GetEntriesFromToday()
    {
        return _context.LeapYearPerson.Where(x => x.TimeOfWrite.Value.Day == DateTime.Now.Day &&
                                            x.TimeOfWrite.Value.Month == DateTime.Now.Month &&
                                            x.TimeOfWrite.Value.Year == DateTime.Now.Year).OrderByDescending(x => x.TimeOfWrite).Take(20).ToList();
    }
}