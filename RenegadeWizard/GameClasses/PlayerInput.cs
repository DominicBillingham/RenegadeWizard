using RenegadeWizard.Entities;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RenegadeWizard.GameClasses
{
    public static class PlayerInput
    {

        public static List<string> Input = new();
        public static List<Interaction> AvailablePlayerActions { get; set; } = new();
        public static Interaction? ChosenAction { get; set; } = null;
        public static List<Entity> ActionParamters { get; set; } = new();

        public static void TakeInput()
        {

            Console.Write(" > ");
            Input = Console.ReadLine().ToLower().Split(" ")
                .Where(x => x.Length > 2)
                .ToList();

            if (Input.Count == 0)
            {
                Console.WriteLine(" ! No words were found");
                return;
            }

            if ( InputContains("help") )
            {
                Narrator.ShowHelp();
                return;
            }

            if ( InputContains("info") )
            {
                foreach (var word in Input)
                {
                    TheCompendium.Search(word);
                    return;
                }
            }

            // If it's not a command keyword, only then run the full Process
            ProcessInput();

        }

        private static void ProcessInput()
        {

            ChosenAction = null;
            ActionParamters.Clear();

            foreach (var action in AvailablePlayerActions)
            {

                if (InputContains(action.Name.ToLower()))
                {
                    ChosenAction = action;
                }

                foreach (var synonym in action.Synonyms)
                {
                    if (InputContains(synonym.ToLower()))
                    {
                        ChosenAction = action;
                    }
                }
            }

            foreach (var entity in Scene.Entities)
            {

                if (InputContains(entity.Name.ToLower()))
                {
                    ActionParamters.Add(entity);
                }
            }

        }

        private static bool InputContains(string word)
        {

            if (Input.Any(inputWord => inputWord.ToLower().Contains(word)))
            {
                return true;
            }

            if (Input.Any(inputWord => word.ToLower().Contains(inputWord)))
            {
                return true;
            }

            return false;

        }


        //public static void TakeTurn(Entity player)
        //{

        //    if (ChosenAction != null)
        //    {
        //        ChosenAction.Targets = actionParameters;
        //        ChosenAction.Execute();

        //        if (ChosenAction.FreeAction)
        //        {
        //            actionCost = 0;

        //            Narrator.ContinuePrompt();
        //            Console.Clear();
        //            Narrator.ShowRoundInfo();

        //        }
        //        else
        //        {
        //            actionCost = 1;
        //            if (Scene.Entities.Any(x => x.IsPlayerControlled == true && x.IsDestroyed == false))
        //            {
        //                AddSpells();
        //            }

        //        }
        //    }


        //}


        // this should pull from the compendium, and not re-create spells because that's a pain
        public static void AddSpells(Entity? player = null)
        {
            List<Interaction> actions = new();

            if (player == null)
            {
                player = new EntQuery().SelectPlayers().GetFirst();
            }

            var inspect = new Interaction(player, "Inspect").Inspect();
            inspect.FreeAction = true;
            inspect.Tags = new List<ActionTag> { ActionTag.Player, ActionTag.RequiresTargetName, ActionTag.Quiet };
            actions.Add(inspect);

            var skip = new Interaction(player, "Skip");
            skip.Tags = new List<ActionTag> { ActionTag.Player };
            actions.Add(skip);


            for (int i = 0; i < 4; i++)
            {
                 
                var spellCount = Random.Shared.Next(0, 15);

                if (spellCount == 0)
                {
                    var fireball = new Interaction(player, "Fireball").SelectAllEnemies().ApplyCondition(new Burning(2));

                    fireball.Synonyms = new List<string> { "Bang", "Wallop", "Boom" };
                    fireball.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllEnemies, ActionTag.Offensive, ActionTag.Igniting };
                    fireball.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} fireball, burning [targets]!";

                    actions.Add(fireball);
                }

                if (spellCount == 1)
                {
                    var thunderstorm = new Interaction(player, "ThunderStorm").SelectRandom().SelectRandom().SelectRandom().ApplyDamage(3);
                    thunderstorm.Description = $"{player.Name} rains down {Narrator.GetPowerfulWord()} bolts of lightning!";
                    thunderstorm.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllCreatures, ActionTag.Loud, ActionTag.Damaging };
                    actions.Add(thunderstorm);

                }

                if (spellCount == 2)
                {
                    var heal = new Interaction(player, "HealingBurst").SelectAll().ApplyHealing(3);
                    heal.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} healing nova restoring health!";
                    heal.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.AffectsAllCreatures };
                    actions.Add(heal);
                }

                if (spellCount == 3)
                {
                    var leech = new Interaction(player, "LifeSteal").SelectAllEnemies().ApplyDamage(1).Lifesteal();
                    leech.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} lifestealing nova!";
                    leech.Tags = new List<ActionTag> { ActionTag.Spell, ActionTag.Offensive, ActionTag.Damaging };
                    actions.Add(leech);
                }

                if (spellCount == 4)
                {
                    var magicMissle = new Interaction(player, "ArcaneMissle").SelectByName(1).ApplyDamage(1).ApplyDamage(1).ApplyDamage(1);
                    magicMissle.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} missle barrage at [targets]!";
                    magicMissle.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(magicMissle);

                }

                if (spellCount == 5)
                {
                    var daggerSpray = new Interaction(player, "DaggerSpray").SelectAllEnemies().ApplyDamage(1).ApplyCondition(new Wounded(3));
                    daggerSpray.Description = $"{player.Name} conjures a fan of {Narrator.GetPowerfulWord()} daggers!";
                    daggerSpray.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(daggerSpray);
                }

                if (spellCount == 6)
                {
                    var divineWard = new Interaction(player, "DivineWard").SelectSelf().ApplyCondition(new Protected(3));
                    divineWard.Description = $"{player.Name} conjures {Narrator.GetPowerfulWord()} barrier to protect themselves!";
                    divineWard.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(divineWard);
                }

                if (spellCount == 7)
                {
                    var thunderBall = new Interaction(player, "ThunderNova").SelectAllEnemies().ApplyDamage(3);
                    thunderBall.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} thunder nova!";
                    thunderBall.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(thunderBall);
                }

                if (spellCount == 8)
                {
                    var conjureFriend = new Interaction(player, "ConjureFriend").ConjureGoblin(player.Faction);
                    conjureFriend.Description = $"{player.Name} summons a goblin friend to help them out :D";
                    conjureFriend.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(conjureFriend);
                }

                if (spellCount == 9)
                {
                    var summonDemon = new Interaction(player, "SummonDemon").ConjureDemon();
                    summonDemon.Description = $"{player.Name} summons a horrific demon for fun!";
                    summonDemon.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(summonDemon);
                }

                if (spellCount == 10)
                {
                    var raiseDead = new Interaction(player, "RaiseDead").SelectDeadCreatures().RaiseDead();
                    raiseDead.Description = $"{player.Name} brings back the dead for a party!";
                    raiseDead.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(raiseDead);
                }

                if (spellCount == 11)
                {
                    var polymorph = new Interaction(player, "Polymorph").SelectByName(1).Polymorph();
                    polymorph.Description = $"{player.Name} waves a wand and casts polymorph!";
                    polymorph.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(polymorph);
                }

                if (spellCount == 12)
                {
                    var explode = new Interaction(player, "Exploderise").SelectByName(1).Explodify();
                    explode.Description = $"{player.Name} turns [targets] into a bomb?!";
                    explode.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(explode);
                }

                if (spellCount == 13)
                {
                    var charmMonster = new Interaction(player, "Charmify").SelectByName(1).Charm();
                    charmMonster.Description = $"{player.Name} charms [targets] onto their side!";
                    charmMonster.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(charmMonster);
                }

                if (spellCount == 14)
                {
                    var enrageMonster = new Interaction(player, "Enrage").SelectByName(1).Enrage();
                    enrageMonster.Description = $"{player.Name} enrages [targets]!";
                    enrageMonster.Tags = new List<ActionTag> { ActionTag.Spell };
                    actions.Add(enrageMonster);
                }

            }

            AvailablePlayerActions = actions;
        }





    }
}
