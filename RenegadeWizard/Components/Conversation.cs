using RenegadeWizard.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RenegadeWizard.Components
{
    public class Conversation
    {
        public virtual void Listen(string name)
        {

        }
    }

    public class GooseConversation : Conversation
    {

        public override void Listen(string name)
        {
            if (PlayerInput.InputContains("hello"))
            {
                Console.WriteLine($" @ {name}: HOOOOOOOOOOOOOOONNNNNNNKK, I can speak human! Finally all that duolingoing has paid off. Got [bread]");
                return;
            }

            if (PlayerInput.InputContains("honk"))
            {
                Console.WriteLine($" @ {name}: HONK HONK [HELLO]");
                return;
            }

            if (PlayerInput.InputContains("fuck"))
            {
                Console.WriteLine($" @ {name}: swearing is immature");
                return;
            }

            if (PlayerInput.InputContains("bread"))
            {
                Console.WriteLine($" @ {name}: love lakes, love bread, ate' knees simple as");
                return;
            }

            Console.WriteLine($" @ {name}: [HONK]");
        }

    }    
    
    public class FishermanConversation : Conversation
    {

        override public void Listen(string name)
        {
            if (PlayerInput.InputContains("hello"))
            {
                Console.WriteLine($" @ {name}: I am a fisherman!");
                return;
            }

            Console.WriteLine($" @ {name}: [HONK]");
        }

    }    
    
    public class ScholarConversation : Conversation
    {

        override public void Listen(string name)
        {

            Console.Write($" @ {name}: ");

            if (PlayerInput.InputContains("passed"))
            {
                Narrator.AutoLine("'I do not know why before you ask. You are far away from <defiance>, and I cannot fathom why someone else would be here <too>.'");
                Console.WriteLine();
                return;
            }

            if (PlayerInput.InputContains("too"))
            {
                Narrator.AutoLine("'I am inspecting the ");
                Console.WriteLine();
                return;
            }


            Narrator.AutoLine( "He shuts his dusty tome with a hefty motion, all the while his eyes are focused on you." +
                               " 'Take it easy. I've been keeping watch, while you were <passed> out.'"
                             );
            Console.WriteLine();
        }

    }    
    
    
    public class PriestessConversation : Conversation
    {

        override public void Listen(string name)
        {

            Console.WriteLine($" @ {name}: ");

            if (PlayerInput.InputContains("passed"))
            {
                Narrator.AutoLine("'I do not know why before you ask. You are far away from <defiance>, and I cannot fathom why someone else would be here <too>.'");
                Console.WriteLine();
                return;
            }

            if (PlayerInput.InputContains("too"))
            {
                Narrator.AutoLine("'I am inspecting the ");
                Console.WriteLine();
                return;
            }


            Narrator.AutoLine( "They flash you an awkward smile, their eyes laced with concern about your situation. " +
                               " 'So um, strange <place> to take a respite. '"
                             );
            Console.WriteLine();
        }

    }
}
