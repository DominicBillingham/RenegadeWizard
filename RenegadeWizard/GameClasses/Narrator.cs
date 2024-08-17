using System.Linq.Expressions;
using System.Text;

namespace RenegadeWizard.GameClasses
{
    internal static class Narrator
    {
        public static void ShowRoundInfo(int round)
        {

            Console.WriteLine();
            Console.WriteLine("0=[]:::::::::>  Round " + round + "  <:::::::::[]=0");

            Console.Write(" - Nearby items: ");
            foreach (var item in Scene.GetItems())
            {
                Console.Write($"[{item.Name}] ");
            }
            Console.WriteLine();

            foreach (var creature in Scene.GetCreatures())
            {
                Console.Write($" - [{creature.Name}] ({creature.GetType().Name}) has {creature.Health}hp ");
                if (creature.DamageTakenLastRound > 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write( $"(-{creature.DamageTakenLastRound})" );
                    Console.ForegroundColor = ConsoleColor.White;
                }

                creature.DamageTakenLastRound = 0;

                if (creature.Conditions.Any())
                {

                    string conditionStr = string.Join(", ", creature.Conditions.Select(x => x.Name + $"({x.Duration})"));
                    Console.Write(" | " + conditionStr);
                }

                Console.WriteLine();

            }

        }
        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine(" - - - - - - - ?  Help  ? - - - - - - - ");
            Console.WriteLine("This is a 1st person, text combat system. Type any sentence that includes one of the commands:");
            Console.WriteLine("'Throw' 'Inspect' 'Consume' 'Kick' ");
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
            Console.WriteLine("You're off skipping lectures at Starblasters's Tavern of Magical Drinks.");
            Console.WriteLine("Before you get a chance to enjoy your first pint, goblins attack!");
            Console.WriteLine("You are [NotHarry]. Type 'help' to learn how to play.");
        }
        public static void ContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static readonly string[] connectors = new string[] { "AND", "BUT THEN", "ALL OF A SUDDEN", "HOWEVER", "THEN", "SUDDENLY", "MEANWHILE", "INSTANTLY", "OUT OF NOWHERE", "JUST AS", "IN THE BLINK OF AN EYE", "WITHOUT WARNING", "MOMENTS LATER", "UNEXPECTEDLY", "AS IF BY MAGIC", "NEVERTHELESS", "AS QUICK AS A FLASH", "BEFORE YOU KNOW IT", "IN A SPLIT SECOND", "RIGHT AFTER THAT", "BY SURPRISE", "IN THE NICK OF TIME", "WITHOUT HESITATION", "IN NO TIME", "WITH A SUDDEN JOLT" };

        private static readonly string[] contrasts = new string[] { "BUT", "HOWEVER", "IT TURNS OUT", "YET", "ALTHOUGH" };

        private static readonly string[] attemptWords = new string[] { "tries", "attempts", "strives", "endeavors", "aims", "seeks", "undertakes", "ventures", "pursues", "experiments", "tests", "explores" };

        public static string GetConnectorWord()
        {
            Random rnd = new Random();
            int r = rnd.Next(connectors.Count());
            return connectors[r];
        }

        public static string GetContrastWord()
        {
            Random rnd = new Random();
            int r = rnd.Next(contrasts.Count());
            return contrasts[r];
        }

        public static string GetAttemptWord()
        {
            Random rnd = new Random();
            int r = rnd.Next(attemptWords.Count());
            return attemptWords[r];
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


    }
}
