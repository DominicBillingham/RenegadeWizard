using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Items;
using RenegadeWizard.Entities.Items.Drinks;
using RenegadeWizard.Enums;
using System;
using static System.Collections.Specialized.BitVector32;
using RenegadeWizard.GameClasses;

namespace RenegadeWizard.GameClasses
{
    static class Scene
    {
        public static List<Entity> Entities { get; set; } = new();
        static Scene()
        {
            Entities.Add(new Player("NotHarry"));
            Entities.Add(new ShieldGoblin("JeffShield"));
            Entities.Add(new Goblin("Jill"));

            AddBarItems();
        }

        public static void AddBarItems()
        {

            var items = typeof(Item).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Item)) && !type.IsAbstract)
                .ToArray();

            for (int i = 0; i < 10; i++)
            {
                var item = items[Random.Shared.Next(items.Count())];

                Item instance = (Item)Activator.CreateInstance(item);
                Entities.Add(instance);

            }
        }

        #region GetMethods

        // builder pattern?
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
            items = Scene.GetItems().Where(item => item is Drink).ToArray();

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

        public static Entity GetRandomCreatureOtherThan(Entity entity)
        {
            var creatures = Scene.GetCreatures();
            creatures.Remove(entity);
            return creatures[Random.Shared.Next(creatures.Count())];
        }

        public static Entity GetFireSpreadTarget(Entity entity)
        {
            var creatures = Scene.GetCreatures();
            creatures.RemoveAll(ent => ent.Modifiers.Any(con => con is Burning));
            creatures.Remove(entity);
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


    }
}

class EntityBuilder
{
    private IQueryable<Entity> Query = Scene.Entities.AsQueryable();

    public void SelectCreatures()
    {
        Query = Query.Where(ent => ent is Creature);
    }

    public void SelectNpcs()
    {
        SelectCreatures();
        Query = Query.Where(ent =>!(ent is Player));
    }

    public void SelectItems()
    {
        Query = Query.Where(ent => ent is Item);
    }

    public void SelectDrinks()
    {
        SelectItems();
        Query = Query.Where(ent => ent is Item);
    }

    public void SelectHostiles(Factions faction)
    {
        Query = Query.Where(ent => ent.Faction != faction);
    }

    public void SelectAllies(Factions faction)
    {
        Query = Query.Where(ent => ent.Faction == faction);
    }

    public void SelectNotBurning()
    {
        Query = Query.Where(ent => ent.Modifiers.Any(con => con is Burning));
    }

    public void SelectNotEntity(Entity entity)
    {
        Query = Query.Where(ent => ent != entity);
    }

    public Entity? GetRandomEntity() 
    {
        var ent = Query.FirstOrDefault();
        return ent;
    }











}
