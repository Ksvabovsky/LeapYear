using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LeapYear.Data;
using LeapYear.Models;
using Microsoft.AspNetCore.Identity;

namespace LeapYear.Pages.Person
{
    public class IndexModel : PageModel
    {
        private readonly LeapYear.Data.LeapYearContext _context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(LeapYearContext context, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }

        public PaginatedList<LeapYearPerson> LeapYearPerson { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            IQueryable<LeapYearPerson> query = _context.LeapYearPerson;

            if (!string.IsNullOrEmpty(searchString))
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }



            var pageSize = Configuration.GetValue("PageSize", 4);
            LeapYearPerson = await PaginatedList<LeapYearPerson>.CreateAsync(
                query, pageIndex ?? 1, pageSize);
        }

        public string GetUserName(string id)
        {
            return (_userManager.Users.Any(x => x.Id == id)) ? _userManager.Users.Where(x => x.Id == id).FirstOrDefault().UserName : "";
        }

        //public IList<LeapYearPerson> LeapYearPerson { get;set; } = default!;

        //public async Task OnGetAsync()
        //{
        //    if (_context.Person != null)
        //    {
        //        LeapYearPerson = await _context.Person.ToListAsync();
        //    }
        //}
    }
}
