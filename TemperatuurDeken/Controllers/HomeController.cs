using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TemperatuurDeken.Models;
using TemperatuurDeken.Models.Viewmodels;
using TemperatuurDeken.Repositories.Interfaces;

namespace TemperatuurDeken.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDagInterface _dagRepository;

    public HomeController(ILogger<HomeController> logger, IDagInterface dagService) => (this._logger, this._dagRepository) = (logger, dagService);

    [HttpGet]
    public IActionResult Index()
    {
        var alleDagenuitDatabase = _dagRepository.ToonAlleDagen();
        List<DagViewModel> alleDagenViewModel = new List<DagViewModel>();
        alleDagenuitDatabase.ForEach(dag => alleDagenViewModel.Add(new DagViewModel { DagId = dag.DagId, Datum = dag.Datum, Gemaakt = dag.Gemaakt, Kleur = dag.Kleur, OudeTemperatuur = dag.Temperatuur }));

        HttpContext.Response.Cookies.Delete("zoekwaarde");
        return View(alleDagenViewModel);
    }

    [HttpPost]
    public IActionResult Update(int id)
    {
        string? zoekwaarde = Request.Cookies["zoekwaarde"];
        _dagRepository.Update(id);

        if (zoekwaarde is null)
            return RedirectToAction(nameof(Index));
        
        return Redirect($"~/Home/ToonPeriode?invoer={zoekwaarde}");
    }

    [HttpPost]
    public IActionResult VoegTemperatuurToe(DagViewModel dag)
    {
        string? zoekwaarde = Request.Cookies["zoekwaarde"];
        _dagRepository.VoegTemperatuurToe(dag);

        if (zoekwaarde is null)
            return RedirectToAction(nameof(Index));

        return Redirect($"~/Home/ToonPeriode?invoer={zoekwaarde}");
    }

    [HttpGet]
    public IActionResult VerbergGemaakteDagen()
    {
        List<Dag> nietGemaakteDagen = _dagRepository.VerbergGemaakteDagen();
        List<DagViewModel> nietGemaakteDagenViewModel = new List<DagViewModel>();
        _dagRepository.VerbergGemaakteDagen().ForEach(dag => nietGemaakteDagenViewModel.Add(new DagViewModel { DagId = dag.DagId, Datum = dag.Datum, Gemaakt = dag.Gemaakt, Kleur = dag.Kleur, OudeTemperatuur = dag.Temperatuur }));
        return View("Index", nietGemaakteDagenViewModel);
    }

    [HttpGet]
    public IActionResult ToonPeriode(string invoer)
    {
        CookieOptions options = new CookieOptions();
        options.MaxAge = TimeSpan.FromDays(1);
        Response.Cookies.Append("zoekwaarde", invoer, options);

        List<Dag> gekozenPeriode = _dagRepository.ToonGekozePeriode(invoer);
        List<DagViewModel> gekozenPeriodeViewModel = new List<DagViewModel>();
        gekozenPeriode.ForEach(dag => gekozenPeriodeViewModel.Add(new DagViewModel { DagId = dag.DagId, Datum = dag.Datum, Gemaakt = dag.Gemaakt,Kleur = dag.Kleur, OudeTemperatuur = dag.Temperatuur }));

        return View("Index", gekozenPeriodeViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
