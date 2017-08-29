using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star_pathfinding
{
    abstract class Being
    {
        internal Vector2 Position;
        internal char Appearance { get; set; }

        public Being(int x, int y, char appearance = ' ')
        {
            Position = new Vector2(x, y);
            Appearance = appearance;
        }
        public void Move(int xAxis, int yAxis)
        {
            if (IsAllowedToMove(xAxis, yAxis))
            {
                Console.SetCursorPosition(Position.X, Position.Y);
                Console.Write(' ');
                Position.X += xAxis;
                Position.Y += yAxis;

            }
        }
        public void Control()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.NumPad1:
                    this.Move(-1, 1);
                    break;
                case ConsoleKey.NumPad2:
                    this.Move(0, 1);
                    break;
                case ConsoleKey.NumPad3:
                    this.Move(1, 1);
                    break;
                case ConsoleKey.NumPad4:
                    this.Move(-1, 0);
                    break;
                case ConsoleKey.NumPad5:
                    this.Move(0, 0);
                    break;
                case ConsoleKey.NumPad6:
                    this.Move(1, 0);
                    break;
                case ConsoleKey.NumPad7:
                    this.Move(-1, -1);
                    break;
                case ConsoleKey.NumPad8:
                    this.Move(0, -1);
                    break;
                case ConsoleKey.NumPad9:
                    this.Move(1, -1);
                    break;
                default:
                    break;
            }
        }
        public bool IsAllowedToMove(int xAxis, int yAxis)
        {
            if (this.Position.X + xAxis >= 0 && this.Position.Y + yAxis >= 0 && this.Position.X + xAxis <= Console.WindowWidth - 1 && this.Position.Y + yAxis <= Console.WindowHeight - 1)
                return Program.Grid[Position.X + xAxis, Position.Y + yAxis].AvailableToMove;
            else return false;
        }
        public Node[,] GetNodesAround()
        {
            Node[,] temp = new Node[3, 3];
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) continue;
                    temp[x + 1, y + 1] = Program.Grid[this.Position.X + x, this.Position.Y + y];
                }
            }
            return temp;
        }

        public virtual void Show()
        {
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(this.Appearance);
        }

        private static void LUL() //just for luls
        {

        }
    }

    class Character : Being
    {
        internal string Name { get; set; }

        public Character(int x, int y, char appear = '@') : base(0, 0, appear)
        {
            if (x == -1 && y == -1)
            {
                Random rng = new Random();
                for (int i = 0; i < Program.Grid.GetLength(0) / 3; i++)
                {
                    for (int j = 0; j < Program.Grid.GetLength(1) / 3; j++)
                    {
                        if (Program.Grid[i, j].Appearance == ' ' && rng.Next(100) > 95)
                        {
                            base.Position.X = i;
                            base.Position.Y = j;
                            return;
                        }
                    }
                }
            }
        }

        public override void Show()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            base.Show();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Spawn()
        {
            for (int i = 0; i < Program.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Program.Grid.GetLength(1); j++)
                {
                    if (Program.Grid[i, j].Appearance == ' ')
                    {
                        Console.SetCursorPosition(i, j);
                        this.Show();
                    }
                }
            }
        }
    }

    class Item : Being
    {
        public Item(int x, int y, char appear = '%') : base(x, y, appear)
        {
            if (x == -1 && y == -1)
            {
                Random rng = new Random();
                for (int i = Console.WindowWidth - 2; i > Program.Grid.GetLength(0) / 3; i--)
                {
                    for (int j = Console.WindowHeight - 2; j > Program.Grid.GetLength(1) / 3; j--)
                    {
                        if (Program.Grid[i, j].Appearance == ' ' && rng.Next(100) > 95)
                        {
                            base.Position.X = i;
                            base.Position.Y = j;
                            return;
                        }
                    }
                }
            }
        }

        public override void Show()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            base.Show();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
