using RenegadeWizard.Conditions;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Items;
using RenegadeWizard.Entities.Items.Drinks;
using RenegadeWizard.Enums;

namespace RenegadeWizard.GameClasses
{
    static class Scene
    {
        public static List<Entity> Entities { get; set; } = new();
        static Scene()
        {
            Entities.Add(new Player("NotHarry"));
            Entities.Add(new Goblin("Goblin"));
            Entities.Add(new Demon("Demon"));
            Entities.Add(new Chandelier());

            AddBarItems();
        }

        public static void AddBarItems()
        {

            var items = typeof(Item).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Item)) && !type.IsAbstract)
                .ToArray();

            for (int i = 0; i < 5; i++)
            {
                var random = new Random();
                var item = items[random.Next(items.Count())];

                Item instance = (Item)Activator.CreateInstance(item);
                Entities.Add(instance);

            }
        }

        public static Entity GetPlayer()
        {
            return Entities.FirstOrDefault(x => x is Player);
        }

        public static List<Entity> GetItems()
        {
            return Entities.Where(ent => ent is Item).ToList();
        }

        public static Entity GetRandomItem()
        {
            var items = Scene.GetItems();
            var random = new Random();
            return items[random.Next(items.Count())];
        }

        public static Entity GetRandomEdibleItem()
        {
            var items = Scene.GetItems().Where(item => item is Drink).ToArray();
            var random = new Random();
            return items[random.Next(items.Count())];
        }

        public static Entity GetRandomEntity()
        {
            var random = new Random();
            return Scene.Entities[random.Next(Scene.Entities.Count())];
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
            var random = new Random();
            return creatures[random.Next(creatures.Count())];
        }

        public static Entity GetRandomHostileCreature(Factions faction)
        {
            var hostiles = Scene.GetCreatures().Where(creature => creature.Faction != faction).ToList();
            var random = new Random();
            return hostiles[random.Next(hostiles.Count())];
        }

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
                var rand = new Random();
                var chosenAction = rand.Next(3);

                if (chosenAction == 2)
                {
                    var randomItem = GetItems()[rand.Next(GetItems().Count())];
                    var enemy = GetRandomHostileCreature(NPC.Faction);

                    NPC.Actions?.ActionThrow(randomItem, enemy);
                }
                else if (chosenAction == 1)
                {
                    var enemy = GetRandomHostileCreature(NPC.Faction);
                    NPC.Actions?.ActionKick(enemy);
                }
                else
                {
                    var randomItem = GetRandomEdibleItem();
                    NPC.Actions?.ActionConsume(randomItem);
                }

            }

        }


    }
}
