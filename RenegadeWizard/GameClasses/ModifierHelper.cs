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
    public static class ModifierHelper
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

        public static int GetDamageAfterMods(Entity entity, int damage)
        {
            var immortal = entity.Modifiers.FirstOrDefault(con => con is Immortal);
            if (immortal != null) {
                return immortal.ModifyDamageTaken(damage);
            }

            var wounded = entity.Modifiers.FirstOrDefault(con => con is Immortal);
            if (wounded != null) {
                damage = wounded.ModifyDamageTaken(damage);
            }

            var protection = entity.Modifiers.FirstOrDefault(con => con is Immortal);
            if (protection != null) {
                damage = protection.ModifyDamageTaken(damage);
            }

            return damage;

        }
        public static int GetStrengthAfterMods(Entity entity) {

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

        public static Factions GetFactionAfterMods(Entity entity)
        {
            var charmed = entity.Modifiers.FirstOrDefault(con => con is Charmed);
            if (charmed != null)
            {
                return charmed.OverwriteFaction();
            }
            return entity.Faction;

        }


    }
}
