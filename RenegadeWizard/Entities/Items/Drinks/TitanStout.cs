using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items.Drinks
{
    public class TitanStout : Item
    {
        public TitanStout()
        {
            Name = "TitanStout";
            Description = " # It's time to over compensate!";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyCondition(new Enlarged(3), Name);
            return 1;
        }
        public override int WhenDrank(Entity drinker)
        {
            drinker.ApplyCondition(new Enlarged(3), Name);
            return 1;
        }
    }
}
