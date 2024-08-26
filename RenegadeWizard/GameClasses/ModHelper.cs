using RenegadeWizard.Modifiers;
using RenegadeWizard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenegadeWizard.Enums;

namespace RenegadeWizard.GameClasses
{
    public static class ModHelper
    {
        // The modifier classes contain the effects of a modifier
        // The modifier helper applies the effects, and sets an order of operations if needed e.g DamageModifiers

        public static void ApplyRoundEndEffects(Entity entity) {

            foreach (var con in entity.Modifiers)
            {
                con.OnRoundEnd(entity);
            }

        }

        public static void ApplyExpirationEffects(Entity entity)
        {
            foreach (var con in entity.Modifiers.Where(x => x.Duration == 0))
            {
                con.OnExpiration(entity);
            }
            entity.Modifiers.RemoveAll(con => con.Duration == 0);
        }

        public static int ModDamage(Entity entity, int damage)
        {
            var immortal = entity.Modifiers.FirstOrDefault(con => con is Immortal);
            if (immortal != null) {
                return immortal.ModifyDamageTaken(damage);
            }

            var wounded = entity.Modifiers.FirstOrDefault(con => con is Wounded);
            if (wounded != null) {
                damage = wounded.ModifyDamageTaken(damage);
            }

            var protection = entity.Modifiers.FirstOrDefault(con => con is Protected);
            if (protection != null) {
                damage = protection.ModifyDamageTaken(damage);
            }

            return damage;

        }
        public static int ModStrength(Entity entity) {

            if (entity.Attributes == null)
            {
                return 0;
            }

            int strengthAfterModifiers = entity.Attributes.Strength;

            var exhausted = entity.Modifiers.FirstOrDefault(con => con is Exhausted);
            if (exhausted != null)
            {
                strengthAfterModifiers = exhausted.ModifyStrength(strengthAfterModifiers);
            }

            var enlarged = entity.Modifiers.FirstOrDefault(con => con is Enlarged);
            if (enlarged != null)
            {
                strengthAfterModifiers = enlarged.ModifyStrength(strengthAfterModifiers);
            }

            return strengthAfterModifiers;
        }

        public static int ModAgility(Entity entity)
        {

            if (entity.Attributes == null)
            {
                return 0;
            }

            int agilityAfterModifiers = entity.Attributes.Agility;

            var exhausted = entity.Modifiers.FirstOrDefault(con => con is Exhausted);
            if (exhausted != null)
            {
                agilityAfterModifiers = exhausted.ModifyIntellect(agilityAfterModifiers);
            }

            return agilityAfterModifiers;
        }

        public static int ModIntellect(Entity entity)
        {

            if (entity.Attributes == null)
            {
                return 0;
            }

            int intellectAfterModifiers = entity.Attributes.Intellect;

            var exhausted = entity.Modifiers.FirstOrDefault(con => con is Exhausted);
            if (exhausted != null)
            {
                intellectAfterModifiers = exhausted.ModifyIntellect(intellectAfterModifiers);
            }

            return intellectAfterModifiers;
        }

        public static Factions ModFaction(Entity entity)
        {
            var changedFaction = entity.Modifiers.FirstOrDefault(con => con is ChangedFaction);

            if (changedFaction != null)
            {
                return changedFaction.OverwriteFaction();
            }

            return entity.Faction;

        }

        public static Entity ModTarget(Entity initialTarget)
        {

            var hidden = initialTarget.Modifiers.FirstOrDefault(con => con is Hidden);

            if (hidden != null)
            {
                return hidden.ModifyTarget(initialTarget);
            }

            return initialTarget;

        }



    }
}
