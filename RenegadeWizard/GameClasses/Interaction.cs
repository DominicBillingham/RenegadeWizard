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
using static System.Net.Mime.MediaTypeNames;

namespace RenegadeWizard.GameClasses
{
    public class Interaction
    {
        public string Name { get; set; }
        public List<string> Synonyms { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        public Entity Agent { get; set; }
        public List<Entity> Targets { get; set; } = new();
        private int Power { get; set; } = 0;
        private int DamageDealt { get; set; } = 0;
        public bool RequiresTargets { get; set; } = false;
        public bool FreeAction { get; set; } = false;
        public bool IsSpell { get; set; } = false;

        private List<Action> ActionComponents = new();

        private List<Action> TargetComponents = new();
        public Interaction? FollowupInteraction { get; set; } = null;

        public Interaction(Entity agent, string name)
        {
            Agent = agent;
            Name = name;
        }

        public Interaction CalculatePower(int basePower)
        {

            Power = basePower;
            return this;
        }

        #region TargetMethods

        // These (target) methods is semi-repeating EntQuery functionality, might be worth keeping an eye for a refactor

        public Interaction SelectRandomEnemy()
        {
            RequiresTargets = true;
            TargetComponents.Add(() => 
            {
                var target = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Agent.Faction).GetRandom();
                if (target != null) 
                { 
                    Targets.Add(target);
                }
            });
            return this;
        }

        public Interaction SelectRandomAlly()
        {
            RequiresTargets = true;
            TargetComponents.Add(() =>
            {
                var target = new EntQuery().SelectCreatures().SelectLiving().SelectAllies(Agent.Faction).GetRandom();
                if (target != null)
                {
                    Targets.Add(target);
                }

            });
            return this;
        }

        public Interaction SelectRandomDeadAlly()
        {
            RequiresTargets = true;
            TargetComponents.Add(() =>
            {
                var target = new EntQuery().SelectCreatures().SelectDead().SelectAllies(Agent.Faction).GetRandom();
                if (target != null)
                {
                    Targets.Add(target);
                }
            });
            return this;
        }

        public Interaction SelectAllEnemies()
        {
            RequiresTargets = true;
            TargetComponents.Add(() => 
            {
                Targets = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Agent.Faction).GetAll();
            });
            return this;
        }

        public Interaction SelectAll()
        {
            RequiresTargets = true;
            TargetComponents.Add(() => 
            {
                Targets = new EntQuery().SelectCreatures().SelectLiving().GetAll();
            });
            return this;
        }

        public Interaction SelectRandom()
        {
            RequiresTargets = true;
            TargetComponents.Add(() =>
            {
                var target = new EntQuery().SelectCreatures().SelectLiving().GetRandom();
                if (target != null)
                {
                    Targets.Add(target);
                }
            });
            return this;
        }

        public Interaction SelectDeadCreatures()
        {
            RequiresTargets = true;
            TargetComponents.Add(() =>
            {
                Targets = new EntQuery().SelectCreatures().SelectDead().GetAll();
            });
            return this;
        }

        public Interaction SelectSelf()
        {
            RequiresTargets = true;
            TargetComponents.Add(() =>
            {
                Targets.Add(Agent);
            });
            return this;
        }

        public Interaction SelectByName(int namesNeeded)
        {
            RequiresTargets = true;
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

        #region ActionComponents

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
                Agent.WhenDamaged(this);
                
            });
            return this;
        }

        public Interaction ApplyDamage(int damage)
        {
            ActionComponents.Add(() => 
            {
                foreach (var entity in Targets)
                {
                    if (entity.IsDestroyed) {  continue; }

                    var wounded = entity.Modifiers.FirstOrDefault(con => con is Wounded);
                    damage = wounded?.ModifyDamageTaken(damage) ?? damage;

                    var protection = entity.Modifiers.FirstOrDefault(con => con is Protected);
                    damage = protection?.ModifyDamageTaken(damage) ?? damage;

                    var immortal = entity.Modifiers.FirstOrDefault(con => con is Immortal);
                    damage = immortal?.ModifyDamageTaken(damage) ?? damage;

                    entity.ApplyDamage(damage, Name);
                    entity.WhenDamaged(this);

                    if (entity.IsDestroyed)
                    {
                        Description += $" - Destroying {entity.Name}";
                    }

                    DamageDealt += damage;
                }
            });
            return this;
        }

        public Interaction Lifesteal()
        {
            ActionComponents.Add(() =>
            {
                Agent.ApplyHealing(DamageDealt, Name);
            });
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
                    Modifier modCopy = modifier.ShallowCopy();
                    entity.ApplyCondition(modCopy, Name);
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
                Scene.Allies.Add(gobbo);
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
                        newForm.Name = $"{entity.Name}";
                        newForm.IsPlayerControlled = entity.IsPlayerControlled;
                        polymorphedEntities.Add(newForm);
                    }

                    if (next == 1)
                    {
                        var newForm = new Sheep("Sheepy");
                        newForm.Faction = entity.Faction;
                        newForm.Name = $"{entity.Name}";
                        newForm.IsPlayerControlled = entity.IsPlayerControlled;
                        polymorphedEntities.Add(newForm);
                    }


                    if (next == 2)
                    {
                        var newForm = new Demon("adasdsa");
                        newForm.Faction = entity.Faction;
                        newForm.Name = $"{entity.Name}";
                        newForm.IsPlayerControlled = entity.IsPlayerControlled;
                        polymorphedEntities.Add(newForm);
                        Description += $" But the spell goes wild and turns {entity.Name} into a DEMON!";
                    }

                }

                Scene.Entities.AddRange(polymorphedEntities);
                Scene.Entities.RemoveAll(x => Targets.Contains(x));

            });
            return this;
        }

        public Interaction Charm()
        {
            ActionComponents.Add(() =>
            {
                foreach (var target in Targets)
                {
                    target.Faction = Agent.Faction;
                }

            });
            return this;
        }

        public Interaction Enrage()
        {
            ActionComponents.Add(() =>
            {
                foreach (var target in Targets)
                {
                    target.Faction = Factions.None;
                }

            });
            return this;
        }

        public Interaction Explodify()
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

        public Interaction Resurrect()
        {
            ActionComponents.Add(() =>
            {
                foreach (var target in Targets)
                {
                    target.Health = 3;
                }

            });
            return this;
        }

        public Interaction Devour()
        {
            ActionComponents.Add(() =>
            {
                foreach (var target in Targets)
                {
                    target.ApplyDamage(3, Name);
                    if (target.IsDestroyed)
                    {
                        Agent.ApplyHealing(3, Name);
                    }
                }

            });
            return this;
        }

        #endregion 

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
                    //IsActionPossible = false;
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
                    //IsActionPossible = false;
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
                    Console.WriteLine();
                    Console.Write($" # {entity.Name} ({entity.GetType().Name})");

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

                    Console.WriteLine();
                }

            });
            return this;

        }

        #endregion

        public void Execute()
        {

            foreach (var action in TargetComponents)
            {
                action.Invoke();
            }

            if (RequiresTargets)
            {
                if (Targets == null || Targets.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($" # {Agent.Name} tries to {Name}, but there's no valid targets");
                    Console.WriteLine();
                    return;
                }
            }

            foreach (var action in ActionComponents)
            {
                action.Invoke();
            }

            if (Description != string.Empty)
            {
                Console.WriteLine();
                string targetString = string.Join(", ", Targets.Select(x => x.Name));
                Description = Description.Replace("[targets]", targetString);
                Console.Write($" # {Description}");
                Console.WriteLine();
            }

            if (FollowupInteraction != null)
            {
                FollowupInteraction.Execute();
            }


            
        }
    }
}
