using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star_pathfinding
{
    static class MapGenerator
    {
        public static Node[,] FillTiles()
        {
            Random rng = new Random();
            Node[,] temp = new Node[Console.WindowWidth, Console.WindowHeight];
            int tempRng;

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    tempRng = rng.Next(0, 100);
                    if (tempRng > 70)
                    {
                        temp[i, j] = new Node(i, j, '#');
                    }
                    else
                    {
                        temp[i, j] = new Node(i, j, ' ');
                    }
                }
            }
            return temp;
        }

        public static void SmoothMap()
        {
            for (int i = 0; i < Program.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Program.Grid.GetLength(1); j++)
                {
                    int tileCounter = 0;
                    Node[,] tempForEach = GetNearbyTiles(i, j);
                    foreach (Node tile in tempForEach)
                    {
                        if (tile.Appearance != ' ')
                        {
                            tileCounter++;
                        }
                    }
                    if (tileCounter >= 5) //Fills empty tiles if there are 5 or more "wall" tiles around
                    {
                        if (Program.Grid[i, j].Appearance != '?')
                        {
                            Program.Grid[i, j].Appearance = '#';
                        }
                    }
                    else if (tileCounter <= 2) //Deletes alone standing tiles
                    {
                        if (Program.Grid[i, j].Appearance != '?')
                        {
                            Program.Grid[i, j].Appearance = ' ';
                        }
                    }
                }
            }
        }

        private static Node[,] GetNearbyTiles(int x, int y)
        {
            Node[,] temp = new Node[3, 3];

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (x + i >= 0 && y + j >= 0 && x + i <= Console.WindowWidth - 1 && y + j <= Console.WindowHeight - 1)
                    {
                        try
                        {
                            if (i == 0 & j == 0) temp[1, 1] = new Node(1, 1, ' ');
                            temp[i + 1, j + 1] = Program.Grid[x + i, y + j];
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                    }
                    else
                    {
                        temp[i + 1, j + 1] = new Node(i + 1, j + 1, ' ');
                    }
                }
            }
            return temp;
        }
    }
}
