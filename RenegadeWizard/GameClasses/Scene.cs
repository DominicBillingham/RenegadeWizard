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
            Entities.Add(new BackwaterBeer());
            Entities.Add(new Knife());
            Entities.Add(new Table());
            Entities.Add(new TitanStout());
            Entities.Add(new StickOfButter());
            Entities.Add(new FireflameWine());
            Entities.Add(new Grenade());
            Entities.Add(new GreatBritishTea());

            Entities.Add(new Goblin("Goblin"));
            Entities.Add(new Player("NotHarry"));
            Entities.Add(new Kobold("Kobold"));
        }

        public static Entity GetPlayer()
        {
            // idea: allow for multiple players
            return Entities.FirstOrDefault(x => x is Player);
        }

        public static List<Entity> GetItems()
        {
            // idea: should have entities split into creature / item rather than this bodged check
            return Entities.Where(ent => ent is Item).ToList();
        }

        public static List<Entity> GetNPCs()
        {
            return Entities.Where(ent => ent is Creature && ent is not Player).ToList();
        }

        public static List<Entity> GetCreatures()
        {
            return Entities.Where(ent => ent is Creature).ToList();
        }

        public static void ApplyConditionEffects()
        {
            foreach (var entity in Entities)
            {
                foreach (var con in entity.Conditions)
                {
                    con.ApplyEffect(entity);
                }
                entity.Conditions.RemoveAll(con => con.Duration <= 0);
            }

            Entities.RemoveAll(ent => ent.Health <= 0);
        }

        //public static void EngageHyperArtificialIntelligence()
        //{
        //    foreach (var NPC in GetNPCs())
        //    {
        //        var rand = new Random();
        //        var randomItem = GetItems()[rand.Next(GetItems().Count)];

        //        if ( rand.Next(1) == 0 )
        //        {
        //            NPC.Actions.ActionThrow(randomItem, Scene.GetPlayer());
        //        } 
        //        else
        //        {
        //            NPC.Actions.ActionDrink(randomItem);
        //        }


        //    }
        //}
    }
}
