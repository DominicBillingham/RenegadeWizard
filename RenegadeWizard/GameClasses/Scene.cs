﻿using RenegadeWizard.Components;
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
using RenegadeWizard.Entities.Creatures.Misc;
using RenegadeWizard.Entities.Creatures.Goblin;
using RenegadeWizard.Entities.Creatures.Geese;

namespace RenegadeWizard.GameClasses
{
    static class Scene
    {
        public static List<Entity> Entities { get; set; } = new();
        public static List<Entity> Reinforcements { get; set; } = new();

        static Scene()
        {
            Entities.Add(new Player("NotHarry"));
        }


        public static void Round(int round)
        {

            AddReinforcements();

            if (round == 0)
            {
                Entities.Add(new Gooseling());
            }

            if (round == 1)
            {
                Console.Write(" # More geese are attacking!");
                Entities.Add(new Gooseling());
                Entities.Add(new Gooseling());
                Entities.Add(new Gooseling());
            }

            if (round == 3)
            {
                Console.Write(" # More geese are attacking!");
                Entities.Add(new Goose());
                Entities.Add(new Goose());
            }

        }

        public static void AddCreature(Entity entity)
        {
            var creatures = new EntQuery().SelectCreatures().GetAll();

            if (creatures.Count < 5)
            {
                Entities.Add(entity);
            }
            else
            {
                var deadCreature = creatures.FirstOrDefault(x => x.IsDestroyed);
                if (deadCreature != null)
                {
                    creatures.Remove(deadCreature);
                    Entities.Add(entity);
                }
                else
                {
                    Reinforcements.Add(entity);
                }
            }


        }

        public static void AddReinforcements()
        {

            var creatures = new EntQuery().SelectCreatures().GetAll();
            var livingCreatures = new EntQuery().SelectCreatures().SelectLiving().GetAll();

            if (Reinforcements.Count == 0 || livingCreatures.Count > 5)
            {
                return;
            }

            if (creatures.Count < 5)
            {
                var ent = Reinforcements.First();
                Entities.Add(ent);
                Reinforcements.Remove(ent);
                AddReinforcements();
            }
            else
            {
                var dead = creatures.First(x => x.IsDestroyed);
                Entities.Remove(dead);

                var ent = Reinforcements.First();
                Entities.Add(ent);
                Reinforcements.Remove(ent);
                AddReinforcements();
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
        Query = Query.Where(ent => ent.IsPlayerControlled == false && ent is Creature);
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

    public EntQuery SelectDead()
    {
        Query = Query.Where(ent => ent.IsDestroyed == true);
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
