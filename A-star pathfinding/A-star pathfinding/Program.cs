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
        //public static List<Item> Items = new List<Item>();
        public static Item Item;

        public static Character MainHero;

        static void Main(string[] args)
        {
            #region Creating the map
            Console.CursorVisible = false;
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
            #endregion

            MainHero = new Character(-1, -1);
            //Items.Add(new Item(-1, -1));
            Item = new Item(-1, -1);
            MainHero.Show();
            Item.Show();

            A_star_code.ComputeThePath();
            A_star_code.MoveToParent(Grid[Item.Position.X, Item.Position.Y]);

            foreach(Node node in A_star_code.Path)
            {
                MainHero.Position = node.Position;
                MainHero.Show();
                Thread.Sleep(300);
            }

            Console.ReadKey();
            #region
            //while (true)
            //{
            //    //foreach (Item item in Items)
            //    //{
            //    //    item.Show();
            //    //}
            //    Item.Show();
            //    MainHero.Show();
            //    MainHero.Control();
            //}
            #endregion
        }
    }
}
