using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items
{
    public class Chandelier : Item
    {
        public Chandelier()
        {
            Name = "Chandelier";
            Description = "I sure hope it doesn't fall on anyone!";
            Health = 3;
        }

        public override void SelfDestruct()
        {
            Scene.GetRandomCreature().ApplyDamage(5, $"the {Name} falling");
            base.SelfDestruct();
        }
    }
}
