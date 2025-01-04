using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArcana
{
    static internal class Controller
    {
        static internal string[] Input = null;

        static internal void Read()
        {
            Console.Write("> ");
            Input = Console.ReadLine().ToLower().Split(" ")
            .Where(x => x.Length > 2)
            .ToArray();

            if (Input.Count() == 0)
            {
                Console.WriteLine("! No words were found");
                Read();
            }

        }

        static internal void Write(string text)
        {

            string[] words = text.Split(' ');
            string currentLine = "";

            foreach (var word in words)
            {

                if (currentLine.Length + word.Length + 1 > 70)
                {
                    Console.WriteLine(currentLine.TrimEnd());
                    currentLine = "";
                }

                currentLine += word + " ";
            }


            Console.WriteLine(currentLine.TrimEnd());
            
        }

        internal static bool InputMatch(string b)
        {
            string[] a = Controller.Input;


            int distance = LevenshteinDistance(a.FirstOrDefault(), b);

            if (distance <= 2)
            {
                return true;
            }

            return false;
        }

        private static int LevenshteinDistance(string a, string b)
        {
            a = a.ToLower();
            b = b.ToLower();

            int[,] dp = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                dp[i, 0] = i;
            for (int j = 0; j <= b.Length; j++)
                dp[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;

                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[a.Length, b.Length];
        }

        internal static void ContinuePrompt()
        {

            Console.SetCursorPosition(5, Console.WindowHeight - 3 );

            Console.WriteLine(" - Press [SPACE] to continue...");

            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
            {
                // Wait until the space bar is pressed.
            }

            Console.Clear();


        }

        internal static void SlowWriteLines(string text)
        {
            string[] lines = text.Split('\n'); // Split the string into lines
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                Thread.Sleep(300); // Delay for the specified time
            }
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

                    Thread.Sleep(25); // Pause for 50 ms

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

        public static void Setbackground()
        {

            Console.SetBufferSize(400, 400);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    Console.Write(' ');
                    
                }
            }

            Console.SetCursorPosition(0, 0);
        }

        internal static void ShowTitleCard()
        {
            SlowWriteLines("" +

                "                                                                                                                           " + "\n" +
                "                                                                                                                           " + "\n" +
                "                                                                                                                           " + "\n" +
                "                                                                                                                           " + "\n" +
                "                                                                                                           *               " + "\n" +
                @"                                                                                               /\                         " + "\n" +
                @"       [][]    [][][]  []  []  [][][]  [][][]    []    [][]    [][][]                        //  \\     *       *         " + "\n" +
                @"       []  []  []      [][ []  []      []       [][]   []  []  []                          ///    \\----.   *             " + "\n" +
                @"       [][]    [][][]  [] ][]  [][][]  []  []  []  []  []  []  [][][]                     //       \\    \                " + "\n" +
                @"       []  []  []      []  []  []      []  []  []  []  []  []  []                        //  /\  /\/\\    |  *   *        " + "\n" +
                @"       []  []  [][][]  []  []  [][][]  [][][]  []  []  [][]    [][][]                    |/\/  \/    \\   |               " + "\n" +
                @"                                                                                        //        .  \\  /  *             " + "\n" +
                @"                                                                                       //    .         \       *          " + "\n" +
                @"                             W  W  W  WWWWW   WWWW     WW    WWWW    WWWW            //         V      \\                 " + "\n" +
                @"                             W  W  W    |         W   WWWW   W   WW  W   W          //     V         .  ||                " + "\n" +
                @"                             W  W  W    |     WWWW   W    W  WWWWW   W   W         ||         .          \\               " + "\n" +
                @"                             W  W  W    |    W       W    W  W    W  W   W        //      V        V     \\               " + "\n" +
                @"                              WW WW   WWWWWW  WWWW   W    W  W    W  WWWW        //    .      .            \\             " + "\n" +
                @"                                                                               ///                    V     \\\           " + "\n" +
                @"                                                                              ///  .   V        .         .  \\\          " + "\n" +
                "");
        }

    }
}
