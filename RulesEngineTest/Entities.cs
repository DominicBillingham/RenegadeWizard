using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngineTest
{
    class Entity
    {
        public string Type { get; set; }  // e.g., "PLAYER" or "GOBLIN"

        public string Immunity { get; set; }

        public int Resolve { get; set; }

        public List<string> Modifiers { get; set; }


        // Entity properties (health, strength, etc.)
    }

    class Modifier
    {
        public string Name { get; set; }  // e.g., "BURNING", "ENLARGED"
                                          // Additional properties like duration, effect details, etc.
    }

}
