using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Modifiers;
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
            Console.Write(" causing shrapnel to fly everywhere!");
            target.ApplyDamage(3, $"{Name} thrown by {thrower.Name}");

            var creatures = new EntQuery().SelectCreatures().GetAll();
            foreach (var creature in creatures)
            {
                creature.ApplyDamage(1, Name);
            }

            Scene.Entities.Remove(this);
            return 1;
        }
    }

}
