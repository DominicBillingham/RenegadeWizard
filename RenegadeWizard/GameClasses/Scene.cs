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
        public static List<Entity> Allies { get; set; } = new();

        public static int Round = 0;

        static Scene()
        {
            Entities.Add(new Player("NotHarry"));
        }


        public static void NextRound()
        {

            if (Round == 0)
            {
                Reinforcements.Add(new Gooseling());
                Reinforcements.Add(new Gooseling());
                Reinforcements.Add(new Gooseling());
            }

            if (Round == 3)
            {
                Console.Write(" # More geese are attacking!");
                Reinforcements.Add(new Goose());
                Reinforcements.Add(new Goose());
            }

            if (Round % 3 == 0 && Round > 3)
            {
                var enemyGroup = Random.Shared.Next(10);

                if (enemyGroup == 0)
                {
                    Reinforcements.Add(new Waddlepriest());
                    Reinforcements.Add(new Goose());
                    Reinforcements.Add(new Goose());
                }

                if (enemyGroup == 1)
                {
                    Reinforcements.Add(new Grimgooser());
                    Reinforcements.Add(new Gooseling());
                    Reinforcements.Add(new Gooseling());
                    Reinforcements.Add(new Gooseling());
                }

                if (enemyGroup == 2)
                {
                    Reinforcements.Add(new Goozerker());
                    Reinforcements.Add(new Goose());
                    Reinforcements.Add(new Goose());
                }

                if (enemyGroup == 3)
                {
                    Reinforcements.Add(new Fowlspell());
                    Reinforcements.Add(new Gooseling());
                    Reinforcements.Add(new Gooseling());
                }

                if (enemyGroup == 4)
                {
                    Reinforcements.Add(new Fowlspell());
                    Reinforcements.Add(new Fowlspell());
                }

                if (enemyGroup == 5)
                {
                    Reinforcements.Add(new Goozerker());
                    Reinforcements.Add(new Goozerker());
                }

                if (enemyGroup == 6)
                {
                    Reinforcements.Add(new Goozerker());
                    Reinforcements.Add(new Fowlspell());
                }

                if (enemyGroup == 7)
                {
                    Reinforcements.Add(new Goozerker());
                    Reinforcements.Add(new Waddlepriest());
                }

                if (enemyGroup == 8)
                {
                    Reinforcements.Add(new Waddlepriest());
                    Reinforcements.Add(new Fowlspell());
                }

                if (enemyGroup == 9)
                {
                    Reinforcements.Add(new Gooseling());
                    Reinforcements.Add(new Gooseling());
                    Reinforcements.Add(new Gooseling());
                    Reinforcements.Add(new Gooseling());
                    Reinforcements.Add(new Gooseling());

                }

            }

            AddReinforcements();
            AddAllies();

        }

        public static void AddReinforcements()
        {

            var creatures = new EntQuery().SelectHostiles(Factions.Player).SelectCreatures().GetAll();
            var livingCreatures = new EntQuery().SelectCreatures().SelectLiving().SelectHostiles(Factions.Player).GetAll();

            if (Reinforcements.Count == 0 || livingCreatures.Count > 3)
            {
                return;
            }

            if (creatures.Count < 4)
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

        public static void AddAllies()
        {

            var creatures = new EntQuery().SelectAllies(Factions.Player).SelectCreatures().GetAll();
            var livingCreatures = new EntQuery().SelectCreatures().SelectLiving().SelectAllies(Factions.Player).GetAll();

            if (Allies.Count == 0 || livingCreatures.Count > 2)
            {
                return;
            }

            if (creatures.Count < 3)
            {
                var ent = Allies.First();
                Entities.Add(ent);
                Allies.Remove(ent);
                AddAllies();
            }
            else
            {
                var dead = creatures.First(x => x.IsDestroyed);
                Entities.Remove(dead);

                var ent = Allies.First();
                Entities.Add(ent);
                Allies.Remove(ent);
                AddAllies();
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
