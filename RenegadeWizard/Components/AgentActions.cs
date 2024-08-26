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
    public class AgentActions
    {
        public int ActionThrow(Entity agent, Entity item, Entity target)
        {
            if (item == null || target == null)
            {
                Console.WriteLine($" # {Narrator.GetConnectorWord()} {agent.Name} has nothing to throw!");
                return 0;
            }

            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} throws {item.Name} at {target.Name}");

            int actionCost = item.WhenThrown(target, agent);

            Console.WriteLine("\n");
            return actionCost;

        }
        public int ActionConsume(Entity agent, Entity edibleItem)
        {
            if (edibleItem == null)
            {
                Console.WriteLine($" # {Narrator.GetConnectorWord()} {agent.Name} is hungry but there's no food!");
                return 0;
            }

            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} consumes {edibleItem.Name}");

            int actionCost = edibleItem.WhenConsumed(agent);

            if (agent == edibleItem)
            {
                Console.Write(Narrator.GetConfusedNarrator());
            }

            Console.WriteLine("\n");
            return actionCost;
        }
        public int ActionInspect(Entity agent, Entity entity)
        {
            int actionCost = entity.WhenInspected();
            Console.WriteLine("\n");
            return actionCost;
        }

        #region Synonyms
        public int ActionDrink(Entity agent, Entity edibleItem)
        {
            return ActionConsume(agent, edibleItem);
        }
        public int ActionInfo(Entity agent, Entity entity)
        {
            return ActionInspect(agent, entity);
        }

        #endregion

        public int ActionCharmMonster(Entity agent, Entity target)
        {

            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} casts charm monster");

            target.ApplyCondition(new ChangedFaction(3, agent.Faction), "Charm");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionTitanBrawl(Entity agent)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} casts titan brawl");

            var creatures = new EntQuery().SelectCreatures().GetAll();

            foreach (var creature in creatures)
            {
                creature.ApplyCondition(new Enlarged(3), "Titan Brawl");
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionFireball(Entity agent, Entity target)
        {

            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} casts a chaotic fireball");

            var creatures = new EntQuery().SelectCreatures().GetAll();

            foreach (var creature in creatures)
            {
                creature.ApplyCondition(new Burning(2), "fireball");
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionFleetingImmortality(Entity agent)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} casts fleeting immortality");

            var creatures = new EntQuery().SelectCreatures().GetAll();

            foreach (var creature in creatures)
            {
                creature.ApplyCondition(new Immortal(1), "fleeting immortality");
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionConjureFriend(Entity agent)
        {

            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} conjures a friendly goblin");

            var joe = new Goblin("FriendlyJimmy");
            joe.Faction = Enums.Factions.Player;
            joe.Health = 3;
            Scene.Entities.Add(joe);

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionEnrage(Entity agent, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} casts enrage goblin");

            target.ApplyCondition(new ChangedFaction(3, Factions.None), "enrage spell");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }


        public int ActionDisguiseAs(Entity agent, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} disguises as a goblin");

            agent.ApplyCondition(new ChangedFaction(3, target.Faction), "disguise spell");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }

        public int ActionTransferConditions(Entity agent, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} transfers their modifiers!");

            foreach (var mod in agent.Modifiers)
            {
                target.ApplyCondition(mod, "transferCon");
            }

            agent.Modifiers.Clear();

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;
        }

        public int ActionConvertCons(Entity agent)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} converts their modifiers!");
            int healthCount = 0;

            foreach (var mod in agent.Modifiers)
            {
                healthCount++;
            }

            agent.Modifiers.Clear();

            agent.ApplyHealing(healthCount, "conversion");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;
        }

        public int ActionChannelCons(Entity agent, Entity target)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} converts their modifiers!");
            int damageCount = 0;

            foreach (var mod in agent.Modifiers)
            {
                damageCount++;
            }

            agent.Modifiers.Clear();

            target.ApplyDamage(damageCount, "conversion");

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;


        }

        public int ActionInvisibility(Entity agent)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} casts invis");

            var creatures = new EntQuery().SelectCreatures().GetAll();

            foreach (var creature in creatures)
            {
                creature.ApplyCondition(new Hidden(3), "invis spell");
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;
        }


        public int ActionHeal(Entity agent)
        {
            Console.Write($" # {Narrator.GetConnectorWord()} {agent.Name} casts mass heal");

            var creatures = new EntQuery().SelectCreatures().GetAll();

            foreach (var creature in creatures)
            {
                creature.ApplyHealing(4);
            }

            int actionCost = 1;
            Console.WriteLine("\n");
            return actionCost;

        }



    }


}
