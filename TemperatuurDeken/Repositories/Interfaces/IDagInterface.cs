using TemperatuurDeken.Models.Viewmodels;

namespace TemperatuurDeken.Repositories.Interfaces;

public interface IDagInterface
{
    List<Dag> ToonAlleDagen();
    void Update(int id);
    void VoegTemperatuurToe(DagViewModel dag);
    List<Dag> VerbergGemaakteDagen();
    List<Dag> ToonGekozePeriode(string periode);
}
