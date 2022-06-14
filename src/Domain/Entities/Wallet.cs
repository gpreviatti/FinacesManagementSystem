using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Wallet : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double CurrentValue { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CloseDate { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public WalletType WalletType { get; set; }
    public Guid WalletTypeId { get; set; }

    public IEnumerable<Entrance> Entrances { get; set; }

    public void UpdateValue(int type, double value)
    {
        switch (type)
        {
            case (int)EntranceType.Income:
                CurrentValue += value;
                break;
            case (int)EntranceType.Expanse:
                if (value > CurrentValue)
                    throw new Exception("Insuficient founds");

                CurrentValue -= value;
                break;
            default:
                throw new ArgumentException("Type not found");
        }
    }
}
