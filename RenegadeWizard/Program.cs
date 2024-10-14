using RenegadeWizard.Components;
using RenegadeWizard.Modifiers;
using RenegadeWizard.Entities;
using RenegadeWizard.Entities.Creatures;
using RenegadeWizard.GameClasses;
using System;
using System.Linq;
using System.Reflection;
using System.Numerics;
using RenegadeWizard.Entities.Creatures.Geese;

List<string> consoleBuffer = new List<string>();
Console.SetBufferSize(300, 400);
Console.BackgroundColor = ConsoleColor.Blue;
Console.ForegroundColor = ConsoleColor.White;
Narrator.Setbackground();


WorldNavigation.ExplorationLoop();

Narrator.Setbackground();


Scene.NextRound();
PlayerFunctionality.AddSpells();
Narrator.ShowRoundInfo();


while ( Scene.Entities.Any(x => x.IsPlayerControlled == true && x.IsDestroyed == false) )
{
    var players = new EntQuery().SelectPlayers().SelectLiving().GetAll();
    foreach (Entity ent in players)
    {
        if (ent.IsDestroyed) { continue; }
        PlayerFunctionality.PlayerTurn(ent);
        Thread.Sleep(750);

    }

    var npcs = new EntQuery().SelectNpcs().SelectLiving().GetAll();
    foreach (Entity ent in npcs)
    {
        if (ent.IsDestroyed) { continue; }
        ent.TakeTurn();
        Thread.Sleep(750);

    }

    if (npcs.Any())
    {
        Console.WriteLine();
    }

    var ents = Scene.Entities;
    foreach (var ent in ents)
    {
        ent.ApplyRoundEndEffects();
    }

    Scene.Round++;
    Scene.NextRound();

    Narrator.ContinuePrompt();

    Console.Clear();
    Narrator.Setbackground();
    Narrator.ShowRoundInfo();

}

Console.WriteLine();
Console.WriteLine();
Narrator.ShowBoom();


Console.WriteLine("                                      You've died womp womp                              ");

Narrator.ContinuePrompt();