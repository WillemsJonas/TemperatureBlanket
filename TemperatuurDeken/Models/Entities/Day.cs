

public class Day
{
    public int DayId { get; set; }
    public DateOnly Date {  get; set; }
    public int? Temperature { get; set; } = null!;
    public Color Color { get; set; }
    public bool Done { get; set; }
}
