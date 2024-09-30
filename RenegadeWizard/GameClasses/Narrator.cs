using RenegadeWizard.Entities.Creatures;
using System.Linq.Expressions;
using System.Text;

namespace RenegadeWizard.GameClasses
{
    internal static class Narrator
    {
        public static void ShowRoundInfo(List<Interaction> actions)
        {

            Console.WriteLine();
            Console.WriteLine("0=[]:::::::::>  Fight!  <:::::::::[]=0");

            Console.Write($" - Actions: ");
            foreach (var action in actions.Where(x => x.IsSpell == false))
            {
                Console.Write($"[{action.Name}] ");
            }

            Console.WriteLine();

            Console.Write($" - Spells: ");
            foreach (var action in actions.Where(x => x.IsSpell == true))
            {
                Console.Write($"[{action.Name}] ");
            }


            //Console.Write($"{GetGrimVoiceline()}");

            Console.WriteLine();
            Console.WriteLine();


            var LivingCreatures = new EntQuery().SelectCreatures().SelectLiving().GetAll().OrderBy(ent => ent.Faction);

            foreach (var creature in LivingCreatures)
            {

                Console.Write($" -");
                if (creature.Faction == Enums.Factions.Player)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.Write($" [{creature.Name}]");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" the {creature.GetType().Name} has {creature.Health}hp");

                if (creature.DamageTakenLastRound > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" (-{creature.DamageTakenLastRound})");
                    Console.ForegroundColor = ConsoleColor.White;
                    creature.DamageTakenLastRound = 0;
                }

                if (creature.HealingLastRound > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" (+{creature.HealingLastRound})");
                    Console.ForegroundColor = ConsoleColor.White;
                    creature.HealingLastRound = 0;
                }

                if (creature.Modifiers.Any())
                {
                    string conditionStr = string.Join(", ", creature.Modifiers.Select(x => x.Name + $"({x.Duration})"));
                    Console.Write(" | " + conditionStr);
                }

                Console.WriteLine();

            }

            Console.WriteLine();

            if (Scene.Reinforcements.Count > 0 || Scene.Allies.Count > 0)
            {
                Console.WriteLine($" - Reinforcements: +{Scene.Reinforcements.Count} Hostiles, +{Scene.Allies.Count} Allies");
            }

            var deadCreatures = new EntQuery().SelectCreatures().SelectDead().GetAll().OrderBy(ent => ent.Faction);

            if (deadCreatures.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" - DEAD:");
                Console.ForegroundColor = ConsoleColor.White;

                foreach (var dead in deadCreatures)
                {
                    if (dead.Faction == Enums.Factions.Player)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write($" [{dead.Name}]");

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
            }


            Console.WriteLine();

        }
        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine(" - - - - - - - ?  Help  ? - - - - - - - ");
            Console.WriteLine("This is a 1st person, text combat system. Type any sentence that includes one of the commands:");
            Console.WriteLine("'Throw' 'Inspect' 'Consume'");
            Console.WriteLine(" 'Fireball' 'DiguiseAs' 'CharmMonster' 'ConjureFriend' 'Enrage' 'TitanBrawl' 'Immortality' 'TransferConditions' 'Heal' 'Invisibility' ");
            Console.WriteLine("EXAMPLE 'I throw beer at the goblin'");
            Console.WriteLine();
            Console.WriteLine("When you enter a command, it then searches for creature/item names. Anything in [ ] is a valid name.");
            Console.WriteLine("No word is case sensitive. Any word can shortened down to 3 letters. EXAMPLE throw -> thr");
            Console.WriteLine(" - - - - - - - ?  Help  ? - - - - - - - ");
            Console.WriteLine();
        }
        public static void ShowIntro()
        {

            Console.WriteLine("You're a wizard! At least that's what the strange bearded homeless man kept inisting...");
            Console.WriteLine("Fed up, he snapped his fingers and you were teleported to the (legally distinct) magical university Fogsnorts.");
            ContinuePrompt();

            Console.WriteLine();
            Console.WriteLine("Despite your half-hearted efforts, magic is hard as fuck.");
            Console.WriteLine("It turns out, a homeless man saying you're a wizard doesn't give you powers. To be fair, your name wasn't harry.");
            Console.WriteLine("Ah well, you can always cheat on the exams using GrimoirePT - gool ol' not so reliable.");
            ContinuePrompt();

            Console.WriteLine();
            //Console.WriteLine("You're off skipping lectures at Starblasters's Tavern of Magical Drinks.");
            //Console.WriteLine("Before you get a chance to enjoy your first pint, goblins attack!");

            Console.WriteLine("You're enjoying a delicious sandwich by the lake, when you see them...");
            Console.WriteLine("Geese. 3 of them, eying your sandwich with malicious intentions.");
            Console.WriteLine("You are [NotHarry]. Type 'help' to learn how to play.");
        }
        public static void ContinuePrompt()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine();
            Console.WriteLine(" - Press [SPACE] to continue...");

            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                // Wait until the space bar is pressed.
            }

            Console.ForegroundColor= ConsoleColor.White;

        }
        public static string GetConfusedNarrator()
        {
            var Expressions = new List<string>();
            Expressions.Add("What the fuck?");
            Expressions.Add("!?");
            Expressions.Add("You do you I guess?");
            Expressions.Add("Why?");
            Expressions.Add("Sigh...");
            Expressions.Add("Is intelligence your dump stat?");
            Expressions.Add("Did the goblins hit your head?");

            Random rnd = new Random();
            int r = rnd.Next(Expressions.Count());
            return Expressions[r];

        }

        private static readonly string[] connectors = new string[] { "AND", "BUT THEN", "ALL OF A SUDDEN", "HOWEVER", "THEN", "SUDDENLY", "MEANWHILE", "INSTANTLY", "OUT OF NOWHERE", "JUST AS", "IN THE BLINK OF AN EYE", "WITHOUT WARNING", "MOMENTS LATER", "UNEXPECTEDLY", "AS IF BY MAGIC", "NEVERTHELESS", "AS QUICK AS A FLASH", "BEFORE YOU KNOW IT", "IN A SPLIT SECOND", "RIGHT AFTER THAT", "BY SURPRISE", "IN THE NICK OF TIME", "WITHOUT HESITATION", "IN NO TIME", "WITH A SUDDEN JOLT" };

        public static string GetConnectorWord()
        {
            Random rnd = new Random();
            int r = rnd.Next(connectors.Count());
            return connectors[r];
        }

        private static readonly string[] contrasts = new string[] { "BUT", "HOWEVER", "IT TURNS OUT", "YET", "ALTHOUGH" };

        public static string GetContrastWord()
        {
            Random rnd = new Random();
            int r = rnd.Next(contrasts.Count());
            return contrasts[r];
        }

        private static readonly string[] attemptWords = new string[] { "tries", "attempts", "strives", "endeavors", "aims", "seeks", "undertakes", "ventures", "pursues", "experiments", "tests", "explores" };

        public static string GetAttemptWord()
        {
            Random rnd = new Random();
            int r = rnd.Next(attemptWords.Count());
            return attemptWords[r];
        }

        private static readonly string[] powerfulSynonyms = new string[] { "MIGHTY", "FORMIDABLE", "OVERWHELMING", "POTENT", "DESTRUCTIVE", "UNSTOPPABLE", "INTENSE", "CRUSHING", "FEROCIOUS", "DOMINANT", "DEVASTATING", "COLOSSAL", "RAW", "UNLEASHED", "WILD", "ALL POWERFUL", "DAMAGING", "MELON-CRUSHING", "JAR-OPENING", "EARTH-SHATTERING", "BALLOON POPPING", "HAIR-RAISING", "TABLE FLIPPING", "MASTERFUL", "SKILLED", "BRILLIANT" };

        public static string GetPowerfulWord()
        {
            Random rnd = new Random();
            int r = rnd.Next(powerfulSynonyms.Count());
            return powerfulSynonyms[r];
        }

        private static readonly string[] grimVoicelines = new string[] { "'This is not spellcasting advice!'", "'GrimoirePT may be wrong 99% of the time!'", "'I refuse to understand the question!'", "'Don't use these spells at home!'", "'Source: Spellcasting 101 for dummies'", "'Source: Trust me bro!'", "'Source: Some random wizzit post'", "'Positive thoughts are key!'", "'Remember when in mortal danger, don't die!'", "'Remember to follow all ethical guidelines!'", "'Hey, I've got just the spell'", "'Don't look so grim!'", "'Wow, you're in danger!'", "'Sure wouldn't want to be made of flesh like you!'", "'This magic will put a spell on you!'", "'Sign up for the premimum package!'", "'The revivify spell is now only £993,212,312,2!'", "'Remember, friendly fire is unavoidable!'", "'If it's raining cats and dogs, you messed up the ritual!'", "'You can't put a price on life, but yours is half off!'" };

        public static string GetGrimVoiceline()
        {
            Random rnd = new Random();
            int r = rnd.Next(grimVoicelines.Count());
            return grimVoicelines[r];
        }

        // Some very entertaining methods made by chatgpt for animations

        public static void Setbackground()
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

        public static void ShowExplosions()
        {
            Thread.Sleep(500);

            int cursorPosX = Console.CursorLeft;
            int cursorPosY = Console.CursorTop;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Random random = new Random();

            // Multiple explosions
            for (int i = 0; i < 10; i++) // Number of explosions
            {
                int x = random.Next(0, width - 10);
                int y = random.Next(0, height - 5);

                Console.SetCursorPosition(x, y);
                Console.WriteLine("   \\|/   ");
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine(" -- * -- ");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("   /|\\   ");
                Thread.Sleep(200); // Pause for 300 ms

                // Explosion dispersing
                Console.SetCursorPosition(x, y);
                Console.WriteLine("   . .   ");
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine(" .     . ");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("   . .   ");
                Thread.Sleep(100); // Pause for 200 ms

            }

            Console.SetCursorPosition(cursorPosX, cursorPosY);

        }

        public static void ShowBoom()
        {
            Thread.Sleep(1000);

            int cursorPosX = Console.CursorLeft;
            int cursorPosY = Console.CursorTop;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Random random = new Random();

            int centerX = width / 2;
            int centerY = height / 2;

            for (int i = 0; i < 1; i++) // Number of explosions
            {
                int radius = 50; // Increased radius for a bigger explosion

                // Create explosion effect
                for (int r = 0; r < radius; r++)
                {
                    for (int angle = 0; angle < 360; angle += 10) // Draw explosion in circular pattern
                    {
                        int x = centerX + (int)(r * Math.Cos(angle * Math.PI / 180));
                        int y = centerY + (int)(r * Math.Sin(angle * Math.PI / 180) / 2);

                        if (x >= 0 && x < width && y >= 0 && y < height)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.WriteLine("*");
                        }
                    }

                    Thread.Sleep(5); // Pause for 50 ms

                    // Clear the explosion effect
                    for (int angle = 0; angle < 360; angle += 10)
                    {
                        int x = centerX + (int)(r * Math.Cos(angle * Math.PI / 180));
                        int y = centerY + (int)(r * Math.Sin(angle * Math.PI / 180));

                        if (x >= 0 && x < width && y >= 0 && y < height)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.WriteLine(" ");
                        }
                    }
                }

                // Particle effect for explosion fading
                for (int k = 0; k < 5; k++) // More particles for a bigger effect
                {
                    for (int j = 0; j < 120; j++) // More particles
                    {
                        int px = centerX + random.Next(-radius, radius);
                        int py = centerY + random.Next(-radius, radius);
                        if (px >= 0 && px < width && py >= 0 && py < height)
                        {
                            Console.SetCursorPosition(px, py);
                            Console.WriteLine("*");
                        }
                    }
                    Thread.Sleep(100); // Pause for 100 ms between each particle effect frame

                    // Clear particles
                    for (int j = 0; j < 40; j++)
                    {
                        int px = centerX + random.Next(-radius, radius);
                        int py = centerY + random.Next(-radius, radius);
                        if (px >= 0 && px < width && py >= 0 && py < height)
                        {
                            Console.SetCursorPosition(px, py);
                            Console.WriteLine(" ");
                        }
                    }
                }

                Thread.Sleep(300); // Pause after explosion completely fades
            }


            Console.SetCursorPosition(cursorPosX, cursorPosY);

        }

        public static void ShowLightning()
        {
            Thread.Sleep(200);

            int cursorPosX = Console.CursorLeft;
            int cursorPosY = Console.CursorTop;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Random random = new Random();

            // Multiple explosions
            for (int i = 0; i < 5; i++) // Number of lightning bolts
            {
                int x = random.Next(0, width - 10);
                int startY = 1;
                int endY = height;

                // Create lightning fork
                for (int y = startY; y < endY; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("|");

                    // Randomly fork the lightning
                    if (random.Next(0, 2) == 0) // 25% chance to fork
                    {

                        int forkX = x + random.Next(-3, 4);
                        if (forkX >= 0 && forkX < width)
                        {
                            Console.SetCursorPosition(forkX, y);
                            Console.WriteLine("V");
                        }
                    }

                    Thread.Sleep(30); // Pause for 50 ms

                    // Clear lightning path
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(" ");
                }

                for (int k = 0; k < 7; k++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            int particleX = x + random.Next(-3, 4);
                            int particleY = startY + random.Next(0, endY - startY);
                            if (particleX >= 0 && particleX < width && particleY < height)
                            {
                                Console.SetCursorPosition(particleX, particleY);
                                Console.WriteLine("*");
                            }
                        }
                        Thread.Sleep(50); // Pause for 100 ms between each particle effect frame

                        // Clear particles
                        for (int j = 0; j < 3; j++)
                        {
                            int particleX = x + random.Next(-3, 4);
                            int particleY = startY + random.Next(0, endY - startY);
                            if (particleX >= 0 && particleX < width && particleY < height)
                            {
                                Console.SetCursorPosition(particleX, particleY);
                                Console.WriteLine(" ");
                            }
                        }
                    }

                Thread.Sleep(100); // Pause after lightning completely vanishes
            }



            Console.SetCursorPosition(cursorPosX, cursorPosY);

        }

        public static void ShowDaggers()
        {
            Thread.Sleep(200);

            int cursorPosX = Console.CursorLeft;
            int cursorPosY = Console.CursorTop;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Random random = new Random();

            // Multiple explosions
            for (int i = 0; i < 20; i++) // Number of daggers
            {
                int x = random.Next(0, width - 10);
                int y = random.Next(0, height - 1);

                for (int j = 0; j < 5; j++) // Dagger moving to the right
                {
                    Console.SetCursorPosition(x + j, y);
                    Console.WriteLine("0=[]===> ");
                    Thread.Sleep(70); // Pause for 100 ms

                    // Clear the dagger position for the next frame
                    Console.SetCursorPosition(x + j, y);
                    Console.WriteLine("          ");
                }

                // Particle effect for dagger vanishing
                for (int k = 0; k < 3; k++)
                {
                    Console.SetCursorPosition(x + 5, y);
                    if (k == 0)
                    {
                        Console.WriteLine("*   *   *");
                    }
                    else if (k == 1)
                    {
                        Console.WriteLine(" . * * . ");
                    }
                    else
                    {
                        Console.WriteLine("  .   .  ");
                    }
                    Thread.Sleep(70); // Pause for 100 ms between each particle effect frame

                    // Clear particle effect after each frame
                    Console.SetCursorPosition(x + 5, y);
                    Console.WriteLine("          ");
                }

                Thread.Sleep(200); // Pause after dagger completely vanishes
            }

            Console.SetCursorPosition(cursorPosX, cursorPosY);

        }

        public static void ShowSparks()
        {
            Thread.Sleep(200);

            int cursorPosX = Console.CursorLeft;
            int cursorPosY = Console.CursorTop;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            Random random = new Random();

            for (int i = 0; i < 20; i++) // Number of sparks
            {
                int x = random.Next(0, width - 7); // Adjusting width for the spark stages
                int y = random.Next(0, height - 3);

                // Stage 1: Spark igniting
                Console.SetCursorPosition(x, y);
                Console.WriteLine("   *   ");
                Thread.Sleep(200);

                // Stage 2: Spark expanding
                Console.SetCursorPosition(x, y);
                Console.WriteLine("  ***  ");
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine(" ***** ");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("  ***  ");
                Thread.Sleep(300);

                // Stage 3: Spark fading out
                Console.SetCursorPosition(x, y);
                Console.WriteLine("  . .  ");
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine(" .     . ");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("  . .  ");
                Thread.Sleep(150);

                // Clear the spark
                Console.SetCursorPosition(x, y);
                Console.WriteLine("       ");
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine("       ");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("       ");
                Thread.Sleep(100);
            }


            Console.SetCursorPosition(cursorPosX, cursorPosY);

        }

    }
}
