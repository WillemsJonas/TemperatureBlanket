namespace TemperatuurDeken.Models.Viewmodels;

public class DayViewModel
{
    public int DayId { get; set; }
    public DateOnly Date { get; set; }
    public int? OldTemperature { get; set; } = null!;
    public int? NewTemperature { get; set; } = null!;
    public Color Color { get; set; }
    public bool Done { get; set; }
    public string? ReturnUrl { get; set; }
}
