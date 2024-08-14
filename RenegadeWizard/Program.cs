﻿using RenegadeWizard.Components;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.GameClasses;
using System;
using System.Linq;
using System.Reflection;

//Narrator.ShowIntro();

bool hasPlayerWon = false;
int currentRound = 1;
Narrator.ShowRoundInfo(currentRound);

while (hasPlayerWon == false)
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

    Console.Write(" # ");
    int actionCost = (int)chosenAction.Invoke(Scene.GetPlayer().Actions, actionParameters.ToArray());
    Console.WriteLine();

    if (actionCost > 0)
    {
        Scene.EngageHyperArtificialIntelligence();

        Scene.ApplyConditionEffects();
        currentRound++;

        if (currentRound % 2 == 0) {
            Console.WriteLine(" # Some more goblins have shown up!");

            string[] goblinNames = { "Grubnak", "Snaggletooth", "Ruknuk", "Zigzag", "Bogrot", "Nibbles", "Grizzle", "Muckmire", "Skreech", "Wartnose", "Dribble", "Snizzle", "Grubfoot", "Gnash", "Sludge", "Grogmar", "Spitfire", "Blister", "Crackle", "Fungus" };

            var rand = new Random();
            var nextGoblinName = goblinNames[rand.Next(20)];
            Scene.Entities.Add(new Goblin(nextGoblinName));
        }

        Narrator.ContinuePrompt();
        Console.Clear();
        Narrator.ShowRoundInfo(currentRound);
    }

 }

