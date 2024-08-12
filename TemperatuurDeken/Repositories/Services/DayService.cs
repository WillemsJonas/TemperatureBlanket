using Microsoft.EntityFrameworkCore;
using TemperatuurDeken.Models.Viewmodels;
using TemperatuurDeken.Repositories.Interfaces;
using TemperatuurDekenLibrary;

namespace TemperatuurDeken.Repositories.Services;

public class DayService : IDayInterface
{
    private TemperatureBlanketContext _context;
    public DayService(TemperatureBlanketContext context) => _context = context;
    public List<Day> ShowAllDays() // Gets all days from database
    {
        return _context.Days.OrderBy(dag => dag.Date).ToList();
    }

    public void Update(int id) // Update Done property of Day object
    {
        Day? day = _context.Days.Find(id);
        if (day != null)
        {
            if (day.Done)
                day.Done = false;
            else
                day.Done = true;
            _context.SaveChanges();
        }
    }

    public void AddTemperature(DayViewModel day) // Update Temperature property of Day object
    {
        Day? foundDay = _context.Days.Find(day.DayId);
        if (foundDay != null)
        {
            foundDay.Temperature = day.NewTemperature;
            foundDay = AddColorToTemperature(foundDay);
            _context.SaveChanges();
        }
    }

    public Day AddColorToTemperature(Day foundDay) // Adds color to Day object
    {
        int? temperature = foundDay.Temperature;
        switch (temperature)
        {
            case <= 5: foundDay.Color = Color.Lincoln; break;
            case <= 10: foundDay.Color = Color.Parchment; break;
            case <= 15: foundDay.Color = Color.Buttermilk; break;
            case <= 20: foundDay.Color = Color.PaleRose; break;
            case <= 25: foundDay.Color = Color.Grape; break;
            default: foundDay.Color = Color.Mocha; break;
        }
        return foundDay;
    }

    public List<Day> HideDoneDays() // Only shows days with property Done "True" 
    {
        List<Day> alleDagen = ShowAllDays();
        return alleDagen.Where(day => day.Done.Equals(false)).ToList();
    }

    public List<Day> ShowChosenPeriod(string period) // Shows chosen period to user
    {
        List<Day> foundenDays = new List<Day>();
        string[] dateSplit = period.Split('|');
        string[] firstDateSplit = dateSplit[0].Split('-');
        string[] secondDateSplit = firstDateSplit; // secondDateSplit default equals = firstDateSplit to not get an exception when only asking 1 date
        if (dateSplit.Length.Equals(2)) // if a period is askes secondDateSplit does get a value
            secondDateSplit = dateSplit[1].Split('-');

        try // if firstDateSplit and/or secondDateSplit are incorrect dates an empty list<Day> will be returned
        {
            DateOnly startDate = new DateOnly(2024, int.Parse(firstDateSplit[1]), int.Parse(firstDateSplit[0]));
            DateOnly endDate = new DateOnly(2024, int.Parse(secondDateSplit[1]), int.Parse(secondDateSplit[0]));

            Day? firstDate = _context.Days.Where(day => day.Date.Equals(startDate)).FirstOrDefault();
            Day? secondDate = _context.Days.Where(day => day.Date.Equals(endDate)).FirstOrDefault();
            foundenDays.AddRange(_context.Days.Where(day => day.Date >= firstDate.Date && day.Date <= secondDate.Date).OrderBy(dag => dag.Date));
            return foundenDays;
        }
        catch 
        {
            return foundenDays;
        }
    }
}
