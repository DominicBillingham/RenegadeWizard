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

Narrator.SetupConsole();

while (true)
{
    Scene.Update();
    Narrator.DescribeScene();
    PlayerInput.TakeInput();








    Narrator.ContinuePrompt();
}

while ( Scene.Entities.Any(x => x.IsPlayerControlled == true && x.IsDestroyed == false) )
{
    var players = new EntQuery().SelectPlayers().SelectLiving().GetAll();
    foreach (Entity ent in players)
    {
        if (ent.IsDestroyed) { continue; }
        //PlayerFunctionality.PlayerTurn(ent);
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


    Narrator.ContinuePrompt();

    Console.Clear();
    Narrator.Setbackground();
    Narrator.DescribeScene();

}

