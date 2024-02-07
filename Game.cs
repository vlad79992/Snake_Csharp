using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Drawing;

namespace Snake
{
    partial class Game
    {
        private Field field;
        bool inGame = true;
        int player = 1;
        struct Pos 
        {  
            public int x; public int y;
            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        //LinkedList<Pos> snake;
        public Game()
        {
            field = new Field(15, 15);
        }
        public Game(int size_x, int size_y)
        {
            field = new Field(size_x, size_y);
        }
        public Game(int size_x, int size_y, int p)
        {
            field = new Field(size_x, size_y);
            player = p;
        }
        int key = (int)ConsoleKey.W, 
            newKey = (int)ConsoleKey.W;
        int key2 = (int)ConsoleKey.DownArrow,
            newKey2 = (int)ConsoleKey.DownArrow;
        private void GetKey()
        {
            while (inGame)
            {
                int key = (int)Console.ReadKey(true).Key;
                if (key != (int)ConsoleKey.W && key != (int)ConsoleKey.A &&
                    key != (int)ConsoleKey.S && key != (int)ConsoleKey.D &&
                    key != (int)ConsoleKey.UpArrow && key != (int)ConsoleKey.LeftArrow &&
                    key != (int)ConsoleKey.DownArrow && key != (int)ConsoleKey.RightArrow &&
                    key != (int)ConsoleKey.Escape)
                    continue;
                { 
                    if (this.key == (int)ConsoleKey.W && key == (int)ConsoleKey.S)
                        continue;
                    if (this.key == (int)ConsoleKey.A && key == (int)ConsoleKey.D)
                        continue;
                    if (this.key == (int)ConsoleKey.S && key == (int)ConsoleKey.W)
                        continue;
                    if (this.key == (int)ConsoleKey.D && key == (int)ConsoleKey.A)
                        continue;

                    if (this.key2 == (int)ConsoleKey.UpArrow && key == (int)ConsoleKey.DownArrow)
                        continue;
                    if (this.key2 == (int)ConsoleKey.LeftArrow && key == (int)ConsoleKey.RightArrow)
                        continue;
                    if (this.key2 == (int)ConsoleKey.DownArrow && key == (int)ConsoleKey.UpArrow)
                        continue;
                    if (this.key2 == (int)ConsoleKey.LeftArrow && key == (int)ConsoleKey.RightArrow)
                        continue;
                }
                if (player == 1)
                    newKey = key;
                else
                {
                    if (key == (int)ConsoleKey.W || key == (int)ConsoleKey.A ||
                        key == (int)ConsoleKey.S || key == (int)ConsoleKey.D)
                        newKey = (int)key;
                    if (key == (int)ConsoleKey.UpArrow || key == (int)ConsoleKey.LeftArrow ||
                        key == (int)ConsoleKey.DownArrow || key == (int)ConsoleKey.RightArrow)
                        newKey2 = (int)key;
                }
                if (key == (int)ConsoleKey.Escape)
                {
                    inGame = false; break;
                }
            }
        }
        private void AddBonus()
        {
            var rand = new Random();
            if (inGame)
            {
                while(true)
                {
                    int rand_x = rand.Next(field.GetSize().Item1),
                        rand_y = rand.Next(field.GetSize().Item2);
                    if (field.getFieldElem(rand_x, rand_y) <= 0)
                    {
                        field.setFieldElem(rand_x, rand_y, -1);
                        Console.SetCursorPosition(rand_x + 1, rand_y + 1);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("*");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
            }
        }
        private void Move(ConsoleColor color, LinkedList<Pos> snake, int curPlayer)
        {
            while (inGame)
            {
                key = newKey; 
                key2 = newKey2;
                int x = snake.First().x , y = snake.First().y;
                Console.ForegroundColor = color;
                if (player == 1 && (key == (int)ConsoleKey.W || key == (int)ConsoleKey.UpArrow) || 
                    (player == 2) && (curPlayer == 1 && key == (int)ConsoleKey.W || curPlayer == 2 && key2 == (int)ConsoleKey.UpArrow))
                {
                    Console.SetCursorPosition(snake.Last().x + 1, snake.Last().y + 1);
                    if (field.getFieldElem(x, (y + field.GetSize().Item2 - 1) % field.GetSize().Item2) != -1)
                    {
                        Console.Write(' ');
                        field.setFieldElem(snake.Last().x, snake.Last().y, 0);
                        snake.RemoveLast();
                    }
                    else
                    {
                        AddBonus();
                        Console.SetCursorPosition(7, field.GetSize().Item2 + 2);
                        Console.Write(snake.Count);
                    }
                    Console.ForegroundColor = color;
                    snake.AddFirst(new Pos(x, (y + field.GetSize().Item2 - 1) % field.GetSize().Item2));
                    Console.SetCursorPosition(snake.First().x + 1, snake.First().y + 1);
                    if (field.getFieldElem(snake.First().x, snake.First().y) == player) inGame = false;
                    field.setFieldElem(snake.First().x, snake.First().y, player);
                    Console.Write('█');
                }
                if (player == 1 && (key == (int)ConsoleKey.S || key == (int)ConsoleKey.DownArrow) ||
                    (player == 2) && (curPlayer == 1 && key == (int)ConsoleKey.S || curPlayer == 2 && key2 == (int)ConsoleKey.DownArrow))
                {
                        Console.SetCursorPosition(snake.Last().x + 1, snake.Last().y + 1);
                        if (field.getFieldElem(x, (y + 1) % field.GetSize().Item2) != -1)
                        {
                            Console.Write(' ');
                            field.setFieldElem(snake.Last().x, snake.Last().y, 0);
                            snake.RemoveLast();
                        }
                        else
                        {
                            AddBonus();
                            Console.SetCursorPosition(7, field.GetSize().Item2 + 2);
                            Console.Write(snake.Count);
                        }
                        Console.ForegroundColor = color;
                        snake.AddFirst(new Pos(x, (y + 1) % field.GetSize().Item2));
                        Console.SetCursorPosition(snake.First().x + 1, snake.First().y + 1);
                        if (field.getFieldElem(snake.First().x, snake.First().y) == player) inGame = false;
                        field.setFieldElem(snake.First().x, snake.First().y, player);
                        Console.Write('█');
                }
                if (player == 1 && (key == (int)ConsoleKey.A || key == (int)ConsoleKey.LeftArrow)
                    || (player == 2) && (curPlayer == 1 && key == (int)ConsoleKey.A || curPlayer == 2 && key2 == (int)ConsoleKey.LeftArrow))
                {
                        Console.SetCursorPosition(snake.Last().x + 1, snake.Last().y + 1);
                        if (field.getFieldElem((x + field.GetSize().Item1 - 1) % field.GetSize().Item1, y) != -1)
                        {
                            Console.Write(' ');
                            field.setFieldElem(snake.Last().x, snake.Last().y, 0);
                            snake.RemoveLast();
                        }
                        else
                        {
                            AddBonus();
                            Console.SetCursorPosition(7, field.GetSize().Item2 + 2);
                            Console.Write(snake.Count);
                        }
                        Console.ForegroundColor = color;
                        snake.AddFirst(new Pos((x + field.GetSize().Item1 - 1) % field.GetSize().Item1, y));
                        Console.SetCursorPosition(snake.First().x + 1, snake.First().y + 1);
                        if (field.getFieldElem(snake.First().x, snake.First().y) == player) inGame = false;
                        field.setFieldElem(snake.First().x, snake.First().y, player);
                        Console.Write('█');
                }
                if (player == 1 && (key == (int)ConsoleKey.D || key == (int)ConsoleKey.RightArrow)
                    || (player == 2) && (curPlayer == 1 && key == (int)ConsoleKey.D || curPlayer == 2 && key2 == (int)ConsoleKey.RightArrow))
                {
                        Console.SetCursorPosition(snake.Last().x + 1, snake.Last().y + 1);
                        if (field.getFieldElem((x + 1) % field.GetSize().Item1, y) != -1)
                        {
                            Console.Write(' ');
                            field.setFieldElem(snake.Last().x, snake.Last().y, 0);
                            snake.RemoveLast();
                        }
                        else
                        {
                            AddBonus();
                            Console.SetCursorPosition(7, field.GetSize().Item2 + 2);
                            Console.Write(snake.Count);
                        }
                        Console.ForegroundColor = color;
                        snake.AddFirst(new Pos((x + 1) % field.GetSize().Item1, y));
                        Console.SetCursorPosition(snake.First().x + 1, snake.First().y + 1);
                        if (field.getFieldElem(snake.First().x, snake.First().y) == player) inGame = false;
                        field.setFieldElem(snake.First().x, snake.First().y, player);
                        Console.Write('█');
                }
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(144);
            }
        }
        
        public void Run()
        {
            field.DrawField();
            AddBonus();
            if (player == 1)
            {
                LinkedList<Pos> snake = new LinkedList<Pos>();
                snake.AddFirst(new Pos(field.GetSize().Item1 / 2, field.GetSize().Item2 / 2));
                Thread inputThread = new Thread(GetKey);
                Thread moveThread = new Thread(() => Move(ConsoleColor.Green, snake, 1));
                inputThread.Start();
                moveThread.Start();
                while (inGame)
                {
                    //Console.SetCursorPosition(7, 7);
                    //Console.Write(key);
                }

                field.DrawGameOver();
                inputThread.Join();
                moveThread.Join();
            }
            if (player != 1)
            {
                LinkedList<Pos> snake1 = new LinkedList<Pos>(); snake1.AddFirst(new Pos(field.GetSize().Item1 / 2, field.GetSize().Item2 - 1));
                LinkedList<Pos> snake2 = new LinkedList<Pos>(); snake2.AddFirst(new Pos(field.GetSize().Item1 / 2, 1));
                Thread inputThread = new Thread(GetKey);
                Thread moveThreadP1 = new Thread(() => Move(ConsoleColor.Black, snake1, 1));
                Thread moveThreadP2 = new Thread(() => Move(ConsoleColor.White, snake2, 2));
                inputThread.Start();
                moveThreadP1.Start();
                Thread.Sleep(50);
                moveThreadP2.Start();
                while (inGame)
                {
                    //Console.SetCursorPosition(7, 7);
                    //Console.Write(key);
                }

                field.DrawGameOver();
                inputThread.Join();
                moveThreadP1.Join(); moveThreadP2.Join();
            }
        }
    }
}
