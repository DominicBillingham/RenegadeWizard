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
using System.Numerics;

namespace RenegadeWizard.GameClasses
{
    static class Scene
    {
        public static List<Entity> Entities { get; set; } = new();
        static Scene()
        {
            Entities.Add(new Player("NotHarry"));
            Entities.Add(new Goblin("Jill"));

            AddBarItems();
        }

        public static void AddBarItems()
        {

            var items = typeof(Item).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Item)) && !type.IsAbstract)
                .ToArray();

            for (int i = 0; i < 3; i++)
            {
                var item = items[Random.Shared.Next(items.Count())];

                Item instance = (Item)Activator.CreateInstance(item);
                Entities.Add(instance);

            }
        }

    }
}

class EntQuery
{
    private IQueryable<Entity> Query = Scene.Entities.AsQueryable();

    public EntQuery SelectCreatures()
    {
        Query = Query.Where(ent => ent is Creature);
        return this;
    }

    public EntQuery SelectNpcs()
    {
        Query = Query.Where(ent => ent.IsPlayerControlled == false);
        return this;
    }

    public EntQuery SelectPlayers()
    {
        Query = Query.Where(ent => ent.IsPlayerControlled);
        return this;
    }

    public EntQuery SelectLiving()
    {
        Query = Query.Where(ent => ent.IsDestroyed == false);
        return this;
    }

    public EntQuery SelectItems()
    {
        Query = Query.Where(ent => ent is Item);
        return this;
    }

    public EntQuery SelectDrinks()
    {
        Query = Query.Where(ent => ent is Drink);
        return this;
    }

    public EntQuery SelectHostiles(Factions faction)
    {
        Query = Query.Where(ent => ent.Faction != faction || ent.Faction == Factions.None);
        return this;
    }

    public EntQuery SelectAllies(Factions faction)
    {
        Query = Query.Where(ent => ent.Faction == faction);
        return this;
    }

    public EntQuery SelectNotBurning()
    {
        Query = Query.Where(ent => !ent.Modifiers.Any(con => con is Burning));
        return this;
    }

    public EntQuery SelectNotEntity(Entity entity)
    {
        Query = Query.Where(ent => ent != entity);
        return this;
    }

    public Entity? GetFirst() 
    {
        return Query.FirstOrDefault(); 
    }

    public Entity? GetRandom()
    {
        var entities = Query.ToList();

        if (entities.Count == 0)
        {
            return null;
        }

        var ent = entities[Random.Shared.Next(entities.Count)];  
        return ent;
    }

    public List<Entity> GetAll() 
    { 
        return Query.ToList(); 
    }

}
