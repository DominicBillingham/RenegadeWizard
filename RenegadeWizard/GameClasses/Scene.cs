using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Creatures.Geese;
using RenegadeWizard.Entities.Items.Drinks;
using RenegadeWizard.Enums;
using RenegadeWizard.GameClasses;
using RenegadeWizard.Modifiers;

namespace RenegadeWizard.GameClasses
{
    static class Scene
    {
        public static List<Entity> Entities { get; set; } = new();
        public static List<Entity> Reinforcements { get; set; } = new();
        public static List<Entity> Allies { get; set; } = new();
        public static string Description { get; set; } = 
            "Your journey starts at the foot of Mount Endeavour, it's colossal size invites you to climb ever higher." +
            "\n # But first you'll need to find your way through the dense trees. <WayfarerForest> ";
        public static bool inCombat { get; set; } = false;

        static Scene()
        {
        }

        public static void Update()
        {
            AddReinforcements();
            AddAllies();
            CorpseCleanup();
        }

        public static void AddReinforcements()
        {

            var creatures = new EntQuery().SelectHostiles(Factions.Player).SelectCreatures().GetAll();
            var livingCreatures = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Factions.Player).GetAll();

            if (Reinforcements.Count == 0 || livingCreatures.Count > 3)
            {
                return;
            }

            var ent = Reinforcements.First();
            Entities.Add(ent);
            Reinforcements.Remove(ent);
            AddReinforcements();
            
        }

        public static void AddAllies()
        {

            var creatures = new EntQuery().SelectAllies(Factions.Player).SelectCreatures().GetAll();
            var livingCreatures = new EntQuery().SelectCreatures().SelectLiving().SelectAllies(Factions.Player).GetAll();

            if (Allies.Count == 0 || livingCreatures.Count > 2)
            {
                return;
            }

            var ent = Allies.First();
            Entities.Add(ent);
            Allies.Remove(ent);
            AddAllies();
        }

        public static void CorpseCleanup()
        {
            var deadCreatures = new EntQuery().SelectCreatures().SelectDead().GetAll();
            if (deadCreatures.Count < 6)
            {
                return;
            }
            Entities.Remove(deadCreatures.First());
            CorpseCleanup();
        }

        public static void ResetScene()
        {
            Entities = new();
            Reinforcements = new();
            Allies = new();
        }

    }
}

