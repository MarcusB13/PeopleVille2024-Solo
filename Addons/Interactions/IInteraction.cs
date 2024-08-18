using PeopleVilleEngine;

namespace Interactions;

public interface IInteraction : IAddOn {
    public void Run(BaseVillager villager);
}
