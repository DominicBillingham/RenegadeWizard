using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Items;
using RenegadeWizard.Entities.Items.Drinks;
using RenegadeWizard.Enums;
using System;
using static System.Collections.Specialized.BitVector32;

namespace RenegadeWizard.GameClasses
{
    static class Scene
    {
        public static List<Entity> Entities { get; set; } = new();
        static Scene()
        {
            Entities.Add(new Player("NotHarry"));

            Entities.Add(new ShieldGoblin("JeffShield"));
            Entities.Add(new ShieldGoblin("JessShield"));

            Entities.Add(new Goblin("Joe"));
            Entities.Add(new Goblin("Jill"));

            AddBarItems();
        }

        public static void AddBarItems()
        {

            var items = typeof(Item).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Item)) && !type.IsAbstract)
                .ToArray();

            for (int i = 0; i < 5; i++)
            {
                var item = items[Random.Shared.Next(items.Count())];

                Item instance = (Item)Activator.CreateInstance(item);
                Entities.Add(instance);

            }
        }

        #region GetMethods

        public static Entity GetPlayer()
        {
            return Entities.FirstOrDefault(x => x is Player);
        }

        public static Entity GetRandomEntity()
        {
            return Scene.Entities[Random.Shared.Next(Scene.Entities.Count())];
        }

        public static List<Entity> GetItems()
        {
            return Entities.Where(ent => ent is Item).ToList();
        }

        public static Entity GetRandomItem()
        {
            var items = Scene.GetItems();
            if (items.Count() < 3 ) { AddBarItems(); }

            return items[Random.Shared.Next(items.Count())];
        }

        public static Entity GetRandomEdibleItem()
        {
            var items = Scene.GetItems().Where(item => item is Drink).ToArray();
            if (items.Count() < 3) { AddBarItems(); }

            return items[Random.Shared.Next(items.Count())];
        }

        public static List<Entity> GetNPCs()
        {
            return Entities.Where(ent => ent is Creature && ent is not Player).ToList();
        }

        public static List<Entity> GetCreatures()
        {
            return Entities.Where(ent => ent is Creature).ToList();
        }

        public static Entity GetRandomCreature()
        {
            var creatures = Scene.GetCreatures();
            return creatures[Random.Shared.Next(creatures.Count())];
        }

        public static Entity GetRandomHostile(Factions faction)
        {
            var hostiles = Scene.GetCreatures().Where(creature => creature.Faction != faction).ToList();
            return hostiles[Random.Shared.Next(hostiles.Count())];
        }

        public static Entity GetRandomAlly(Factions faction)
        {
            var allies = Scene.GetCreatures().Where(creature => creature.Faction == faction).ToList();
            return allies[Random.Shared.Next(allies.Count())];
        }

        #endregion

        public static void ApplyConditionEffects()
        {
            foreach (var entity in Entities)
            {
                entity.ApplyConditionEffects();
            }
            Entities.RemoveAll(ent => ent.Health <= 0);
        }

        public static void EngageHyperArtificialIntelligence()
        {
            foreach (var NPC in GetNPCs())
            {
                NPC.TakeTurn();
            }

        }


    }
}
