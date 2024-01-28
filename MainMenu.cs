using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class MainMenu
    {
        public static void DrawGames()
        {
            Console.WriteLine("""
                ████ ████ █   █ ████ █████
                █  █ █  █ ██ ██ █    █   █
                █    █  █ █ █ █ █    ███
                █ ██ ████ █   █ ████   ███
                █  █ █  █ █   █ █    █   █
                ████ █  █ █   █ ████ █████

                """);
            //Console.BackgroundColor =  ConsoleColor.White;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("    Snake");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    DrawModes();
                    return;
                }
            }
        }
        public static void DrawModes()
        {
            Console.Clear();
            Console.WriteLine("""
                    1 PLAYER
                    2 PLAYERS
                """);
            int cur = 0;
            while (true)
            {
                Console.SetCursorPosition(0, (cur > 0) ? (0) : (1));
                Console.Write(" ");
                Console.SetCursorPosition(0, cur);
                Console.Write(">");
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    if (cur == 0)
                    {
                        Thread.Sleep(500);
                        Console.Clear();
                        Game game = new Game(30, 15);
                        game.Run();
                        return;
                    }
                    if (cur == 1)
                    {
                        Thread.Sleep(500);
                        Console.Clear();
                        Game game = new Game(30, 15, 2);
                        game.Run();
                        return;
                    }
                }
                cur++;
                cur %= 2;
            }
        }
    }
}
