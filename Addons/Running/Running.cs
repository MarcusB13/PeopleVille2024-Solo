using PeopleVilleEngine;
using Interactions;

namespace Running;

public class Running : IInteraction
{
    public string ServiceName { get { return "Interaction"; } }

    public void Execute(Village village)
    {
        return;
    }

    public void Run(BaseVillager villager)
    {
        int timeToRun = RNG.GetInstance().Next(4, 10);
        int timeSpent = 0;
        WorldTimer worldTimer = WorldTimer.GetInstance();

        Console.WriteLine($"{worldTimer.ToString()}  --  {villager.ToString()} started jumping arround");
        worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
            if (timeSpent <= timeToRun) {timeSpent++; return;}

            Console.WriteLine($"{worldTimer.ToString()}  --  {villager.ToString()} is starting to relax again");
            villager.IsBusy = false;
            worldTimer.Unsubscribe(id, WorldTimer.SubscribtionTypes.Minute);
        }, WorldTimer.SubscribtionTypes.Minute);
    }
}
