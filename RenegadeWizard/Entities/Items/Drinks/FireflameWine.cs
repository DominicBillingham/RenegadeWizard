using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{

    public class FireflameWine : Drink
    {
        public FireflameWine()
        {
            Name = "FireflameWine";
            Description = "Yeah of course I can handle th- OH GOD IT BURNS";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyCondition(new Burning(3), $"{Name} thrown by {thrower.Name}");
            Scene.Entities.Remove(this);
            return 1;
        }
        public override int WhenConsumed(Entity consume)
        {
            consume.ApplyCondition(new Burning(3), $"consuming {Name}");
            Scene.Entities.Remove(this);
            return 1;
        }


    }

}
