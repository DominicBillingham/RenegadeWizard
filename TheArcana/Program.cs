using System.Linq.Expressions;
using TheArcana;






Controller.Setbackground();

//Controller.ShowTitleCard();
//Controller.ContinuePrompt();

//Help.DisplayHelp();

//while (true)
//{

//    Controller.Read();

//    if (  Controller.InputMatch("UNDERSTAND" )  )
//    {
//        break;
//    }

//    if (Controller.InputMatch("WORDS") || Controller.InputMatch("MARKED"))
//    {
//        Console.WriteLine(@"
//Talking to yourself, perhaps you really are a fool? 
//        ");
//    }
//    else
//    {
//        Console.Clear();
//        Help.DisplayHelp();

//    }

//}

//Controller.ContinuePrompt();

Controller.SlowWriteLines(@"
Shapes whirl in your fractured mind, the darkness familiar but still unsettling. 
Words of [Justice] echo across your sensory expanse.

'To give a name, is to bind them to your whims. Symbols are the
ritual in which we tame the material, conceptual, and even demonic.'

'The fool believes they may choose a name, they are mistaken. 
You may only choose a mask in which to hide your blasphemous truth.'

'And so, what is your name?'
");

Controller.Read();

Controller.SlowWriteLines(@"
With a name, your existence slowly crawls back to you. 
Alas your body still refuses to move, even when a shadow looms over you.

The cleric looks on with morbid curiosity, her body calm despite the strange circumstance.
'No signs of agony, the flesh is unscarred'
'No signs of terror, the mind is still'
'And no signs of addiction, the blood untained'

She fumbles through your notebook, looking through it's blank pages.
'Blank pages to match an unstained mask I suppose.'
With her hefty sigh, you begin to shift.

");






//Player.Name = Controller.Input.FirstOrDefault();

//Console.WriteLine(Player.Name); 


//while (true)
//{
//    string input = Console.ReadLine()?.ToLower();
//    var convo = new Conversation();
//    convo.Listen();


//}



