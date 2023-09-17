using Microsoft.EntityFrameworkCore;

namespace LeapYear.Services;
using LeapYear.Models;
using LeapYear.Interfaces;
using LeapYear.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;

public class LeapYearService : ILeapYearService
{
    private readonly ILeapYearRepository _leapYearRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public LeapYearService(ILeapYearRepository leapYearRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _leapYearRepository = leapYearRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }



public string AddRecord(LeapYearPerson leapYear, System.Security.Claims.ClaimsPrincipal user )
    {
        string successMessage = $"{leapYear.Name} urodzony w {leapYear.Year} roku,czyli roku {leapYear.isYearLeapYear()}.";
        leapYear.Outcome = successMessage;
        leapYear.TimeOfWrite = DateTime.Now;
        if (_signInManager.IsSignedIn(user))
        {
            leapYear.UserId = _userManager.GetUserId(user);
            leapYear.User = (IdentityUser?)_userManager.Users.Where(x => x.Id == leapYear.UserId).First();
        }
        _leapYearRepository.addToDatabase(leapYear);
        return successMessage;
    }
    public ListOfLeapYearListVM GetAllEntries()
    {
        var leapYearlist = _leapYearRepository.getAll();
        ListOfLeapYearListVM result = new ListOfLeapYearListVM();
        result.LeapYears = new List<LeapYearListVM>();
        foreach (var person in leapYearlist)
        {
            // mapowanie obiektow
            var LYLVM = new LeapYearListVM()
            {
                Outcome = person.Outcome
            };
            result.LeapYears.Add(LYLVM);
        }
        result.Count = result.LeapYears.Count;
        return result;
    }

    public ListOfLeapYearListVM GetTodayEntries()
    {
        var leapYearlist = _leapYearRepository.GetEntriesFromToday();
        ListOfLeapYearListVM result = new ListOfLeapYearListVM();
        result.LeapYears = new List<LeapYearListVM>();
        foreach (var person in leapYearlist)
        {
            var LYLVM = new LeapYearListVM()
            {
                Outcome = person.Outcome
            };
            result.LeapYears.Add(LYLVM);
        }
        result.Count = result.LeapYears.Count;
        return result;
    }

}