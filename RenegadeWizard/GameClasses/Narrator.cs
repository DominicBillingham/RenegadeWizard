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

            //Console.Write(" - Nearby items: ");

            //var items = new EntQuery().SelectItems().GetAll();
            //foreach (var item in items)
            //{
            //    Console.Write($"[{item.Name}] ");
            //}
            //Console.WriteLine();


            Console.Write($" - Grim's Spells: ");
            foreach (var action in actions)
            {
                Console.Write($"[{action.Name}] ");
            }
            //Console.Write($"{GetGrimVoiceline()}");
            Console.WriteLine();


            var creatures = new EntQuery().SelectCreatures().GetAll();  
            foreach (var creature in creatures)
            {
                if (creature.IsDestroyed == false)
                {

                    Console.Write($" -");
                    if (creature.Faction == Enums.Factions.Player)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write($" [{creature.Name}]");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" the {creature.GetType().Name} has {creature.Health}hp");

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" - [{creature.Name}] the {creature.GetType().Name} is DEAD");
                    Console.ForegroundColor = ConsoleColor.White;
                    creature.DamageTakenLastRound = 0;
                }


                if (creature.DamageTakenLastRound > 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write( $" (-{creature.DamageTakenLastRound})" );
                    Console.ForegroundColor = ConsoleColor.White;
                    creature.DamageTakenLastRound = 0;
                }

                if (creature.HealingLastRound > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" ({creature.HealingLastRound})");
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

            if (Scene.Reinforcements.Count > 0 )
            {
                Console.WriteLine($" - +{Scene.Reinforcements.Count} Reinforcements");
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
            Console.WriteLine();
            Console.WriteLine(" - Press [SPACE] to continue...");

            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                // Wait until the space bar is pressed.
            }

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












    }
}
