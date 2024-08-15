using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{

    public class DemonInABottle : Drink
    {
        public DemonInABottle()
        {
            Name = "DemonInABottle";
            Description = "Seem's like a bad idea, and that's because IT IS";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyDamage(2, $"{Name} thrown by {thrower.Name}");
            SelfDestruct();
            return 1;
        }
        public override int WhenConsumed(Entity consume)
        {
            consume.ApplyHealing(2, $"consuming {Name}");
            consume.ApplyCondition(new Burning(3), $"consuming {Name}");
            consume.ApplyCondition(new Enlarged(3), $"consuming {Name}");
            Scene.Entities.Remove(this);
            return 1;
        }
        public override void SelfDestruct()
        {
            base.SelfDestruct();
            Console.Write("A demon has been released! | ");
            Scene.Entities.Add(new Demon("Demon"));
        }

    }

}
