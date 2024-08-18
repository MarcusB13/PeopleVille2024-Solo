using PeopleVilleEngine;

namespace ItemSystem.Weapons
{
    public class Phone : IItem
    {
        public string ServiceName { get { return "Item"; } }
        public string ItemType { get { return "Phone"; } }
        public string Name { get { return "Iphone 15 Pro"; } }
        private Village _village;
        private RNG _rng;
        private WorldTimer _worldTimer;

        private List<string> _phoneApps = new List<string> {
            "Messages",
            "Phone",
            "Camera",
            "Maps",
            "Calendar",
            "Weather",
            "Clock",
            "Notes",
            "Reminders",
            "Stocks",
            "News",
            "Health",
            "Wallet",
            "Settings"
        };
 
        public void Execute(Village village)
        {
            _village = village;
            _rng = RNG.GetInstance();
            _worldTimer = WorldTimer.GetInstance();
        }

        public void Use(BaseVillager villager)
        {
            string app = _phoneApps[_rng.Next(0, _phoneApps.Count)];
            Console.WriteLine($"{_worldTimer.ToString()}  --  {villager.ToString()} is opening the {app} app on their {Name}");

            int timeToUsePhone = _rng.Next(5, 25);
            int timeSpent = 0;
            _worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
                if (timeSpent < timeToUsePhone) {timeSpent++; return;}
                
                Console.WriteLine($"{_worldTimer.ToString()}  --  {villager.ToString()} turned off their phone");
                _worldTimer.Unsubscribe(id, WorldTimer.SubscribtionTypes.Minute);
            }, WorldTimer.SubscribtionTypes.Minute);
        }
    }
}