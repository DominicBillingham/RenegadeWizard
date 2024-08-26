using RenegadeWizard.Modifiers;
using RenegadeWizard.Entities;
using RenegadeWizard.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Enums;

namespace RenegadeWizard.Components
{
    public class Interaction
    {
        public Entity Agent { get; set; }

        public int ActionThrow(Entity item, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} throws {item.Name} at {target.Name}");

            int actionCost = item.WhenThrown(target, Agent);

            Console.WriteLine("\n");
            return actionCost;

        }
        public int ActionConsume(Entity edibleItem)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} consumes {edibleItem.Name}");

            int actionCost = edibleItem.WhenConsumed(Agent);

            if (Agent == edibleItem)
            {
                Console.Write(Narrator.GetConfusedNarrator());
            }

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionInspect(Entity entity)
        {

            if (Agent.Modifiers.Any(con => con is Madness))
            {
                // idea: write some custom lines for inspecting while mad
            }

            int actionCost = entity.WhenInspected();
            Console.WriteLine("\n");
            return actionCost;
        }

        #region Synonyms
        public int ActionDrink(Entity edibleItem)
        {
            return ActionConsume(edibleItem);
        }
        public int ActionEat(Entity edibleItem)
        {
            return ActionConsume(edibleItem);
        }
        public int ActionToss(Entity item, Entity target)
        {
            return ActionThrow(item, target);
        }
        public int ActionInfo(Entity entity)
        {
            return ActionInspect(entity);
        }

        #endregion



        public int ActionFireball(Entity target)
        {

            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} casts a");

            var chaotic = Random.Shared.Next(2);

            if (chaotic == 1)
            {
                Console.Write(" chaotic fireball");
                var creatures = new EntQuery().SelectCreatures().GetAll();

                foreach (var creature in creatures)
                {
                    creature.ApplyCondition(new Burning(2), "fireball");
                }

            }

            if (chaotic == 0)
            {
                Console.Write(" focused fireball");
                target.ApplyCondition(new Burning(3), "fireball");
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionFleetingImmortality()
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} casts fleeting immortality");

            var creatures = new EntQuery().SelectCreatures().GetAll();

            foreach (var creature in creatures)
            {
                creature.ApplyCondition(new Immortal(1), "fleeting immortality");
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionConjureFriend() {

            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} conjures a friendly goblin");

            var joe = new Goblin("JoeTheFriendly");
            joe.Faction = Enums.Factions.Player;
            joe.Health = 3;
            Scene.Entities.Add(joe);

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }
        public int ActionCharm(Entity target)
        {

            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} casts charm goblin");

            target.ApplyCondition(new ChangedFaction(3, Agent.Faction), "charm spell");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionEnrage(Entity target) {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} casts enrage goblin");

            target.ApplyCondition(new ChangedFaction(3, Factions.None), "enrage spell");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;



        }


        public int ActionDisguiseSelf(Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} disguises as a goblin");

            Agent.ApplyCondition(new ChangedFaction(3, target.Faction), "disguise spell");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionTransferConds(Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} transfers their modifiers!");

            foreach (var mod in Agent.Modifiers)
            {
                target.ApplyCondition(mod, "transferCon");
            }

            Agent.Modifiers.Clear();

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;
        }

        public int ActionConvertCons()
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} converts their modifiers!");
            int healthCount = 0;

            foreach (var mod in Agent.Modifiers)
            {
                healthCount++;
            }

            Agent.Modifiers.Clear();

            Agent.ApplyHealing(healthCount, "conversion");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;
        }

        public int ActionChannelCons(Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} converts their modifiers!");
            int damageCount = 0;

            foreach (var mod in Agent.Modifiers)
            {
                damageCount++;
            }

            Agent.Modifiers.Clear();

            target.ApplyDamage(damageCount, "conversion");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;


        }

        public int ActionInvis()
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {Agent.Name} casts invis");

            var creatures = new EntQuery().SelectCreatures().GetAll();

            foreach (var creature in creatures)
            {
                creature.ApplyCondition(new Hidden(3), "invis spell");
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;
        }


        //public int ActionHeal()
        //{
        //    foreach (var mod in Agent.Modifiers)
        //    {
        //        damageCount++;
        //    }

        //    Agent.Modifiers.Clear();

        //    target.ApplyDamage(damageCount, "conversion");

        //    int actionCost = 1;
        //    Console.WriteLine("\n");
        //    return actionCost;

        //}



    }


}
