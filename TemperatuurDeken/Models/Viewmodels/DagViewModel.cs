namespace TemperatuurDeken.Models.Viewmodels;

public class DagViewModel
{
    public int DagId { get; set; }
    public DateOnly Datum { get; set; }
    public int? OudeTemperatuur { get; set; } = null!;
    public int? NieuweTemperatuur { get; set; } = null!;
    public Kleur Kleur { get; set; }
    public bool Gemaakt { get; set; }
    public string? ReturnUrl { get; set; }
}
