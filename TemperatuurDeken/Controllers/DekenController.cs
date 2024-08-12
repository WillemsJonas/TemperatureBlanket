using Microsoft.AspNetCore.Mvc;
using TemperatuurDeken.Models.Viewmodels;
using TemperatuurDeken.Repositories.Interfaces;
using TemperatuurDeken.Repositories.Services;

namespace TemperatuurDeken.Controllers;
public class DekenController : Controller
{
    private readonly IDagInterface _dagRepository;
    public DekenController( IDagInterface dagService) =>  this._dagRepository = dagService;
    public IActionResult PreviewDeken()
    {
        List<DekenViewModel> dekenViewModels = new List<DekenViewModel>();
        _dagRepository.ToonAlleDagen().ForEach(dag => dekenViewModels.Add(new DekenViewModel{Datum = dag.Datum,Kleur = dag.Kleur, Temperatuur = dag.Temperatuur, Gemaakt = dag.Gemaakt}));
        return View(dekenViewModels);
    }

    public IActionResult ToonGemaakt()
    {
        List<DekenViewModel> dekenViewModels = new List<DekenViewModel>();
        List<Dag> gemaakteDagen = _dagRepository.ToonAlleDagen().Where(d => d.Gemaakt.Equals(true)).ToList();
        gemaakteDagen.ForEach(dag => dekenViewModels.Add(new DekenViewModel { Datum = dag.Datum, Kleur = dag.Kleur, Temperatuur = dag.Temperatuur, Gemaakt = dag.Gemaakt }));
        return View(nameof(PreviewDeken), dekenViewModels);
    }

    public IActionResult ToonNogTeMaken()
    {
        List<DekenViewModel> dekenViewModels = new List<DekenViewModel>();
        List<Dag> gemaakteDagen = _dagRepository.ToonAlleDagen().Where(d => d.Gemaakt.Equals(false)).ToList();
        gemaakteDagen.ForEach(dag => dekenViewModels.Add(new DekenViewModel { Datum = dag.Datum, Kleur = dag.Kleur, Temperatuur = dag.Temperatuur, Gemaakt = dag.Gemaakt }));
        return View(nameof(PreviewDeken), dekenViewModels);
    }

    public IActionResult ToonPeriode(string invoer)
    {
        List<Dag> gekozenPeriode = _dagRepository.ToonGekozePeriode(invoer);
        List<DekenViewModel> gekozenPeriodeViewModel = new List<DekenViewModel>();
        gekozenPeriode.ForEach(dag => gekozenPeriodeViewModel.Add(new DekenViewModel {Datum = dag.Datum, Gemaakt = dag.Gemaakt, Kleur = dag.Kleur, Temperatuur = dag.Temperatuur }));

        return View(nameof(PreviewDeken), gekozenPeriodeViewModel);
    }
}
