using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities;

public class Category : Entity
{
    public string Name { get; set; }

    public Guid? UserId { get; set; }
    public User User { get; set; }

    public Guid? CategoryId { get; set; }
    public Category CustomCategory { get; set; }

    public IEnumerable<Entrance> Entrances { get; set; }

    public double GetTotalValues() {
        if (Entrances == null || !Entrances.Any())
            return 0.0;

        return Entrances.Sum(e => e.Value);
    }
}
