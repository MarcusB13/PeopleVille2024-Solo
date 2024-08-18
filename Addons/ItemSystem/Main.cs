using PeopleVilleEngine;

namespace ItemSystem
{
    public class ItemSystem : IAddOn {
        public string ServiceName { get { return "ItemSystem"; } }
        private Village _village;
        private RNG _rng;
        private Dictionary<BaseVillager, Inventory> _inventories = new Dictionary<BaseVillager, Inventory>();

        public void Execute(Village village)
        {
            _rng = RNG.GetInstance();
            _village = village;
            foreach (BaseVillager villager in _village.Villagers)
            {
                _inventories.Add(villager, new Inventory(villager));
            }

            WorldTimer worldTimer = WorldTimer.GetInstance();
            worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
                if (hour % 2 != 0) return;
                BaseVillager villager = _village.Villagers[_rng.Next(0, _village.Villagers.Count)];
                UseRandomItem(villager);
            }, WorldTimer.SubscribtionTypes.Hour);

            BaseLoader.GetInstance().SubscribeToOnAddonsLoaded(AddStartItems);
        }

        public Inventory GetInventory(BaseVillager villager)
        {
            return _inventories[villager];
        }

        public void UseRandomItem(BaseVillager villager)
        {
            Inventory inventory = GetInventory(villager);
            if (inventory.Items.Count == 0) return;

            IItem item = inventory.Items[_rng.Next(0, inventory.Items.Count)];
            inventory.UseItem(item);
        }

        public void AddStartItems(){
            foreach (BaseVillager villager in _village.Villagers)
            {
                Inventory inventory = GetInventory(villager);
                List<IAddOn> sortedItems = _village.Addons.Where(addon => addon.ServiceName == "Item").ToList();
                List<IItem> items = (List<IItem>)sortedItems.Select(addon => (IItem)addon).ToList();
                if (villager.Age < 18){
                    items = items.Where(item => item.ItemType != "Weapon").ToList();
                }
                if (items.Count == 0) return;

                // Choose 2 random items
                List<IItem> randomItems = new List<IItem>();
                for (int i = 0; i < 2; i++)
                {
                    IItem item = (IItem)items[_rng.Next(0, items.Count)];
                    if (randomItems.Contains(item)) continue;
                    randomItems.Add(item);
                }

                foreach (IItem item in randomItems)
                {
                    if (villager.Age >= 18){
                        Console.WriteLine($"{villager.ToString()}  -  received {item.Name}");
                    } else {
                        string maleOrFemale = villager.IsMale ? "him" : "her";
                        Console.WriteLine($"{villager.ToString()}'s mom gave {maleOrFemale} {item.Name}");
                        continue;
                    }
                    inventory.AddItem(item);
                }
            }
        }

    }
}