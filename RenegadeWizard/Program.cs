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


Console.SetBufferSize(300, 400);

Console.BackgroundColor = ConsoleColor.Blue;
Console.ForegroundColor = ConsoleColor.White;
setbackground();

int round = 0;
Scene.Round(round);

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

    round++;
    Scene.Round(round);

    Narrator.ContinuePrompt();

    Console.Clear();
    setbackground();
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
            actionCost = 1;
            actions = PopulateActions();
        }


    }

}

List<Interaction> PopulateActions()
{
    var player = new EntQuery().SelectPlayers().SelectLiving().GetFirst();
    List<Interaction> actions = new();

    for (int i = 0; i < 4; i++)
    {
        var spellCount = Random.Shared.Next(4, 5); 

        if (spellCount == 0)
        {
            var fireball = new Interaction(player, "Fireball").SelectAllEnemies().ApplyCondition(new Burning(2));
            fireball.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} fireball, setting all enemies on fire!";
            actions.Add(fireball);
        }

        if (spellCount == 1)
        {
            var thunderstorm = new Interaction(player, "ThunderStorm").SelectRandom().ApplyDamage(3).SelectRandom().ApplyDamage(3).SelectRandom().ApplyDamage(3);
            thunderstorm.Description = $"{player.Name} rains down {Narrator.GetPowerfulWord()} bolts of lightning, they strike randomly!";
            actions.Add(thunderstorm);

        }

        if (spellCount == 2)
        {
            var heal = new Interaction(player, "HealingBurst").SelectAll().ApplyHealing(3);
            heal.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} healing nova restoring everyone's health!";
            actions.Add(heal);
        }

        if (spellCount == 3)
        {
            var leech = new Interaction(player, "Lifesteal").SelectAllEnemies().ApplyDamage(1).Lifesteal();
            leech.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} lifestealing nova!";
            actions.Add(leech);
        }

        if (spellCount == 4)
        {
            var magicMissle = new Interaction(player, "ArcaneMissle").SelectByName(1).ApplyDamage(1).ApplyDamage(1).ApplyDamage(1);
            magicMissle.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} set of magical missles!";
            actions.Add(magicMissle);

        }

        if (spellCount == 5)
        {
            var daggerSpray = new Interaction(player, "DaggerSpray").SelectAllEnemies().ApplyDamage(1).ApplyCondition(new Wounded(3));
            daggerSpray.Description = $"{player.Name} conjures a fan of {Narrator.GetPowerfulWord()} daggers!";
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
            thunderBall.Description = $"{player.Name} casts a {Narrator.GetPowerfulWord()} thunder nova!";
            actions.Add(thunderBall);
        }

        if (spellCount == 8)
        {
            var conjureFriend = new Interaction(player, "ConjureFriend").ConjureGoblin(player.Faction);
        }

        if (spellCount == 9)
        {
            var summonDemon = new Interaction(player, "SummonDemon").ConjureDemon();
        }

    }

    var inspect = new Interaction(player, "Inspect").Inspect();
    actions.Add(inspect);

    return actions;
}


void setbackground()
{
    //Some nice console art to act as background made by gpt

    // Get the current console dimensions
    int width = Console.WindowWidth;
    int height = Console.WindowHeight;

    char[] glitchChars = new char[]
    {
        '.', '*', '+', 'o', 'x'
    };

    // Fill the console with spaces to apply the background color

    Console.Clear();
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            var ranInt = Random.Shared.Next(200);

            if (ranInt < 1) // 5% chance to place a glitch character
            {
                Console.Write(glitchChars[Random.Shared.Next(glitchChars.Length)]);
            }
            else
            {
                Console.Write(' ');
            }
        }
    }

    Console.SetCursorPosition(100, height - 10);
    Console.Write("\n         _.._\r\n       .' .-'`\r\n      /  /\r\n      |  |\r\n      \\  \\\r\n       '._'-._\r\n          ```");

    // Reset the cursor position to the top-left corner
    Console.SetCursorPosition(0, 0);
}