namespace TemperatuurDeken.Models.Viewmodels;

public class DekenViewModel
{
    public int? Temperatuur { get; set; } = null!;
    public Kleur Kleur { get; set; }
    public DateOnly Datum { get; set; }
    public bool Gemaakt { get; set; }
}
