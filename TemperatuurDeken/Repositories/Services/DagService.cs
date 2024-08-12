using Microsoft.EntityFrameworkCore;
using TemperatuurDeken.Models.Viewmodels;
using TemperatuurDeken.Repositories.Interfaces;
using TemperatuurDekenLibrary;

namespace TemperatuurDeken.Repositories.Services;

public class DagService : IDagInterface
{
    private TemperatuurDekenContext _context;
    public DagService(TemperatuurDekenContext context) => _context = context;
    public List<Dag> ToonAlleDagen() // Haalt alle dagen op uit database
    {
        return _context.Dagen.OrderBy(dag => dag.Datum).ToList();
    }

    public void Update(int id) // Update Gemaakt property van Dag object
    {
        Dag? dag = _context.Dagen.Find(id);
        if (dag != null)
        {
            if (dag.Gemaakt)
                dag.Gemaakt = false;
            else
                dag.Gemaakt = true;
            _context.SaveChanges();
        }
    }

    public void VoegTemperatuurToe(DagViewModel dag) // Update Temperatuur property van Dag object
    {
        Dag? gevondenDag = _context.Dagen.Find(dag.DagId);
        if (gevondenDag != null)
        {
            gevondenDag.Temperatuur = dag.NieuweTemperatuur;
            gevondenDag = VoegKleurAanTemperatuurToe(gevondenDag);
            _context.SaveChanges();
        }
    }

    public Dag VoegKleurAanTemperatuurToe(Dag gevondenDag) // Kleur toevoegen aan Dag object a.h.v. temperatuur
    {
        int? temperatuur = gevondenDag.Temperatuur;
        switch (temperatuur)
        {
            case <= 5: gevondenDag.Kleur = Kleur.Lincoln; break;
            case <= 10: gevondenDag.Kleur = Kleur.Parchment; break;
            case <= 15: gevondenDag.Kleur = Kleur.Buttermilk; break;
            case <= 20: gevondenDag.Kleur = Kleur.PaleRose; break;
            case <= 25: gevondenDag.Kleur = Kleur.Grape; break;
            default: gevondenDag.Kleur = Kleur.Mocha; break;
        }
        return gevondenDag;
    }

    public List<Dag> VerbergGemaakteDagen() // Enkel dagen weergeven waarvan property Gemaakt "True" is 
    {
        List<Dag> alleDagen = ToonAlleDagen();
        return alleDagen.Where(dag => dag.Gemaakt.Equals(false)).ToList();
    }

    public List<Dag> ToonGekozePeriode(string periode) // Periode gekozen door gebruiker tonen
    {
        List<Dag> gevondenDagen = new List<Dag>();
        string[] datumsSplit = periode.Split('|');
        string[] eersteDatumSplit = datumsSplit[0].Split('-');
        string[] tweedeDatumSplit = eersteDatumSplit; // tweedeDatumSplit is standaard = eersteDatumSplit om geen exception te throwen bij één gevraagde datum
        if (datumsSplit.Length.Equals(2)) // Als periode tussen twee datums gevraagd wordt krijgt tweedeDatumSplit wel eigen value
            tweedeDatumSplit = datumsSplit[1].Split('-');

        try // Zijn eersteDatumSplit en/of tweeDatumSplit geen correcte datums wordt default legen list<Dag> gereturned
        {
            DateOnly begindatum = new DateOnly(2024, int.Parse(eersteDatumSplit[1]), int.Parse(eersteDatumSplit[0]));
            DateOnly eindDatum = new DateOnly(2024, int.Parse(tweedeDatumSplit[1]), int.Parse(tweedeDatumSplit[0]));

            Dag? eersteDatum = _context.Dagen.Where(dag => dag.Datum.Equals(begindatum)).FirstOrDefault();
            Dag? tweedeDatum = _context.Dagen.Where(dag => dag.Datum.Equals(eindDatum)).FirstOrDefault();
            gevondenDagen.AddRange(_context.Dagen.Where(dag => dag.Datum >= eersteDatum.Datum && dag.Datum <= tweedeDatum.Datum).OrderBy(dag => dag.Datum));
            return gevondenDagen;
        }
        catch 
        {
            return gevondenDagen;
        }
    }
}
