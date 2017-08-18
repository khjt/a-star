using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star_pathfinding
{
    static class A_star_code
    {
        internal static Node[,] OpenList = new Node[3, 3];

        private static void FillOpenList(Being targetObj)
        {
            if (targetObj is Character)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (Program.Grid[targetObj.Position.X + i, targetObj.Position.Y + j].AvailableToMove)
                        {
                            OpenList[i + 1, j + 1] = Program.Grid[targetObj.Position.X + i, targetObj.Position.Y + j];
                        }
                    }
                }
            }
        }

        private static void AddToPassedTiles(Being targetObj)
        {
            Program.PassedTiles.Add(new Node(targetObj.Position));
        }
    }
}
