using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Conditions;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities
{
    public class Item : Entity
    {
        public override int WhenGrabbed(Entity grabber)
        {
            if (Conditions.Any(con => con is Slippery))
            {
                Console.Write($"{Narrator.GetContrastWord} {Name} too slippery!");
                return 1;
            }

            grabber.HeldObject = this;
            return 1;
        }

        public override int WhenKicked(Entity kicker)
        {
            ApplyDamage(2, $"kicked by {kicker.Name}");
            return 1;
        }

    }

}
