using LeapYear.Data;
using LeapYear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeapYear.Pages
{
    public class SearchHistoryModel : PageModel
    {
        private readonly LeapYearContext _context;
        public IList<LeapYearPerson> LeapYearPeople { get; set; }

        public SearchHistoryModel(LeapYearContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            LeapYearPeople = _context.LeapYearPerson.OrderByDescending(x => x.TimeOfWrite).Take(20).ToList();
        }
    }
}
