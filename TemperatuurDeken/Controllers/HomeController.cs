using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TemperatuurDeken.Models;
using TemperatuurDeken.Models.Viewmodels;
using TemperatuurDeken.Repositories.Interfaces;

namespace TemperatuurDeken.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDayInterface _dayRepository;

    public HomeController(ILogger<HomeController> logger, IDayInterface dayService) => (this._logger, this._dayRepository) = (logger, dayService);

    [HttpGet]
    public IActionResult Index()
    {
        var allDays = _dayRepository.ShowAllDays();
        List<DayViewModel> allDaysViewModel = new List<DayViewModel>();
        allDays.ForEach(day => allDaysViewModel.Add(new DayViewModel { DayId = day.DayId, Date = day.Date, Done = day.Done, Color = day.Color, OldTemperature = day.Temperature }));

        HttpContext.Response.Cookies.Delete("searchInput");
        return View(allDaysViewModel);
    }

    [HttpPost]
    public IActionResult Update(int id)
    {
        string? searchInput = Request.Cookies["searchInput"];
        _dayRepository.Update(id);

        if (searchInput is null)
            return RedirectToAction(nameof(Index));
        
        return Redirect($"~/Home/ShowPeriod?input={searchInput}");
    }

    [HttpPost]
    public IActionResult AddTemperature(DayViewModel day)
    {
        string? searchInput = Request.Cookies["searchInput"];
        _dayRepository.AddTemperature(day);

        if (searchInput is null)
            return RedirectToAction(nameof(Index));

        return Redirect($"~/Home/ShowPeriod?input={searchInput}");
    }

    [HttpGet]
    public IActionResult HideDoneDays()
    {
        List<Day> toDoDays = _dayRepository.HideDoneDays();
        List<DayViewModel> toDoDaysViewModel = new List<DayViewModel>();
        _dayRepository.HideDoneDays().ForEach(dag => toDoDaysViewModel.Add(new DayViewModel { DayId = dag.DayId, Date = dag.Date, Done = dag.Done, Color = dag.Color, OldTemperature = dag.Temperature }));
        return View("Index", toDoDaysViewModel);
    }

    [HttpGet]
    public IActionResult ShowPeriod(string input)
    {
        CookieOptions options = new CookieOptions();
        options.MaxAge = TimeSpan.FromDays(1);
        Response.Cookies.Append("searchInput", input, options);

        List<Day> chosenPeriod = _dayRepository.ShowChosenPeriod(input);
        List<DayViewModel> chosenPeriodViewModel = new List<DayViewModel>();
        chosenPeriod.ForEach(dag => chosenPeriodViewModel.Add(new DayViewModel { DayId = dag.DayId, Date = dag.Date, Done = dag.Done,Color = dag.Color, OldTemperature = dag.Temperature }));

        return View("Index", chosenPeriodViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
