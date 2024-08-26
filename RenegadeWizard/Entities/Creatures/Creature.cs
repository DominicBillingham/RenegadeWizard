using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Entities.Creatures
{
    public class Creature : Entity
    {

        public override int WhenConsumed(Entity consumer)
        {
            if (consumer.Attributes?.Strength < Weight)
            {
                Console.Write($" {Narrator.GetContrastWord()} {Name} is too large to be eaten!");
                return 1;
            }

            Console.Write($" {Name} is devoured by {consumer.Name}!");
            SelfDestruct();
            return 1;

        }

        public override int WhenThrown(Entity target, Entity thrower)
        {
            ApplyDamage(1, "being thrown");
            target.ApplyDamage(2, $"thrown {Name}");
            return 1;
        }


        public override void SelfDestruct()
        {
            Console.Write($" {Name} has died!");
            Scene.Entities.Remove(this);
        }

    }



}
