using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items
{
    public class StickOfButter : Item
    {
        public StickOfButter()
        {
            Name = "StickOfButter";
            Description = " # Why does it have a bite mark!?";
            Health = 1;
            Conditions.Add(new Slippery(999));
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyDamage(1, $"thrown {Name}");
            target.ApplyCondition(new Slippery(3), Name);
            return 1;
        }

    }

}
