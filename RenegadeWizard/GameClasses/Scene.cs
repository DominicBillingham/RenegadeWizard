using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Items;
using RenegadeWizard.Entities.Items.Drinks;

namespace RenegadeWizard.GameClasses
{
    static class Scene
    {
        public static List<Entity> Entities { get; set; } = new();
        static Scene()
        {
            Entities.Add(new Player("NotHarry"));
            Entities.Add(new Goblin("Goblin"));
            Entities.Add(new Chandelier());

            AddBarItems();
        }

        public static void AddBarItems()
        {

            var items = typeof(Item).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Item)));
            foreach (var item in items)
            {
                Item instance = (Item)Activator.CreateInstance(item);
                Entities.Add(instance);
            }


        }

        public static Entity GetPlayer()
        {
            // idea: allow for multiple players
            return Entities.First(x => x is Player);
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

        public static void ApplyConditionEffects()
        {
            Console.Write(" # ");

            foreach (var entity in Entities)
            {
                foreach (var con in entity.Conditions)
                {
                    con.ApplyEffect(entity);
                }
                entity.Conditions.RemoveAll(con => con.Duration <= 0);
            }

            Entities.RemoveAll(ent => ent.Health <= 0);

            Console.WriteLine();
        }

        public static void EngageHyperArtificialIntelligence()
        {
            foreach (var NPC in GetNPCs())
            {

                var rand = new Random();
                var randomItem = GetItems()[rand.Next(GetItems().Count)];

                if (GetNPCs().Count() > 0) {
                    Console.Write(" # ");
                }

                var chosenAction = rand.Next(3);

                var enemy = Scene.GetPlayer();

                if (NPC is Demon)
                {
                    enemy = Scene.GetRandomCreature();
                }

                if (chosenAction == 2)
                {
                    NPC.Actions?.ActionThrow(randomItem, enemy);
                }
                else if (chosenAction == 1)
                {
                    NPC.Actions?.ActionKick(enemy);
                }
                else
                {
                    NPC.Actions?.ActionConsume(randomItem);
                }

                Console.WriteLine();

            }

        }


    }
}
