using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{
    public class TitanStout : Drink
    {
        public TitanStout()
        {
            Name = "TitanStout";
            Description = "It's time to over compensate!";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyCondition(new Enlarged(3), $"{Name} thrown by {thrower.Name}");
            Scene.Entities.Remove(this);
            return 1;
        }
        public override int WhenConsumed(Entity consumer)
        {
            consumer.ApplyCondition(new Enlarged(3), $"consuming {Name}");
            Scene.Entities.Remove(this);
            return 1;
        }
        public override void SelfDestruct()
        {
            base.SelfDestruct();
        }
    }
}
