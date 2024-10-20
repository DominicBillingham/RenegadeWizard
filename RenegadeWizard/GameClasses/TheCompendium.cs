using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.Entities.Creatures.Geese;
using RenegadeWizard.Entities.Creatures.Misc;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RenegadeWizard.GameClasses
{
    static public class TheCompendium
    {
        // Used to store a generic instance of each ACTION, ENTITY, MODIFIER etc.
        // This is primarily for being able to quickly find information about a game mechanic using the INFO command

        public static List<Entity> Entities { get; set; } = new();
        public static List<Interaction> CoreActions { get; set; } = new();
        public static List<Interaction> Spells { get; set; } = new();
        public static List<Interaction> Actions
        {
            get
            {
                return CoreActions.Concat( Spells).ToList();
            }
        }
        public static List<Modifier> Modifiers { get; set; } = new();
        public static Player Player { get; set; } = new Player("NotHarry");

        static TheCompendium()
        {
            AddSpells();
            AddCoreActions();
            AddMonsters();
            AddModifiers();

        }

        static public void Search(string name)
        {

            foreach (var action in Actions)
            {
                if (action.Name.ToLower().Contains(name))
                {
                    DisplayActionInfo(action);
                }
            }

            foreach (var modifier in Modifiers)
            {
               
                if (modifier.Name.ToLower().Contains(name))
                {
                    DisplayModifierInfo(modifier);
                }
            }
        }

        static void DisplayModifierInfo(Modifier modifier)
        {
            Console.WriteLine();
            Console.WriteLine($" -i- {modifier.Name}: {modifier.CompendiumNote}");
        }

        static void DisplayActionInfo(Interaction action)
        {
            Console.WriteLine();
            Console.WriteLine($" -i- {action.Name}: {action.CompendiumNote}");
            var tagString = string.Join(" | ", action.Tags.Select(x => x.ToString()));
            Console.WriteLine($" -i- tags: {tagString}");
        }

        static public void ListSpells()
        {
            foreach (var spell in Spells)
            {
                DisplayActionInfo(spell);
                Thread.Sleep(100);
            }
        }
        static public void ListCoreActions()
        {
            foreach (var action in CoreActions)
            {
                DisplayActionInfo(action);
                Thread.Sleep(100);

            }
        }

        static void AddModifiers()
        {
            Modifiers.Add(new Bleeding(3));
            Modifiers.Add(new Wounded(3));
            Modifiers.Add(new Immortal(3));
            Modifiers.Add(new Burning(3));
        }

        static void AddMonsters()
        {
            Entities.Add(new Gooseling());
            Entities.Add(new Goose());
            Entities.Add(new Grimgooser());
            Entities.Add(new Goozerker());
            Entities.Add(new Waddlepriest());
            Entities.Add(new Fowlspell());
        }

        static void AddCoreActions()
        {
            var inspect = new Interaction(Player, "Inspect").Inspect();
            inspect.FreeAction = true;
            inspect.CompendiumNote = "Inspect a creature or item to learn more information, depending on how smart you are...";
            inspect.Tags = new List<ActionTag> { ActionTag.Player, ActionTag.FreeAction };
            CoreActions.Add(inspect);

            var skip = new Interaction(Player, "Skip");
            skip.CompendiumNote = "Allows you to skip your turn in combat.";
            skip.Tags = new List<ActionTag> { ActionTag.Player };
            CoreActions.Add(skip);
        }
        static void AddSpells()
        {

            var fireball = new Interaction(Player, "Fireball").SelectAllEnemies().ApplyCondition(new Burning(2));
            fireball.Synonyms = new List<string> { "Bang", "Wallop", "Boom" };
            fireball.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllEnemies, ActionTag.Offensive, ActionTag.Igniting };
            fireball.Description = $"{Player.Name} casts a {Narrator.GetPowerfulWord()} fireball, burning [targets]!";
            fireball.CompendiumNote = "Sets all hostile enemies on fire, but doesn't deal any damage directly. Becareful, fire spreads!";
            Spells.Add(fireball);

            var thunderstorm = new Interaction(Player, "ThunderStorm").SelectRandom().SelectRandom().SelectRandom().ApplyDamage(3);
            thunderstorm.Description = $"{Player.Name} rains down {Narrator.GetPowerfulWord()} bolts of lightning!";
            thunderstorm.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllCreatures, ActionTag.Loud, ActionTag.Damaging };
            thunderstorm.CompendiumNote = "Rains down 3 bolts of lightning, that strike randomly (that includes you!)";
            Spells.Add(thunderstorm);

            //var heal = new Interaction(Player, "HealingBurst").SelectAll().ApplyHealing(3);
            //heal.Description = $"{Player.Name} casts a {Narrator.GetPowerfulWord()} healing nova restoring health!";
            //heal.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllCreatures };
            //heal.CompendiumNote = "Heals everyone 3 health, that's right, EVERYONE!";
            //Spells.Add(heal);

            //var leech = new Interaction(Player, "LifeSteal").SelectAllEnemies().ApplyDamage(1).Lifesteal();
            //leech.CompendiumNote = "Damage all enemies by 1, with the caster healing the total damage dealt";
            //leech.Description = $"{Player.Name} casts a {Narrator.GetPowerfulWord()} lifestealing nova!";
            //leech.Tags = new List<ActionTag> { ActionTag.Spell };
            //Spells.Add(leech);

            //var magicMissle = new Interaction(Player, "ArcaneMissle").SelectByName(1).ApplyDamage(1).ApplyDamage(1).ApplyDamage(1);
            //magicMissle.Description = $"{Player.Name} casts a {Narrator.GetPowerfulWord()} missle barrage at [targets]!";
            //magicMissle.Tags = new List<ActionTag> { ActionTag.Spell };
            //magicMissle.CompendiumNote = "Conjure 3 missles that each deal a seperate damage instance";
            //Spells.Add(magicMissle);

            //var daggerSpray = new Interaction(Player, "DaggerSpray").SelectAllEnemies().ApplyDamage(1).ApplyCondition(new Wounded(3));
            //daggerSpray.Description = $"{Player.Name} conjures a fan of {Narrator.GetPowerfulWord()} daggers!";
            //daggerSpray.Tags = new List<ActionTag> { ActionTag.Spell };
            //daggerSpray.CompendiumNote = "Throw a fan of dagger striking all enemies, dealing 1 damage and applying wounded for 3 turns";
            //Spells.Add(daggerSpray);

            //var divineWard = new Interaction(Player, "DivineWard").SelectSelf().ApplyCondition(new Protected(3));
            //divineWard.Description = $"{Player.Name} conjures {Narrator.GetPowerfulWord()} barrier to protect themselves!";
            //divineWard.Tags = new List<ActionTag> { ActionTag.Spell };
            //divineWard.CompendiumNote = "Summon a barrier for 3 turns that reduces incoming damage by 1";
            //Spells.Add(divineWard);

            //var thunderBall = new Interaction(Player, "ThunderNova").SelectAllEnemies().ApplyDamage(3);
            //thunderBall.Description = $"{Player.Name} casts a {Narrator.GetPowerfulWord()} thunder nova!";
            //thunderBall.Tags = new List<ActionTag> { ActionTag.Spell };
            //thunderBall.CompendiumNote = "Deal a flat 3 damage to all enemies";
            //Spells.Add(thunderBall);

            //var conjureFriend = new Interaction(Player, "ConjureFriend").ConjureGoblin(Player.Faction);
            //conjureFriend.Description = $"{Player.Name} summons a goblin friend to help them out :D";
            //conjureFriend.Tags = new List<ActionTag> { ActionTag.Spell };
            //conjureFriend.CompendiumNote = "Summon an ally goblin, who will start attacking next turn.";
            //Spells.Add(conjureFriend);

            //var summonDemon = new Interaction(Player, "SummonDemon").ConjureDemon();
            //summonDemon.Description = $"{Player.Name} summons a horrific demon for fun!";
            //summonDemon.Tags = new List<ActionTag> { ActionTag.Spell };
            //summonDemon.CompendiumNote = "Summon a demon who's on no one's side, especially not YOURS!";
            //Spells.Add(summonDemon);

            //var raiseDead = new Interaction(Player, "RaiseDead").SelectDeadCreatures().RaiseDead();
            //raiseDead.Description = $"{Player.Name} brings back the dead for a party!";
            //raiseDead.Tags = new List<ActionTag> { ActionTag.Spell };
            //raiseDead.CompendiumNote = "Resurrect all corpses, summoning as zombies to aid you";
            //Spells.Add(raiseDead);

            //var polymorph = new Interaction(Player, "Polymorph").SelectByName(1).Polymorph();
            //polymorph.Description = $"{Player.Name} waves a wand and casts polymorph!";
            //polymorph.Tags = new List<ActionTag> { ActionTag.Spell };
            //Spells.Add(polymorph);

            //var explode = new Interaction(Player, "Exploderise").SelectByName(1).Explodify();
            //explode.Description = $"{Player.Name} turns [targets] into a bomb?!";
            //explode.Tags = new List<ActionTag> { ActionTag.Spell };
            //Spells.Add(explode);

            //var charmMonster = new Interaction(Player, "Charmify").SelectByName(1).Charm();
            //charmMonster.Description = $"{Player.Name} charms [targets] onto their side!";
            //charmMonster.Tags = new List<ActionTag> { ActionTag.Spell };
            //Spells.Add(charmMonster);

            //var enrageMonster = new Interaction(Player, "Enrage").SelectByName(1).Enrage();
            //enrageMonster.Description = $"{Player.Name} enrages [targets]!";
            //enrageMonster.Tags = new List<ActionTag> { ActionTag.Spell };
            //Spells.Add(enrageMonster);

        }

    }
}
