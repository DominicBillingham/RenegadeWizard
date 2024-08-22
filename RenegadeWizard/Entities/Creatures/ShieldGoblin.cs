//using RenegadeWizard.Components;
//using RenegadeWizard;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RenegadeWizard.Enums;
//using RenegadeWizard.Conditions;
//using RenegadeWizard.GameClasses;

//namespace RenegadeWizard.Entities.Creatures
//{
//    public class ShieldGoblin : Creature
//    {
//        public ShieldGoblin(string name)
//        {
//            Name = name;
//            Description = "As strong as they are dumb";
//            Health = 5;
//            Weight = 12;
//            CharacterActions = new ShieldGoblinActions();
//            Attributes = new Attributes(12, 3, 3);
//            Faction = Factions.Goblin;
//        }

//        public override void TakeTurn()
//        {

//        }

//    }

//    public class ShieldGoblinActions : Actions
//    {
//        public override void TakeTurn(Entity entity)
//        {
//            if (Random.Shared.Next(2) == 0)
//            {
//                var ally = Scene.GetRandomAlly(entity.Faction);
//                ActionShieldAllies(entity, ally);

//            }
//            else
//            {
//                var enemy = Scene.GetRandomHostile(Faction);
//                ActionKick(this, enemy);
//            }

//        }

//        public int ActionShieldAllies(Entity shielder, Entity ally)
//        {
//            Console.Write($" # {Narrator.GetConnectorWord()} {shielder.Name} valiantly protects {ally.Name}");
//            ally.ApplyCondition(new Protected(2), $"{shielder.Name}");
//            Console.WriteLine("\n");
//            return 1;
//        }
//    }


//}
