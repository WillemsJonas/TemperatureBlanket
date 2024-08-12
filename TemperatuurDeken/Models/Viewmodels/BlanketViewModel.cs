namespace TemperatuurDeken.Models.Viewmodels;

public class BlanketViewModel
{
    public int? Temperature { get; set; } = null!;
    public Color Color { get; set; }
    public DateOnly Date { get; set; }
    public bool Done { get; set; }
}
