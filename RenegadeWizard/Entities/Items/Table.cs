using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities.Items
{
    public class Table : Item
    {
        public Table()
        {
            Name = "Table";
            Description = " # It has four legs, like a horse";
            Health = 3;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {

            if (thrower.Attributes?.Strength > 10)
            {
                target.ApplyDamage(4, $"thrown {Name}");
                return 1;
            }
            else
            {
                Console.WriteLine($"{thrower.Name} tries to throw {Name} but is not strong enough!");
                return 0;
            }
        }
    }
}
