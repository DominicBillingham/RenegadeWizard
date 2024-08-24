using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
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
        public override int WhenGrabbed(Entity grabber)
        {

            if (grabber.Attributes?.Strength < Attributes?.Agility)
            {
                Console.Write($" {Narrator.GetContrastWord()} {Name} is too fast.");
                return 1;
            }

            if (grabber.Attributes?.Strength < Weight)
            {
                Console.Write($" {Narrator.GetContrastWord()} {Name} is too heavy.");
                return 1;
            }

            grabber.HeldObject = this;
            return 1;
        }

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
        public override int WhenKicked(Entity kicker)
        {

            if (kicker.Attributes?.Strength > Attributes?.Strength)
            {
                var item = Scene.GetRandomItem();

                Console.Write($" causing {Name} to crash into {item.Name}");

                ApplyDamage(2, $"being kicked by {kicker.Name}");
                item.ApplyDamage(2, $"{Name} crashing into it");
            }
            else
            {
                Console.Write($" {Narrator.GetContrastWord()} {Name} is too strong!");
            }

            return 1;
        }

        public override void SelfDestruct()
        {
            Console.Write($" {Name} has died!");
            Scene.Entities.Remove(this);
        }

    }



}
