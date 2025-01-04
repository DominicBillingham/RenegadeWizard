using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArcana
{
    static internal class Help
    {

        static internal void DisplayHelp()
        {
            Controller.SlowWriteLines(
                @"
This is a first person, text based adventure game. To do anything
start by typing a sentence containing any {COMMAND} word.
> 'I {TRAVEL} to the [WOODS]'

[WOODS] is a name, many commands require name(s) to be provided.
Nothing in the game is case sensitive, and you can make tpyos.

When talking to others, some <WORDS> will cause new lines of dialogue
Not all of them will be <MARKED> clearly, so experiment!

Type {COMMANDS} to see a list of commands.
Type {HELP} to see this pop up again.
Type {UNDERSTAND} to progress
    ");
        }




    }
}
