﻿using System;
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
            Description = "It has four legs, like a horse";
            Health = 3;
        }
        public override int WhenThrown(Entity target, Entity thrower)
        {


            Console.Write($" {Narrator.GetContrastWord()} {thrower.Name} is not strong enough!");
            return 0;

        }
    }
}
