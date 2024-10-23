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

    public class Goose : Conversation
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
}
