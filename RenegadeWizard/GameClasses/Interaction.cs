using RenegadeWizard.Entities;
using RenegadeWizard.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RenegadeWizard.GameClasses
{
    public class Interaction
    {
        public string ActionName { get; set; }
        public Entity Agent { get; set; }
        public List<Entity> Targets { get; set; }
        public bool IsActionPossible { get; set; } = true;
        public int ActionDamage { get; set; }

        public Interaction(Entity agent, string name)
        {
            Agent = agent;
            ActionName = name;
        } 

        public Interaction CheckImmunity(Modifier immunity)
        {
            foreach (var entity in Targets)
            {
                if (entity.ModifierImmunities.Contains(immunity))
                {

                }
            }
            return this;
        }

        public Interaction SelectRandomEnemy()
        {
            Targets = new List<Entity>() { new EntQuery().SelectCreatures().SelectHostiles(Agent.Faction).GetRandom() };
            return this;
        }

        public Interaction SelectAllEnemies()
        {
            Targets = new EntQuery().SelectCreatures().SelectHostiles(Agent.Faction).GetAll();
            return this;
        }

        public Interaction SelectAll()
        {
            Targets = new EntQuery().SelectCreatures().GetAll();
            return this;
        }

        public Interaction ApplyDamage(int damage)
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
            return this;
        }

        public Interaction CheckIntellect(int requirement)
        {
            int intellect = Agent.Attributes?.Intellect ?? 0;

            var exhausted = Agent.Modifiers.FirstOrDefault(con => con is Exhausted);
            intellect = exhausted?.ModifyIntellect(intellect) ?? intellect;

            if (intellect < requirement)
            {
                IsActionPossible = false;
            }

            return this;
        }

        public Interaction CheckStrength(int requirement)
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

            return this;
        }

        public Interaction ApplyHealing(int healing)
        {
            foreach (var entity in Targets)
            {
                entity.ApplyHealing(healing, ActionName);
            }
            return this;
        }

        public Interaction ApplyCondition(Modifier modifier)
        {
            foreach (var entity in Targets)
            {
                entity.ApplyCondition(modifier, ActionName);
            }
            return this;
        }



    }

}
