using PeopleVilleEngine;

namespace ItemSystem.Weapons
{
    public class Bag : IItem
    {
        public string ServiceName { get { return "Item"; } }
        public string ItemType { get { return "Bag"; } }
        public string Name { get { return "Gucci"; } }
        private Village _village;
        private RNG _rng;
        private WorldTimer _worldTimer;

        public void Execute(Village village)
        {
            _village = village;
            _rng = RNG.GetInstance();
            _worldTimer = WorldTimer.GetInstance();
        }

        public void Use(BaseVillager villager)
        {
            Console.WriteLine($"{_worldTimer.ToString()}  --  {villager.ToString()} is looking in their {Name} {ItemType}");
        }
    }
}