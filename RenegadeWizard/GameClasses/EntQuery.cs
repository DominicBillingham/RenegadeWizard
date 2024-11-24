using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Items.Drinks;
using RenegadeWizard.Entities;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenegadeWizard.GameClasses
{
    public class EntQuery
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

        public EntQuery SelectHostiles(Faction faction)
        {
            Query = Query.Where(ent => ent.Faction != faction || ent.Faction == Faction.None);
            return this;
        }

        public EntQuery SelectAllies(Faction faction)
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

}
