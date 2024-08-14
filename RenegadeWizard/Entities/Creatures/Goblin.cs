using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures
{
    public class Goblin : Creature
    {
        public Goblin(string name)
        {
            Name = name;
            Description = "A weak, silly and clumsy goblin";
            Health = 5;
            Actions = new Actions(this);
            Attributes = new Attributes(5, 5, 5);
        }

    }

}
