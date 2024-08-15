using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items
{
    public class Grenade : Item
    {
        public Grenade()
        {
            Name = "Grenade";
            Description = "I think it's missing a piece at the top?";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {

            target.ApplyDamage(2, $"{Name} thrown by {thrower.Name}");
            foreach (var creature in Scene.GetCreatures())
            {
                creature.ApplyCondition(new Bleeding(2), Name);
            }
            Scene.Entities.Remove(this);
            return 1;
        }
    }

}
