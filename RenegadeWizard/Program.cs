using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.GameClasses;
using System;
using System.Linq;
using System.Reflection;

Console.BackgroundColor = ConsoleColor.Blue;
Console.ForegroundColor = ConsoleColor.White;
setbackground();
Narrator.ShowRoundInfo();

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

    Narrator.ContinuePrompt();

    Console.Clear();
    setbackground();
    Narrator.ShowRoundInfo();

}

void PlayerTurn(Entity player)
{
    int actionCost = 0;

    List<Interaction> spells = new();
    var fireball = new Interaction(player, "Fireball").CheckIntellect(5).SelectAllEnemies().ApplyDamage(2).ApplyCondition(new Burning(2));
    var daggerstorm = new Interaction(player, "Daggerstorm").CheckIntellect(5).SelectAllEnemies().ApplyDamage(2).ApplyCondition(new Wounded(2));
    var heal = new Interaction(player, "Heal").CheckIntellect(5).SelectAllEnemies().ApplyHealing(2);

    spells.Add(fireball);
    spells.Add(heal);
    spells.Add(daggerstorm);

    while (actionCost == 0)
    {
        Console.Write(" > ");
        var input = Console.ReadLine().ToLower().Split(" ")
            .Where(x => x.Length > 2)
            .ToArray();

        if (input == null)
        {
            Console.WriteLine(" ! No words were found");
            continue;
        }

        if (input[0] == "help")
        {
            Narrator.ShowHelp();
            continue;
        }


        var spell = spells.FirstOrDefault(spell => input.Any(word => spell.ActionName.ToLower().Contains(word)));

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
        }


    }

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