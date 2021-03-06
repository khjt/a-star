﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star_pathfinding
{
    class Node
    {
        internal Vector2 Position;
        internal char Appearance;

        #region A-star stuff
        internal Node ParentNode { get; set; }
        internal int G { get; set; }
        internal int H { get; set; }

        internal int F
        {
            get
            {
                return G + H;
            }
            private set { }
        }
        #endregion

        public bool AvailableToMove
        {
            get
            {
                if (this.Appearance == ' ')
                {
                    return true;
                }
                else return false;
            }
            private set { }
        }

        public Node(Vector2 vector)
        {
            Position = vector;
            Appearance = ' ';
        }
        public Node(int x, int y)
        {
            Position = new Vector2(x, y);
            Appearance = ' ';
        }
        public Node(int x, int y, char appearance)
        {
            Position = new Vector2(x, y);
            Appearance = appearance;
        }

        public void Show()
        {
            switch (Appearance)
            {
                case ('#'):
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case ('?'):
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    break;
            }
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(this.Appearance);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    struct Vector2
    {
        internal int X;
        internal int Y;

        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator !=(Vector2 vec1, Vector2 vec2)
        {
            if (vec1.X != vec2.X || vec1.Y != vec2.Y) return true;
            else return false;
        }
        public static bool operator ==(Vector2 vec1, Vector2 vec2)
        {
            if (vec1.X == vec2.X || vec1.Y == vec2.Y) return true;
            else return false;
        }
    }
}
