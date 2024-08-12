using TemperatuurDeken.Models.Viewmodels;

namespace TemperatuurDeken.Repositories.Interfaces;

public interface IDayInterface
{
    List<Day> ShowAllDays();
    void Update(int id);
    void AddTemperature(DayViewModel day);
    List<Day> HideDoneDays();
    List<Day> ShowChosenPeriod(string period);
}
