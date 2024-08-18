using PeopleVilleEngine;
using BankSystem;

namespace JobSystem;

public class JobSystem : IAddOn
{
    public string ServiceName { get { return "JobSystem"; } }
    private int _timeTowork = 7;
    private Village _village;

    public void Execute(Village village)
    {
        _village = village;

        WorldTimer worldTimer = WorldTimer.GetInstance();
        worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
            if (hour != 8) {return;}
            foreach (BaseVillager villager in village.Villagers)
            {
                if (villager.IsBusy) {continue;}
                villager.IsBusy = true;
                Console.WriteLine($"{worldTimer.ToString()}  --  {villager.ToString()} started working");

                int timeSpent = 0;
                worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
                    if (timeSpent < _timeTowork) {timeSpent++; return;}

                    Console.WriteLine($"{worldTimer.ToString()}  --  {villager.ToString()} finished working");
                    TryGiveSalery(villager);
                    villager.IsBusy = false;

                    worldTimer.Unsubscribe(id, WorldTimer.SubscribtionTypes.Hour);
                }, WorldTimer.SubscribtionTypes.Hour);
            }
        }, WorldTimer.SubscribtionTypes.Hour);
    }

    private void TryGiveSalery(BaseVillager villager) {
        IAddOn bankSystem = _village.Addons.Find(addon => addon.ServiceName == "BankSystem");
        if (bankSystem == null) {return;}

        BankSystem.BankSystem bank = (BankSystem.BankSystem)bankSystem;
        bank.GiveSalery(villager, RNG.GetInstance().Next(300, 1000));
    }
}
