using RenegadeWizard.Components;
using RenegadeWizard.Conditions;
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

bool hasPlayerWon = false;
int currentRound = 1;
Narrator.ShowRoundInfo(currentRound);

while (Scene.GetPlayer() != null)
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

    var possibleActions = typeof(Actions).GetMethods().Where(action => action.Name.Contains("Action"));
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
    int actionCost = (int)chosenAction.Invoke(Scene.GetPlayer().Actions, actionParameters.ToArray());

    if (actionCost > 0)
    {
        Scene.EngageHyperArtificialIntelligence();

        Scene.ApplyConditionEffects();
        currentRound++;

        if (Scene.GetItems().Count < 3) {
            Console.WriteLine(" # More items are nearby! ");
            Scene.AddBarItems();
        }

        if (currentRound % 3 == 0) {

            Console.WriteLine(" # Some more goblins have shown up!");

            string[] goblinNames = { "Grubnak", "Snaggletooth", "Ruknuk", "Zigzag", "Bogrot", "Nibbles", "Grizzle", "Muckmire", "Skreech", "Wartnose", "Dribble", "Snizzle", "Grubfoot", "Gnash", "Sludge", "Grogmar", "Spitfire", "Blister", "Crackle", "Fungus" };

            var rand = new Random();
            var nextGoblinName = goblinNames[rand.Next(20)];
            Scene.Entities.Add(new Goblin(nextGoblinName));

            rand = new Random();
            nextGoblinName = goblinNames[rand.Next(20)];
            Scene.Entities.Add(new Goblin(nextGoblinName));
            

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

    var rand = new Random();

    Console.Clear();
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            var ranInt = rand.Next(200);

            if (ranInt < 1) // 5% chance to place a glitch character
            {
                Console.Write(glitchChars[rand.Next(glitchChars.Length)]);
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