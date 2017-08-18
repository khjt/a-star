using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace A_star_pathfinding
{
    class Program
    {
        public static Node[,] Grid = new Node[Console.WindowWidth, Console.WindowHeight];

        public static List<Node> PassedTiles = new List<Node>();

        public static Character MainHero;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Grid = MapGenerator.FillTiles();
            for (int i = 0; i < 5; i++)
            {
                MapGenerator.SmoothMap();
            }
            foreach (Node tile in Grid)
            {
                tile.Show();
            }
            Console.SetCursorPosition(0, 0);

            MainHero = new Character(-1, -1);
            //MainHero.Show();

            while (true)
            {
                MainHero.Show();

                MainHero.Control();
            }
        }
    }
}
