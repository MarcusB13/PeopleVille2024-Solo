using PeopleVilleEngine;
Console.WriteLine("PeopleVille");

WorldTimer worldTimer = WorldTimer.GetInstance();

//Create village
var village = new Village();
Console.WriteLine(village.ToString());


//Print locations with villagers to screen
foreach (var location in village.Locations)
{
    var locationStatus = location.Name;
    foreach(var villager in location.Villagers().OrderByDescending(v => v.Age))
    {
        locationStatus += $" {villager}";
    }
    Console.WriteLine(locationStatus);
}



while (true){
    Console.WriteLine(worldTimer.ToString());
    Thread.Sleep(1000);
}
