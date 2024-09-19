using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.GameClasses;
using System;
using System.Linq;
using System.Reflection;
using System.Numerics;

List<Interaction> actions = PopulateActions();

List<string> consoleBuffer = new List<string>();

Console.SetBufferSize(300, 400);

Console.BackgroundColor = ConsoleColor.Blue;
Console.ForegroundColor = ConsoleColor.White;
Narrator.Setbackground();

Scene.NextRound();
Narrator.ShowRoundInfo(actions);


while (true)
{
    var players = new EntQuery().SelectPlayers().SelectLiving().GetAll();
    foreach (Entity ent in players)
    {
        PlayerTurn(ent);
    }

    var npcs = new EntQuery().SelectNpcs().SelectLiving().GetAll();
    foreach (Entity ent in npcs)
    {
        ent.TakeTurn();
    }

    var ents = Scene.Entities;
    foreach (var ent in ents)
    {
        ent.ApplyRoundEndEffects();
    }

    Scene.Round++;
    Scene.NextRound();

    Narrator.ShowExplosions();

    Narrator.ContinuePrompt();

    Console.Clear();
    Narrator.Setbackground();
    Narrator.ShowRoundInfo(actions);

}

void PlayerTurn(Entity player)
{
    int actionCost = 0;

    while (actionCost == 0)
    {
        Console.Write(" > ");
        var input = Console.ReadLine().ToLower().Split(" ")
            .Where(x => x.Length > 2)
            .ToArray();

        if (input.Length == 0)
        {
            Console.WriteLine(" ! No words were found");
            continue;
        }

        if (input[0] == "help")
        {
            Narrator.ShowHelp();
            continue;
        }

        var spell = actions.FirstOrDefault(spell => input.Any(word => spell.Name.ToLower().Contains(word)));

        if (spell == null)
        {
            Console.WriteLine(" ! No matching actions were found");
            continue;
        }

        List<Entity> sceneEntities = new List<Entity>(Scene.Entities);
        List<Entity> actionParameters = new();

        foreach (var word in input)
        {
            var paramerter = sceneEntities.FirstOrDefault(x => x.Name.ToLower().Contains(word));
            if (paramerter != null)
            {
                actionParameters.Add(paramerter);
                sceneEntities.Remove(paramerter);
            }
        }

        if (spell != null)
        {
            spell.Targets = actionParameters;
            spell.Execute();

            if (spell.FreeAction)
            {
                actionCost = 0;

                Narrator.ContinuePrompt();
                Console.Clear();
                Narrator.ShowRoundInfo(actions);

            }
            else
            {
                actionCost = 1;
                actions = PopulateActions();
            }



        }


    }

}

List<Interaction> PopulateActions()
{
    var player = new EntQuery().SelectPlayers().SelectLiving().GetFirst();
    List<Interaction> actions = new();

    for (int i = 0; i < 4; i++)
    {
        var spellCount = Random.Shared.Next(10, 12); 

        if (spellCount == 0)
        {
            var fireball = new Interaction(player, "Fireball").SelectAllEnemies().ApplyCondition(new Burning(2));
            fireball.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} fireball, burning [targets]!";
            actions.Add(fireball);
        }

        if (spellCount == 1)
        {
            var thunderstorm = new Interaction(player, "ThunderStorm").SelectRandom().SelectRandom().SelectRandom().ApplyDamage(3);
            thunderstorm.Description = $"{player.Name} rains down {Narrator.GetPowerfulWord()} bolts of lightning, they strike [targets]!";
            actions.Add(thunderstorm);

        }

        if (spellCount == 2)
        {
            var heal = new Interaction(player, "HealingBurst").SelectAll().ApplyHealing(3);
            heal.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} healing nova restoring health!";
            actions.Add(heal);
        }

        if (spellCount == 3)
        {
            var leech = new Interaction(player, "LifeSteal").SelectAllEnemies().ApplyDamage(1).Lifesteal();
            leech.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} lifestealing nova!";
            actions.Add(leech);
        }

        if (spellCount == 4)
        {
            var magicMissle = new Interaction(player, "ArcaneMissle").SelectByName(1).ApplyDamage(1).ApplyDamage(1).ApplyDamage(1);
            magicMissle.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} missle barrage at [targets]!";
            actions.Add(magicMissle);

        }

        if (spellCount == 5)
        {
            var daggerSpray = new Interaction(player, "DaggerSpray").SelectAllEnemies().ApplyDamage(1).ApplyCondition(new Wounded(3));
            daggerSpray.Description = $"{player.Name} conjures a fan of {Narrator.GetPowerfulWord()} daggers slashing [targets]!";
            actions.Add(daggerSpray);
        }

        if (spellCount == 6)
        {
            var divineWard = new Interaction(player, "DivineWard").SelectSelf().ApplyCondition(new Protected(3));
            divineWard.Description = $"{player.Name} conjures {Narrator.GetPowerfulWord()} barrier to protect themselves!";
            actions.Add(divineWard);
        }

        if (spellCount == 7)
        {
            var thunderBall = new Interaction(player, "ThunderNova").SelectAllEnemies().ApplyDamage(3);
            thunderBall.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} thunder nova smashing [targets]!";
            actions.Add(thunderBall);
        }

        if (spellCount == 8)
        {
            var conjureFriend = new Interaction(player, "ConjureFriend").ConjureGoblin(player.Faction);
            conjureFriend.Description = $"{player.Name} summons a goblin friend to help them out :D";
            actions.Add(conjureFriend);
        }

        if (spellCount == 9)
        {
            var summonDemon = new Interaction(player, "SummonDemon").ConjureDemon();
            summonDemon.Description = $"{player.Name} summons a horrific demon for fun!";
            actions.Add(summonDemon);
        }

        if (spellCount == 10)
        {
            var raiseDead = new Interaction(player, "RaiseDead").SelectDeadCreatures().RaiseDead();
            raiseDead.Description = $"{player.Name} brings back the dead for a party!";
            actions.Add(raiseDead);
        }

        if (spellCount == 11)
        {
            var polymorph = new Interaction(player, "Polymorph").SelectByName(1).Polymorph();
            polymorph.Description = $"{player.Name} turns [targets] into a sheep!";
            actions.Add(polymorph);
        }

        if (spellCount == 12)
        {
            var explode = new Interaction(player, "Exploderise").SelectByName(1).Explodify();
            explode.Description = $"{player.Name} turns [targets] into a bomb?!";
            actions.Add(explode);
        }

        if (spellCount == 13)
        {
            var charmMonster = new Interaction(player, "Charmify").SelectByName(1).Charm();
            charmMonster.Description = $"{player.Name} charms [targets] onto their side!";
            actions.Add(charmMonster);
        }

        if (spellCount == 14)
        {
            var enrageMonster = new Interaction(player, "Enrage").SelectByName(1).Enrage();
            enrageMonster.Description = $"{player.Name} enrages [targets]!";
            actions.Add(enrageMonster);
        }

    }

    var inspect = new Interaction(player, "Inspect").Inspect();
    inspect.FreeAction = true;
    actions.Add(inspect);

    return actions;
}

