using RenegadeWizard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.Components
{
    public class Attributes
    {
        public Attributes(int strength, int agility, int intellect)
        {
            (Strength, Agility, Intellect) = (strength, agility, intellect);
        }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intellect { get; set; }

    }
}
