using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items
{
    public class Knife : Item
    {
        public Knife()
        {
            Name = "Knife";
            Description = "It's a serrated bread knife, covered in butter?";
            Health = 1;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {
            target.ApplyDamage(1, $"{Name} thrown by {thrower.Name}");
            target.ApplyCondition(new Bleeding(2), Name);
            Scene.Entities.Remove(this);
            return 1;
        }
    }
}
