using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.GameClasses;
using System;
using System.Linq;
using System.Reflection;

//Narrator.ShowIntro
//
Console.Clear();

Console.BackgroundColor = ConsoleColor.Blue;
Console.ForegroundColor = ConsoleColor.White;
setbackground();

bool gameEnd = false;
int currentRound = 1;
Narrator.ShowRoundInfo(currentRound);

while (gameEnd == false)
{
    Console.Write(" > ");
    var input = Console.ReadLine().ToLower().Split(" ")
        .Where(x => x.Length > 2)
        .ToArray();

    if (input.Count() == 0)
    {
        Console.WriteLine(" ! No words found");
        continue;
    }

    if (input.Any(x => x.Contains("help")))
    {
        Narrator.ShowHelp();
        continue;
    }

    var possibleActions = typeof(Interaction).GetMethods().Where(action => action.Name.Contains("Action"));
    MethodInfo chosenAction = possibleActions.FirstOrDefault(action => input.Any(word => action.Name.ToLower().Contains(word)));

    if (chosenAction == null)
    {
        Console.WriteLine(" ! No valid action was found");
        continue;
    }

    // Find action paratmers
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

    // Validation
    int paramsNeeded = chosenAction.GetParameters().Count();
    if (actionParameters.Count() != paramsNeeded)
    {
        Console.WriteLine($" ! '{chosenAction.Name.Substring(6)}' requires {paramsNeeded} name(s) to be provided");
        continue;
    }

    // Perform Round Actions
    Console.WriteLine();


    var interaction = new Interaction();
    interaction.Agent = new EntQuery().SelectPlayers().GetFirst();
    int actionCost = (int)chosenAction.Invoke(interaction, actionParameters.ToArray());


    if (actionCost > 0)
    {
        foreach (var entity in Scene.Entities)
        {
            entity.BattleLog = string.Empty;
        }

        var Npcs = new EntQuery().SelectNpcs().SelectLiving().GetAll();
        foreach (var NPC in Npcs)
        {
            NPC.TakeTurn();
        }

        var Ents = Scene.Entities;
        foreach (var entity in Ents)
        {
            ModifierHelper.ApplyRoundEndEffects(entity);
            ModifierHelper.ApplyExpirationEffects(entity);
        }

        currentRound++;

        if (new EntQuery().SelectItems().GetAll().Count < 5) {
            Scene.AddBarItems();
        }

        Narrator.ContinuePrompt();

        Console.Clear();
        setbackground();

        Narrator.ShowRoundInfo(currentRound);
    }

 }

Console.Clear();
Console.WriteLine("You've lost you silly goose");
Narrator.ContinuePrompt();

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