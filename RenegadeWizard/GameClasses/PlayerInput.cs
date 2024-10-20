using RenegadeWizard.Entities;
using RenegadeWizard.Enums;
using RenegadeWizard.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RenegadeWizard.GameClasses
{
    public static class PlayerInput
    {

        public static List<string> Input = new();
        public static List<Interaction> PossibleActions { get; set; } = new();
        public static Interaction? ChosenAction { get; set; } = null;
        public static List<Entity> ActionParamters { get; set; } = new();

        public static void TakeInput()
        {

            Console.Write(" > ");
            Input = Console.ReadLine().ToLower().Split(" ")
                .Where(x => x.Length > 2)
                .ToList();

            if (Input.Count == 0)
            {
                Console.WriteLine(" ! No words were found");
                return;
            }

            if ( InputContains("help") )
            {
                Narrator.ShowHelp();
                return;
            }

            if (InputContains("actions"))
            {
                TheCompendium.ListCoreActions();
                return;
            }

            if (InputContains("spells"))
            {
                TheCompendium.ListSpells();
                return;
            }

            if (InputContains("travel"))
            {
                foreach (var word in Input)
                {
                    WorldNavigation.TravelTo(word);
                }
            }

            if (InputContains("locations"))
            {

            }


            if ( InputContains("info") )
            {
                foreach (var word in Input)
                {
                    TheCompendium.Search(word);
                }
                return;
            }

            // If it's not a command keyword, only then run the full Process
            ProcessInput();

        }

        private static void ProcessInput()
        {
            // TODO: Add a disambiugation function to allow the user to specifiy

            ChosenAction = null;
            ActionParamters.Clear();

            foreach (var entity in Scene.Entities)
            {
                if (InputContains(entity.Name.ToLower()))
                {
                    ActionParamters.Add(entity);
                }
            }

            foreach (var action in PossibleActions)
            {

                if (InputContains(action.Name.ToLower()))
                {
                    ChosenAction = action;
                }

                foreach (var synonym in action.Synonyms)
                {
                    if (InputContains(synonym.ToLower()))
                    {
                        ChosenAction = action;
                    }
                }
            }



        }

        private static bool InputContains(string word)
        {

            if (Input.Any(inputWord => inputWord.ToLower().Contains(word)))
            {
                return true;
            }

            if (Input.Any(inputWord => word.ToLower().Contains(inputWord)))
            {
                return true;
            }

            return false;

        }

    }
}
