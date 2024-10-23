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
Narrator.ShowTitleCard();
Thread.Sleep(1000);

while (true)
{
    Scene.Update();
    Narrator.DescribeScene();

    PlayerInput.TakeInput();
    PlayerInput.ChosenAction?.Execute();

    Narrator.ContinuePrompt();
}
