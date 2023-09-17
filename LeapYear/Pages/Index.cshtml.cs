using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeapYear.Models;
using LeapYear.Data;

using System.Collections;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace LeapYear.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly LeapYearContext _context;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    [BindProperty]
    public LeapYearPerson LeapYear { get; set; }

    public String SuccessMessage { get; set; }
    public ArrayList InformationsAboutLeapYear { get; set; }

    public IList<LeapYearPerson> LeapYearPeople { get; set; }

    public IndexModel(ILogger<IndexModel> logger, LeapYearContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public void OnGet()
    {

    }
    public IActionResult OnPost()
    {
        // LeapYearPeople = _context.LeapYearComponent.ToList();
        if (ModelState.IsValid)
        {
            var Data = HttpContext.Session.GetString("SessionVariable1");
            if (Data != null)
            {
                InformationsAboutLeapYear = JsonConvert.DeserializeObject<ArrayList>(Data);
            }
            else
                InformationsAboutLeapYear = new ArrayList();
            SuccessMessage = $"{LeapYear.Name} urodził się w {LeapYear.Year} roku. To był rok {LeapYear.isYearLeapYear()} .";

            var stringToCache = $"{InformationsAboutLeapYear.Count}. {LeapYear.Name}, {LeapYear.Year} - rok {LeapYear.isYearLeapYear()}";

            InformationsAboutLeapYear.Insert(0, stringToCache);
            HttpContext.Session.SetString("SessionVariable1", JsonConvert.SerializeObject(InformationsAboutLeapYear));

            //zapis do bazy danych 
            LeapYear.Outcome = SuccessMessage;
            LeapYear.TimeOfWrite = DateTime.Now;
            if (_signInManager.IsSignedIn(User))
            {
                LeapYear.UserId = _userManager.GetUserId(User);
                LeapYear.User = (IdentityUser?)_userManager.Users.Where(x => x.Id == LeapYear.UserId).First();
            }
            _context.LeapYearPerson.Add(LeapYear);
            _context.SaveChanges();
            return Page();
        }
        return Page();
    }
}
