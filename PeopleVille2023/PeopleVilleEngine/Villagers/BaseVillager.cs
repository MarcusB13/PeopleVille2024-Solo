using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

public abstract class BaseVillager
{
    public int Age { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }
    private Village _village;
    public bool IsBusy = false;
    public int Health = 100;
    public ILocation? Home { get; set; } = null;
    public bool HasHome() => Home != null;

    protected BaseVillager(Village village)
    {
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);

        WorldTimer.GetInstance().Subscribe((int hours, int minutes, int seconds, string guid) =>
        {
            Health += 20;
            if (Health > 100) Health = 100;
        }, WorldTimer.SubscribtionTypes.Day);
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} ({Age} years)";
    }
}