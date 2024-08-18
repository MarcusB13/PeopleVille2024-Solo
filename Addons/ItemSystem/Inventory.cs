using PeopleVilleEngine;

namespace ItemSystem
{
    public class Inventory
    {
        private List<IItem> _items;
        public List<IItem> Items { get { return _items; } }
        private BaseVillager _villager;

        public Inventory(BaseVillager villager)
        {
            _villager = villager;
            _items = new List<IItem>();
        }

        public void AddItem(IItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
        }

        public void UseItem(IItem item)
        {
            item.Use(_villager);
        }
    }
}