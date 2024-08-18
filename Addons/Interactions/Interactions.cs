using System.Runtime.CompilerServices;
using PeopleVilleEngine;

namespace Interactions;

public class InteractionSystem : IAddOn 
{
    private Village _village;
    public string ServiceName { get { return "InteractionSystem"; } }
    static private InteractionSystem _interactionSystem;

    public static InteractionSystem GetInstance() {
        if (_interactionSystem == null) {
            _interactionSystem = new InteractionSystem();
        }
        return _interactionSystem;
    }

    public void Execute(Village village) {
        _village = village;
        RunInteractions();
    }

    private void RunInteractions(){
        WorldTimer worldTimer = WorldTimer.GetInstance();
        RNG rng = RNG.GetInstance();

        worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
            if (minute == 30 || minute == 0 || minute == 15 || minute == 45){
                // Load Interaction Addons
                List<IAddOn> _interactions = new List<IAddOn> ();

                List<IAddOn> Addon = _village.Addons.Where(addon => addon.ServiceName == "Interaction").ToList();
                foreach (IAddOn addon in Addon) {
                    _interactions.Add(addon);
                }

                if (_interactions.Count > 0){
                    BaseVillager randomVillager = _village.Villagers[rng.Next(0, _village.Villagers.Count + 1)];
                    IAddOn interaction = _interactions[rng.Next(0, _interactions.Count + 1)];

                    // Don't run interactions if the villager is busy. Find a new villager
                    while (randomVillager.IsBusy){
                        randomVillager = _village.Villagers[rng.Next(0, _village.Villagers.Count + 1)];
                        Thread.Sleep(100);
                    }

                    randomVillager.IsBusy = true;
                    IInteraction interactionToRun = (IInteraction)interaction;
                    interactionToRun.Run(randomVillager);
                }
            }
        }, WorldTimer.SubscribtionTypes.Minute);
    }

}