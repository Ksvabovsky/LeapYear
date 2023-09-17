using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeapYear.Models;
using LeapYear.Data;

using System.Collections;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using LeapYear.Interfaces;
using LeapYear.Services;
using LeapYear.ViewModels;

namespace LeapYear.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ILeapYearService _leapYearService;
    

    [BindProperty]
    public LeapYearPerson LeapYear { get; set; }

    public String SuccessMessage { get; set; }
    public ArrayList infoLeapYear { get; set; }

    public ListOfLeapYearListVM LeapYearPeople { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ILeapYearService leapYearInterface)
    {
        _logger = logger;
        _leapYearService = leapYearInterface;
    }

    public void OnGet()
    {
        LeapYearPeople = _leapYearService.GetTodayEntries();
    }
    public IActionResult OnPost()
    {
        // LeapYearPeople = _context.LeapYearComponent.ToList();
        if (ModelState.IsValid)
        {

            SuccessMessage = _leapYearService.AddRecord(LeapYear,User);
            LeapYearPeople = _leapYearService.GetTodayEntries();
            return Page();

        }
        return Page();
    }
}
