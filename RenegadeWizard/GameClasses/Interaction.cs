using RenegadeWizard.Components;
using RenegadeWizard.Entities;
using RenegadeWizard.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RenegadeWizard.GameClasses
{
    public class Interaction
    {
        public string Action { get; set; }
        public string Description { get; set; }
        public Entity Agent { get; set; }
        public List<Entity> Targets { get; set; }
        public bool IsActionPossible { get; set; } = true;
        private int Power { get; set; } = 0;
        private int DamageDealt { get; set; } = 0;

        private List<Action> ActionComponents = new();

        public Interaction(Entity agent, string name)
        {
            Agent = agent;
            Action = name;
        }

        #region TargetMethods

        public Interaction SelectRandomEnemy()
        {
            ActionComponents.Add(() => 
            {
                Targets = new List<Entity>() { new EntQuery().SelectCreatures().SelectHostiles(Agent.Faction).GetRandom() };
            });
            return this;
        }

        public Interaction SelectAllEnemies()
        {
            ActionComponents.Add(() => 
            {
                Targets = new EntQuery().SelectCreatures().SelectHostiles(Agent.Faction).GetAll();
            });
            return this;
        }

        public Interaction SelectAll()
        {
            ActionComponents.Add(() => 
            {
                Targets = new EntQuery().SelectCreatures().GetAll();
            });
            return this;
        }

        public Interaction SelectRandom()
        {
            ActionComponents.Add(() =>
            {
                Targets = new List<Entity>() { new EntQuery().SelectCreatures().GetRandom() };
            });
            return this;
        }

        public Interaction SelectSelf()
        {
            ActionComponents.Add(() =>
            {
                Targets = new List<Entity>() { Agent };
            });
            return this;
        }

        public Interaction SelectByName(string[] input)
        {
            Console.Write("[Enter target name]");

            List<Entity> sceneEntities = new List<Entity>(Scene.Entities);
            List<Entity> actionParameters = new();

            foreach (var word in input)
            {
                var paramerter = sceneEntities.FirstOrDefault(x => x.Name.ToLower().Contains(word));
                if (paramerter != null)
                {
                    actionParameters.Add(paramerter);
                    sceneEntities.Remove(paramerter);
                }
            }

            Targets = actionParameters;

            return this;
        }

        #endregion 




        public Interaction DamageSelf(int damage)
        {
            ActionComponents.Add(() =>
            {

                var wounded = Agent.Modifiers.FirstOrDefault(con => con is Wounded);
                damage = wounded?.ModifyDamageTaken(damage) ?? damage;

                var protection = Agent.Modifiers.FirstOrDefault(con => con is Protected);
                damage = protection?.ModifyDamageTaken(damage) ?? damage;

                var immortal = Agent.Modifiers.FirstOrDefault(con => con is Immortal);
                damage = immortal?.ModifyDamageTaken(damage) ?? damage;

                Agent.ApplyDamage(damage, Action);
                Agent.WhenDamaged();
                
            });
            return this;
        }

        public Interaction ApplyDamage(int damage)
        {
            ActionComponents.Add(() => 
            {
                foreach (var entity in Targets)
                {
                    var wounded = entity.Modifiers.FirstOrDefault(con => con is Wounded);
                    damage = wounded?.ModifyDamageTaken(damage) ?? damage;

                    var protection = entity.Modifiers.FirstOrDefault(con => con is Protected);
                    damage = protection?.ModifyDamageTaken(damage) ?? damage;

                    var immortal = entity.Modifiers.FirstOrDefault(con => con is Immortal);
                    damage = immortal?.ModifyDamageTaken(damage) ?? damage;

                    entity.ApplyDamage(damage, Action);
                    entity.WhenDamaged();
                    DamageDealt += damage;
                }
            });
            return this;
        }

        public Interaction Lifesteal()
        {
            Agent.ApplyHealing(DamageDealt);
            return this;
        }

        public Interaction ApplyHealing(int healing)
        {
            ActionComponents.Add(() =>
            {
                foreach (var entity in Targets)
                {
                    entity.ApplyHealing(healing, Action);
                }
            });
            return this;
        }

        public Interaction ApplyCondition(Modifier modifier)
        {
            ActionComponents.Add(() =>
            {
                foreach (var entity in Targets)
                {
                    entity.ApplyCondition(modifier, Action);
                }
            });
            return this;
        }


        private int RollDice(int dice, int diceSize, int advantage = 0, int disadvantage = 0)
        {
            int totalAdv = Math.Abs( advantage - disadvantage);

            if (advantage > disadvantage)
            {
                int highest = 0;

                for(int i = 0; i < totalAdv; i++)
                {
                    int current = Roll(dice, diceSize);
                    if (current > highest)
                    {
                        highest = current;
                    }
                }

                return highest;

            } 
            else
            {
                int lowest = 0;

                for (int i = 0; i < totalAdv; i++)
                {
                    int current = Roll(dice, diceSize);
                    if (current < lowest)
                    {
                        lowest = current;
                    }
                }

                return lowest;
            }

        }

        private int Roll(int dice, int diceSize)
        {
            int total = 0;
            for (int j = 0; j < dice; j++)
            {
                total += Random.Shared.Next(diceSize) + 1;
            }
            return total;
        }

        #region Attributes

        public Interaction CheckIntellect(int requirement)
        {
            ActionComponents.Add(() => 
            {
                int intellect = Agent.Attributes?.Intellect ?? 0;

                var exhausted = Agent.Modifiers.FirstOrDefault(con => con is Exhausted);
                intellect = exhausted?.ModifyIntellect(intellect) ?? intellect;

                if (intellect < requirement)
                {
                    IsActionPossible = false;
                }
            });
            return this;
        }

        public Interaction CheckStrength(int requirement)
        {
            ActionComponents.Add(() => 
            {
                int strength = Agent.Attributes?.Strength ?? 0;

                var exhausted = Agent.Modifiers.FirstOrDefault(con => con is Exhausted);
                strength = exhausted?.ModifyStrength(strength) ?? strength;

                var enlarged = Agent.Modifiers.FirstOrDefault(con => con is Enlarged);
                strength = enlarged?.ModifyStrength(strength) ?? strength;

                if (strength < requirement)
                {
                    IsActionPossible = false;
                }
            });
            return this;
        }

        #endregion

        #region UtilityFunctions
        // 

        public Interaction Inspect()
        {

            ActionComponents.Add(() =>
            {
                foreach (var entity in Targets)
                {
                    Console.Write($" # {entity.Name}");

                    if (entity.Attributes != null)
                    {
                        Console.Write($" | STR:{entity.Attributes.Strength}, AGI:{entity.Attributes.Agility}, INT:{entity.Attributes.Intellect} |");
                    }

                    Console.Write($" {entity.Description}");

                    if (entity.BattleLog != string.Empty)
                    {
                        Console.WriteLine();
                        Console.Write(" #" + entity.BattleLog);
                    }

                }

            });
            return this;

        }

        #endregion

        public void Execute()
        {
            // Can check for a valid amount of names here!

            if (IsActionPossible)
            {
                Console.Write(Description);

                foreach (var action in ActionComponents)
                {

                    action.Invoke();
                }
            }
        }
    }
}
