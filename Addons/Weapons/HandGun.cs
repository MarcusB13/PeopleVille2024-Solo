using PeopleVilleEngine;

namespace ItemSystem.Weapons
{
    public class HandGun : IItem
    {
        public string ServiceName { get { return "Item"; } }
        public string ItemType { get { return "Weapon"; } }
        public string Name { get { return "Handgun"; } }
        private int _damage = -25;
        private Village _village;
        private RNG _rng;

        public void Execute(Village village)
        {
            _village = village;
            _rng = RNG.GetInstance();
        }

        public void Use(BaseVillager villager)
        {
            List<BaseVillager> villagers = _village.Villagers.Where(v => v != villager).ToList();
            BaseVillager target = villagers[_rng.Next(0, villagers.Count)];

            target.Health += _damage;
            Console.WriteLine($"{villager.ToString()} used {Name} on {target.ToString()}    -    {target.ToString()} health: {target.Health}");
            
            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.ToString()} died");
                _village.Villagers.Remove(target);
            }
        }
    }
}