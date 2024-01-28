using System.Collections;

namespace Snake
{
    partial class Game
    {
        protected class Field
        {
            int size_x = 15, size_y = 15;
            int[,] field_;
            public int getFieldElem(int x, int y)
            {
                return field_[y, x];
            }
            public void setFieldElem(int y, int x,  int val)
            {
                field_[x, y] = val;
            }
            public Tuple<int, int> GetSize()
            {
                return new Tuple<int, int>(size_x, size_y);
            }
            public Field(int size_x, int size_y)
            {
                field_ = new int[size_y, size_x];
                this.size_x = size_x;
                this.size_y = size_y;
                for (int i = 0; i < size_y; i++)
                {
                    for (int j  = 0; j < size_x; j++)
                    {
                        field_[i, j] = 0;
                    }
                }
            }

            public void DrawField()
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.Write('╔');
                for (int i = 0; i < size_x; i++)
                    Console.Write('═');
                Console.Write('╗');
                for (int i = 0; i <= size_y; i++)
                {
                    Console.SetCursorPosition(0, i + 1);
                    Console.Write('║');
                    Console.SetCursorPosition(size_x + 1, i + 1);
                    Console.Write('║');
                }
                Console.SetCursorPosition(0, size_y + 1);
                Console.Write('╚');
                for (int i = 0; i < size_x; i++)
                    Console.Write('═');
                Console.WriteLine('╝');
                Console.WriteLine("Score: 0");
            }
            public void DrawGameOver()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("""
                    ████ ████ █   █ ████
                    █  █ █  █ ██ ██ █
                    █    █  █ █ █ █ █
                    █ ██ ████ █   █ ████
                    █  █ █  █ █   █ █
                    ████ █  █ █   █ ████

                    ████ █  █ ████ ████
                    █  █ █  █ █    █   █
                    █  █ █  █ █    █   █ 
                    █  █ █  █ ████ ████ 
                    █  █ █  █ █    █ ██
                    ████  ██  ████ █  ██

                      PRESS ANY BUTTON

                    """);
                Console.SetCursorPosition(10, 16);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey(true);
                Console.Clear();
                Console.CursorVisible = true;
            }
        }
    }
}
