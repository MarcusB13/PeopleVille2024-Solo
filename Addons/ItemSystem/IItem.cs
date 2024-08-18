using PeopleVilleEngine;

namespace ItemSystem
{
    public interface IItem : IAddOn
    {
        public string ItemType { get; }
        public string Name { get; }
        public void Use (BaseVillager villager);
    }
}