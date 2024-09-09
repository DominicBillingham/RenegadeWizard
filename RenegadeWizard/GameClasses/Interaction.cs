using RenegadeWizard.Entities;
using RenegadeWizard.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.GameClasses
{
    public class Interaction
    {
        public string ActionName { get; set; }
        public Entity Agent { get; set; }
        public List<Entity> Targets { get; set; }
        public bool IsActionPossible { get; set; } = true;

        private List<Action> ActionComponents = new();

        public Interaction(Entity agent, string name)
        {
            Agent = agent;
            ActionName = name;
        }

        public Interaction CheckImmunity(Modifier immunity)
        {
            ActionComponents.Add(() => 
            {
                foreach (var entity in Targets)
                {
                    if (entity.ModifierImmunities.Contains(immunity))
                    {

                    }
                }
            });
            return this;
        }

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

                    entity.ApplyDamage(damage, ActionName);
                    entity.WhenDamaged();
                }
            });
            return this;
        }

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

        public Interaction ApplyHealing(int healing)
        {
            ActionComponents.Add(() => 
            {
                foreach (var entity in Targets)
                {
                    entity.ApplyHealing(healing, ActionName);
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
                    entity.ApplyCondition(modifier, ActionName);
                }
            });
            return this;
        }

        public void Execute()
        {
            if (IsActionPossible)
            {
                foreach (var action in ActionComponents)
                {
                    action.Invoke();
                }
            }
        }
    }
}
