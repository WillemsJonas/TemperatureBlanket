

public class Dag
{
    public int DagId { get; set; }
    public DateOnly Datum {  get; set; }
    public int? Temperatuur { get; set; } = null!;
    public Kleur Kleur { get; set; }
    public bool Gemaakt { get; set; }
}
