using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{

    public class FireflameWine : Item
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
            return 1;
        }
        public override int WhenDrank(Entity drinker)
        {
            drinker.ApplyCondition(new Burning(3), $"drinking {Name}");
            return 1;
        }
        public override void SelfDestruct()
        {
            base.SelfDestruct();
            Scene.GetRandomCreature().ApplyCondition(new Burning(3), $"{Name} splashing randomly");
        }

    }

}
