using System.Runtime.CompilerServices;
using PeopleVilleEngine;

namespace BankSystem;

internal class Account
{
    private BaseVillager _villager;
    internal BaseVillager Villager { get { return _villager; } }

    private int _balance;
    internal int Balance { get { return _balance; } }

    private Guid accountNumber;
    public Guid AccountNumber { get { return accountNumber; } }

    private double _interestRate;
    internal double InterestRate { get { return _interestRate; } }


    public Account(BaseVillager villager)
    {
        RNG rng = RNG.GetInstance();
        
        _villager = villager;
        _balance = rng.Next(100, 1000);
        accountNumber = Guid.NewGuid();
        _interestRate = (double) rng.Next(1, 10) / 100;
    }

    internal int withdraw(int amount) {
        if (amount > _balance)
        {
            return 0;
        }
        else
        {
            _balance -= amount;
            return amount;
        }
    }

    internal void deposit(int amount) {
        _balance += amount;
    }
}
