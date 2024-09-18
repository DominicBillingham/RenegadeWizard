using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Modifiers;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.Entities
{
    public class Clutter : Item
    {
        public Clutter(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
