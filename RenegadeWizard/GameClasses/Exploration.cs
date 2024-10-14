﻿
namespace RenegadeWizard.GameClasses
{
    static class WorldNavigation
    {

        static (int x, int y) position = (2, 2);

        static char[,] array = new char[,]
        {
            { 'M', 'M', 'M', 'M', 'M' },
            { 'M', 'F', 'F', 'F', 'M' },
            { 'M', 'F', 'D', 'F', 'M' },
            { 'M', 'F', 'F', 'F', 'M' },
            { 'M', 'M', 'M', 'M', 'M' }
        };

        static public void ExplorationLoop()
        {

            while (true)
            {
                var prevPos = position;

                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Console.WriteLine("Up key pressed!");
                    position.y -= 1;
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    Console.WriteLine("Down key pressed!");
                    position.y += 1;
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Console.WriteLine("Left key pressed!");
                    position.x -= 1;
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Console.WriteLine("Right key pressed!");
                    position.x += 1;
                }

                char locationChar = array[position.y, position.x];

                if (locationChar == 'D')
                {
                    Console.WriteLine("Welcome to defiance!" + position.x + position.y);
                }

                if (locationChar == 'F')
                {
                    Console.WriteLine("You are inside the forest of fangs, danger lurks here" + position.x + position.y);
                }

                if (locationChar == 'M')
                {
                    Console.WriteLine("You hit a mountain range and are forced to turn back" + position.x + position.y);
                    position = prevPos;
                }

            }
        }

        static void LoadForestScene()
        {
            Scene.ResetScene();

        }
    }
}
