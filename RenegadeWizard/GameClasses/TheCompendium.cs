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
        public static List<Entity> Entities { get; set; } = new();
        public static List<Interaction> Actions { get; set; } = new();
        public static List<Modifier> Modifiers { get; set; } = new();

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

            Narrator.ContinuePrompt();
            Narrator.Setbackground();
            Narrator.ShowRoundInfo();   
        }

        static void DisplayActionInfo(Interaction action)
        {
            Console.WriteLine();
            Console.WriteLine($" -i- {action.Name}: {action.CompendiumNote}");
            var tagString = string.Join(" | ", action.Tags.Select(x => x.ToString()));
            Console.WriteLine($" -i- tags: {tagString}");

            Narrator.ContinuePrompt();
            Narrator.Setbackground();
            Narrator.ShowRoundInfo();
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
            var CompendiumHuman = new Player("TheCompendium");

            var inspect = new Interaction(CompendiumHuman, "Inspect").Inspect();
            inspect.FreeAction = true;
            inspect.Tags = new List<ActionTag> { ActionTag.Player, ActionTag.FreeAction };
            Actions.Add(inspect);

            var skip = new Interaction(CompendiumHuman, "Skip");
            skip.Tags = new List<ActionTag> { ActionTag.Player };
            Actions.Add(skip);
        }
        static void AddSpells()
        {

            var CompendiumHuman = new Player("TheCompendium");

            var fireball = new Interaction(CompendiumHuman, "Fireball").SelectAllEnemies().ApplyCondition(new Burning(2));
            fireball.Synonyms = new List<string> { "Bang", "Wallop", "Boom" };
            fireball.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllEnemies, ActionTag.Offensive, ActionTag.Igniting };
            fireball.Description = $"{CompendiumHuman.Name} casts a {Narrator.GetPowerfulWord()} fireball, burning [targets]!";
            fireball.CompendiumNote = "Sets all hostile enemies on fire, but doesn't deal any damage directly. Becareful, fire spreads!";
            Actions.Add(fireball);

            var thunderstorm = new Interaction(CompendiumHuman, "ThunderStorm").SelectRandom().SelectRandom().SelectRandom().ApplyDamage(3);
            thunderstorm.Description = $"{CompendiumHuman.Name} rains down {Narrator.GetPowerfulWord()} bolts of lightning!";
            thunderstorm.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllCreatures, ActionTag.Loud, ActionTag.Damaging };
            thunderstorm.CompendiumNote = "Rains down 3 bolts of lightning, that strike randomly (that includes you!)";
            Actions.Add(thunderstorm);

            var heal = new Interaction(CompendiumHuman, "HealingBurst").SelectAll().ApplyHealing(3);
            heal.Description = $"{CompendiumHuman.Name} casts a {Narrator.GetPowerfulWord()} healing nova restoring health!";
            heal.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllCreatures };
            heal.CompendiumNote = "Heals everyone 3 health, that's right, EVERYONE!";
            Actions.Add(heal);

            var leech = new Interaction(CompendiumHuman, "LifeSteal").SelectAllEnemies().ApplyDamage(1).Lifesteal();
            leech.CompendiumNote = "Damage all enemies by 1, with the caster healing the total damage dealt";
            leech.Description = $"{CompendiumHuman.Name} casts a {Narrator.GetPowerfulWord()} lifestealing nova!";
            leech.Tags = new List<ActionTag> { ActionTag.Spell };
            Actions.Add(leech);

            var magicMissle = new Interaction(CompendiumHuman, "ArcaneMissle").SelectByName(1).ApplyDamage(1).ApplyDamage(1).ApplyDamage(1);
            magicMissle.Description = $"{CompendiumHuman.Name} casts a {Narrator.GetPowerfulWord()} missle barrage at [targets]!";
            magicMissle.Tags = new List<ActionTag> { ActionTag.Spell };
            magicMissle.CompendiumNote = "Conjure 3 missles that each deal a seperate damage instance";
            Actions.Add(magicMissle);

            var daggerSpray = new Interaction(CompendiumHuman, "DaggerSpray").SelectAllEnemies().ApplyDamage(1).ApplyCondition(new Wounded(3));
            daggerSpray.Description = $"{CompendiumHuman.Name} conjures a fan of {Narrator.GetPowerfulWord()} daggers!";
            daggerSpray.Tags = new List<ActionTag> { ActionTag.Spell };
            daggerSpray.CompendiumNote = "Throw a fan of dagger striking all enemies, dealing 1 damage and applying wounded for 3 turns";
            Actions.Add(daggerSpray);

            var divineWard = new Interaction(CompendiumHuman, "DivineWard").SelectSelf().ApplyCondition(new Protected(3));
            divineWard.Description = $"{CompendiumHuman.Name} conjures {Narrator.GetPowerfulWord()} barrier to protect themselves!";
            divineWard.Tags = new List<ActionTag> { ActionTag.Spell };
            divineWard.CompendiumNote = "Summon a barrier for 3 turns that reduces incoming damage by 1";
            Actions.Add(divineWard);

            var thunderBall = new Interaction(CompendiumHuman, "ThunderNova").SelectAllEnemies().ApplyDamage(3);
            thunderBall.Description = $"{CompendiumHuman.Name} casts a {Narrator.GetPowerfulWord()} thunder nova!";
            thunderBall.Tags = new List<ActionTag> { ActionTag.Spell };
            thunderBall.CompendiumNote = "Deal a flat 3 damage to all enemies";
            Actions.Add(thunderBall);

            var conjureFriend = new Interaction(CompendiumHuman, "ConjureFriend").ConjureGoblin(CompendiumHuman.Faction);
            conjureFriend.Description = $"{CompendiumHuman.Name} summons a goblin friend to help them out :D";
            conjureFriend.Tags = new List<ActionTag> { ActionTag.Spell };
            conjureFriend.CompendiumNote = "Summon an ally goblin, who will start attacking next turn.";
            Actions.Add(conjureFriend);

            var summonDemon = new Interaction(CompendiumHuman, "SummonDemon").ConjureDemon();
            summonDemon.Description = $"{CompendiumHuman.Name} summons a horrific demon for fun!";
            summonDemon.Tags = new List<ActionTag> { ActionTag.Spell };
            summonDemon.CompendiumNote = "Summon a demon who's on no one's side, especially not YOURS!";
            Actions.Add(summonDemon);

            var raiseDead = new Interaction(CompendiumHuman, "RaiseDead").SelectDeadCreatures().RaiseDead();
            raiseDead.Description = $"{CompendiumHuman.Name} brings back the dead for a party!";
            raiseDead.Tags = new List<ActionTag> { ActionTag.Spell };
            raiseDead.CompendiumNote = "Resurrect all corpses, summoning as zombies to aid you";
            Actions.Add(raiseDead);

            var polymorph = new Interaction(CompendiumHuman, "Polymorph").SelectByName(1).Polymorph();
            polymorph.Description = $"{CompendiumHuman.Name} waves a wand and casts polymorph!";
            polymorph.Tags = new List<ActionTag> { ActionTag.Spell };
            Actions.Add(polymorph);

            var explode = new Interaction(CompendiumHuman, "Exploderise").SelectByName(1).Explodify();
            explode.Description = $"{CompendiumHuman.Name} turns [targets] into a bomb?!";
            explode.Tags = new List<ActionTag> { ActionTag.Spell };
            Actions.Add(explode);

            var charmMonster = new Interaction(CompendiumHuman, "Charmify").SelectByName(1).Charm();
            charmMonster.Description = $"{CompendiumHuman.Name} charms [targets] onto their side!";
            charmMonster.Tags = new List<ActionTag> { ActionTag.Spell };
            Actions.Add(charmMonster);

            var enrageMonster = new Interaction(CompendiumHuman, "Enrage").SelectByName(1).Enrage();
            enrageMonster.Description = $"{CompendiumHuman.Name} enrages [targets]!";
            enrageMonster.Tags = new List<ActionTag> { ActionTag.Spell };
            Actions.Add(enrageMonster);

        }

    }
}
