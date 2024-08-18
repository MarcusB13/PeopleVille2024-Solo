using PeopleVilleEngine;
using Interactions;

namespace Jumping;

public class Jumping : IInteraction
{
    public string ServiceName { get { return "Interaction"; } }

    public void Execute(Village village)
    {
        return;
    }

    public void Run(BaseVillager villager)
    {
        int timeToRun = RNG.GetInstance().Next(2, 8);
        int timeSpent = 0;
        WorldTimer worldTimer = WorldTimer.GetInstance();

        Console.WriteLine($"{worldTimer.ToString()}  --  {villager.ToString()} started running");
        worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
            if (timeSpent <= timeToRun) {timeSpent++; return;}

            Console.WriteLine($"{worldTimer.ToString()}  --  {villager.ToString()} finished running");
            villager.IsBusy = false;
            worldTimer.Unsubscribe(id, WorldTimer.SubscribtionTypes.Minute);
        }, WorldTimer.SubscribtionTypes.Minute);
    }
}
