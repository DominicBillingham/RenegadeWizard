using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures
{
    public class Demon : Creature
    {
        public Demon(string name)
        {
            Name = name;
            Description = "A very angry, pissed off demon";
            Health = 7;
            Weight = 15;
            Actions = new Actions(this);
            Attributes = new Attributes(12, 12, 12);
            Conditions.Add(new Burning(999));
        }

    }

}
