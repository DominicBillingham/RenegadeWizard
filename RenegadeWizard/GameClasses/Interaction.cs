using RenegadeWizard.Components;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Creatures.Goblin;
using RenegadeWizard.Enums;
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
        public string Name { get; set; }
        public string Description { get; set; }
        public Entity Agent { get; set; }
        public List<Entity> Targets { get; set; }
        public bool IsActionPossible { get; set; } = true;
        private int Power { get; set; } = 0;
        private int DamageDealt { get; set; } = 0;

        private List<Action> ActionComponents = new();

        private List<Action> TargetComponents = new();

        public Interaction(Entity agent, string name)
        {
            Agent = agent;
            Name = name;
        }

        #region TargetMethods

        public Interaction SelectRandomEnemy()
        {
            TargetComponents.Add(() => 
            {
                Targets = new List<Entity>() { new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Agent.Faction).GetRandom() };
            });
            return this;
        }

        public Interaction SelectAllEnemies()
        {
            TargetComponents.Add(() => 
            {
                Targets = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Agent.Faction).GetAll();
            });
            return this;
        }

        public Interaction SelectAll()
        {
            TargetComponents.Add(() => 
            {
                Targets = new EntQuery().SelectCreatures().SelectLiving().GetAll();
            });
            return this;
        }

        public Interaction SelectRandom()
        {
            TargetComponents.Add(() =>
            {
                Targets = new List<Entity>() { new EntQuery().SelectCreatures().SelectLiving().GetRandom() };
            });
            return this;
        }

        public Interaction SelectSelf()
        {
            TargetComponents.Add(() =>
            {
                Targets = new List<Entity>() { Agent };
            });
            return this;
        }

        public Interaction SelectByName(int namesNeeded)
        {
            TargetComponents.Add(() =>
            {

                while (Targets.Count != namesNeeded) {

                    Console.Write($" ! [{Name}] requires");
                    for (int i = 0; i < namesNeeded; i++)
                    {
                        Console.Write($" [targetName{i + 1}]");
                    }
                    Console.WriteLine();

                    Console.Write(" > ");
                    var input = Console.ReadLine().ToLower().Split(" ")
                        .Where(x => x.Length > 2)
                        .ToArray();

                    if (input == null)
                    {
                        Console.WriteLine(" ! No words were found");
                        continue;
                    }

                    List<Entity> sceneEntities = new List<Entity>(Scene.Entities);
                    List<Entity> actionParameters = new();

                    foreach (var word in input)
                    {
                        if (actionParameters.Count == namesNeeded)
                        {
                            break;
                        }

                        var paramerter = sceneEntities.FirstOrDefault(x => x.Name.ToLower().Contains(word));
                        if (paramerter != null)
                        {
                            actionParameters.Add(paramerter);
                            sceneEntities.Remove(paramerter);
                        }
                    }

                    Targets = actionParameters;

                }

            });
            return this;
        }

        #endregion 

        public Interaction CalculatePower(int basePower)
        {







            Power = basePower;
            return this;
        }


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

                Agent.ApplyDamage(damage, Name);
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

                    entity.ApplyDamage(damage, Name);
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
                    entity.ApplyHealing(healing, Name);
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
                    entity.ApplyCondition(modifier, Name);
                }
            });
            return this;
        }

        public Interaction ConjureGoblin(Factions faction, string name = "JoeTheFriend")
        {
            ActionComponents.Add(() =>
            {
                var gobbo = new Goblin(name);
                gobbo.Faction = faction;
                Scene.Entities.Add(gobbo);
            });
            return this;
        }

        public Interaction ConjureDemon(string name = "GlorbTheDemon")
        {
            ActionComponents.Add(() =>
            {
                var demon = new Demon(name);
                Scene.Entities.Add(demon);
            });
            return this;
        }

        public Interaction RaiseDead()
        {
            ActionComponents.Add(() =>
            {
                foreach(var entity in Targets)
                {
                    if (entity.IsDestroyed)
                    {
                        entity.Health = 1;
                        entity.Faction = Agent.Faction;
                        entity.Name = $"Zombie{entity.Name}";
                    }
                    else
                    {

                    }
                }


            });
            return this;
        }

        public Interaction Polymorph()
        {

            ActionComponents.Add(() =>
            {
                List<Entity> polymorphedEntities = new List<Entity>();

                foreach (var entity in Targets)
                {
                    var next = Random.Shared.Next(3);

                    if (next == 0)
                    {
                        var newForm = new Sheep("Sheepy");
                        newForm.Faction = entity.Faction;
                        newForm.Name = $"{entity.Name}theSheep";
                        polymorphedEntities.Add(newForm);
                    }

                    if (next == 1)
                    {
                        var newForm = new Goblin("adasdsa");
                        newForm.Faction = entity.Faction;
                        newForm.Name = $"{entity.Name}theGoblin";
                        polymorphedEntities.Add(newForm);


                    }

                    if (next == 2)
                    {
                        var newForm = new Demon("adasdsa");
                        newForm.Faction = entity.Faction;
                        newForm.Name = $"{entity.Name}theDemon";
                        polymorphedEntities.Add(newForm);

                    }

                }

                Scene.Entities.AddRange(polymorphedEntities);
                Scene.Entities.RemoveAll(x => Targets.Contains(x));

            });
            return this;
        }

        public Interaction CauseExplosion()
        {
            ActionComponents.Add(() =>
            {
                foreach (var target in Targets)
                {
                    var totalDamage = target.Health;
                    var allEntities = new EntQuery().SelectCreatures().GetAll();

                    foreach (var entity in allEntities)
                    {
                        entity.ApplyDamage(totalDamage, Name);
                    }

                }

            });
            return this;
        }

        #region  RollFunctions
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

        #endregion

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

                foreach (var action in TargetComponents)
                {
                    action.Invoke();
                }

                Console.WriteLine();
                Console.Write($" # {Description} at");
                foreach (var target in Targets)
                {
                    Console.Write($" {target.Name},");
                }


                foreach (var action in ActionComponents)
                {
                    action.Invoke();
                }

                Console.WriteLine();
            }
        }
    }
}
