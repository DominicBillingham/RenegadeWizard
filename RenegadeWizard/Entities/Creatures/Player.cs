using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures
{
    public class Player : Creature
    {
        public Player(string name)
        {
            Name = name;
            Health = 10;
            Description = " # Admiring yourself I see?";
            Actions = new Actions(this);
            Attributes = new Attributes(10, 10, 10);
        }

    }

}
