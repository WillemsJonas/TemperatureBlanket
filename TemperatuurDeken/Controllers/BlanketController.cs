using Microsoft.AspNetCore.Mvc;
using TemperatuurDeken.Models.Viewmodels;
using TemperatuurDeken.Repositories.Interfaces;
using TemperatuurDeken.Repositories.Services;

namespace TemperatuurDeken.Controllers;
public class BlanketController : Controller
{
    private readonly IDayInterface _dayRepository;
    public BlanketController( IDayInterface dayService) =>  this._dayRepository = dayService;

    [HttpGet]
    public IActionResult PreviewBlanket()
    {
        List<BlanketViewModel> blanketViewModels = new List<BlanketViewModel>();
        _dayRepository.ShowAllDays().ForEach(day => blanketViewModels.Add(new BlanketViewModel{Date = day.Date,Color = day.Color, Temperature = day.Temperature, Done = day.Done}));
        return View(blanketViewModels);
    }

    [HttpGet]
    public IActionResult ShowDone()
    {
        List<BlanketViewModel> blanketViewModels = new List<BlanketViewModel>();
        List<Day> doneDays = _dayRepository.ShowAllDays().Where(day => day.Done.Equals(true)).ToList();
        doneDays.ForEach(day => blanketViewModels.Add(new BlanketViewModel { Date = day.Date, Color = day.Color, Temperature = day.Temperature, Done = day.Done }));
        return View(nameof(PreviewBlanket), blanketViewModels);
    }

    [HttpGet]
    public IActionResult ShowToDo()
    {
        List<BlanketViewModel> blanketViewModels = new List<BlanketViewModel>();
        List<Day> toDoDays = _dayRepository.ShowAllDays().Where(d => d.Done.Equals(false)).ToList();
        toDoDays.ForEach(day => blanketViewModels.Add(new BlanketViewModel { Date = day.Date, Color = day.Color, Temperature = day.Temperature, Done = day.Done }));
        return View(nameof(PreviewBlanket), blanketViewModels);
    }

    [HttpGet]
    public IActionResult ShowPeriod(string input)
    {
        List<Day> chosenPeriod = _dayRepository.ShowChosenPeriod(input);
        List<BlanketViewModel> chosenPeriodViewModel = new List<BlanketViewModel>();
        chosenPeriod.ForEach(day => chosenPeriodViewModel.Add(new BlanketViewModel {Date = day.Date, Done = day.Done, Color = day.Color, Temperature = day.Temperature }));

        return View(nameof(PreviewBlanket), chosenPeriodViewModel);
    }
}
