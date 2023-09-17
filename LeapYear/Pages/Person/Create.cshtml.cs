using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeapYear.Data;
using LeapYear.Models;

namespace LeapYear.Pages.Person
{
    public class CreateModel : PageModel
    {
        private readonly LeapYear.Data.LeapYearContext _context;

        public CreateModel(LeapYear.Data.LeapYearContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LeapYearPerson LeapYearPerson { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.LeapYearPerson == null || LeapYearPerson == null)
            {
                return Page();
            }

            _context.LeapYearPerson.Add(LeapYearPerson);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
