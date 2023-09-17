using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeapYear.Data;
using LeapYear.Models;

namespace LeapYear.Pages.Person
{
    public class EditModel : PageModel
    {
        private readonly LeapYear.Data.LeapYearContext _context;

        public EditModel(LeapYear.Data.LeapYearContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LeapYearPerson LeapYearPerson { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var leapyearperson =  await _context.Person.FirstOrDefaultAsync(m => m.Id == id);
            if (leapyearperson == null)
            {
                return NotFound();
            }
            LeapYearPerson = leapyearperson;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LeapYearPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeapYearPersonExists(LeapYearPerson.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LeapYearPersonExists(int id)
        {
          return (_context.Person?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
