using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LeapYear.Data;
using LeapYear.Models;

namespace LeapYear.Pages.Person
{
    public class DeleteModel : PageModel
    {
        private readonly LeapYear.Data.LeapYearContext _context;

        public DeleteModel(LeapYear.Data.LeapYearContext context)
        {
            _context = context;
        }

        [BindProperty]
      public LeapYearPerson LeapYearPerson { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LeapYearPerson == null)
            {
                return NotFound();
            }

            var leapyearperson = await _context.LeapYearPerson.FirstOrDefaultAsync(m => m.Id == id);

            if (leapyearperson == null)
            {
                return NotFound();
            }
            else 
            {
                LeapYearPerson = leapyearperson;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.LeapYearPerson == null)
            {
                return NotFound();
            }
            var leapyearperson = await _context.LeapYearPerson.FindAsync(id);

            if (leapyearperson != null)
            {
                LeapYearPerson = leapyearperson;
                _context.LeapYearPerson.Remove(LeapYearPerson);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
