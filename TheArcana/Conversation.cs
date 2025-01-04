using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArcana
{
    internal class Conversation
    {

        private Dictionary<string, Action> Responses = new()
        {
            { "Scarf", () => Console.WriteLine("Going to the bar...") },
            { "Hair", () => Console.WriteLine("Going to the church...") },
            { "Mask", () => Console.WriteLine("Going to the gate...") }
        };


        internal void Listen()
        {


            // Check input against commands
            foreach (var res in Responses)
            {
                if (Controller.InputMatch(res.Key))
                {
                    res.Value();
                }
            }
        }




    }

}
