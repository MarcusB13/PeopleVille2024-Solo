using PeopleVilleEngine;

namespace BankSystem;

public class BankSystem : IAddOn
{
    public string ServiceName {get { return "BankSystem"; } }
    private Village _village;
    public Village village { get { return _village; } }

    private List<Account> _accounts = new List<Account>();

    public void Execute (Village village)
    {
        _village = village;

        foreach (BaseVillager villager in village.Villagers)
        {
            _accounts.Add(new Account(villager));
        }

        WorldTimer worldTimer = WorldTimer.GetInstance();
        worldTimer.Subscribe(MakeInterest, WorldTimer.SubscribtionTypes.Day);

        worldTimer.Subscribe((int hour, int minute, int seconds, string id) => {
            if (hour % 5 > 0) {return;}
            foreach (Account account in _accounts)
            {
                Console.WriteLine($"Account {account.AccountNumber} - {account.Villager.FirstName} {account.Villager.LastName} - Balance: {account.Balance}");
            }
        }, WorldTimer.SubscribtionTypes.Hour);
    }

    public int? withdraw(BaseVillager villager, int amount)
    {
        Account account = _accounts.Find(a => a.Villager == villager);
        if (account == null)
        {
            return null;
        }

        return account.withdraw(amount);
    }

    public void deposit(BaseVillager villager, int amount)
    {
        Account account = _accounts.Find(a => a.Villager == villager);
        if (account == null)
        {
            return;
        }

        account.deposit(amount);
    }

    public int? getBalance(BaseVillager villager)
    {
        Account account = _accounts.Find(a => a.Villager == villager);
        if (account == null)
        {
            return null;
        }

        return account.Balance;
    }

    public void GiveSalery(BaseVillager villager, int salary)
    {
        Account account = _accounts.Find(a => a.Villager == villager);
        if (account == null)
        {
            return;
        }

        Console.WriteLine($"Salery of {salary} added to account {account.AccountNumber} - {account.Villager.FirstName} {account.Villager.LastName} - Balance: {account.Balance}");
        account.deposit(salary);
    }


    private void MakeInterest(int hours, int minutes, int seconds, string guid)
    {
        foreach (Account account in _accounts)
        {
            int interest = (int)(account.Balance * account.InterestRate);
            Console.WriteLine($"Interest of {interest} added to account {account.AccountNumber} - {account.Villager.FirstName} {account.Villager.LastName} - Balance: {account.Balance}");
            account.deposit(interest);
        }
    }
}
