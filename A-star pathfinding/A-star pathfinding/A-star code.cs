using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace A_star_pathfinding
{
    static class A_star_code
    {
        internal static Node currentPosition;
        private static Node startPoint;
        private static Node endPoint;

        //internal static Node[,] OpenList = new Node[3, 3]; //#1
        internal static List<Node> OpenList = new List<Node>();
        internal static List<Node> ClosedList = new List<Node>();

        public static List<Node> Path = new List<Node>();

        static A_star_code() //temporary thingy (?)
        {
            startPoint = Program.Grid[Program.MainHero.Position.X, Program.MainHero.Position.Y];
            endPoint = Program.Grid[Program.Item.Position.X, Program.Item.Position.Y];
            currentPosition = Program.Grid[Program.MainHero.Position.X, Program.MainHero.Position.Y];
            FillOpenList();
        }

        public static void ComputeThePath()
        {
            while (!OpenList.Contains(endPoint) & OpenList.Count > 0)
            {
                Node nodeWithMinF = null;
                int minimumF = Int32.MaxValue;

                FillOpenList();
                foreach (Node node in OpenList)
                {
                    if (node.F < minimumF)
                    {
                        minimumF = node.F;
                        nodeWithMinF = node;
                    }
                }
                AddToClosedList(nodeWithMinF);
                RemoveFromOpenList(nodeWithMinF);
                Visualize(nodeWithMinF);
                currentPosition = nodeWithMinF;
            }
        }

        private static void FillOpenList()
        {
            Node tempNode = null;
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    //Проверка на отсутствие IndexOutOfRangeException
                    if (currentPosition.Position.X + x < 0 || currentPosition.Position.X + x >= Console.WindowWidth ||
                            currentPosition.Position.Y + y < 0 || currentPosition.Position.Y + y >= Console.WindowHeight) continue;
                    else { tempNode = Program.Grid[currentPosition.Position.X + x, currentPosition.Position.Y + y]; }
                    //Проверка на разрешение тайла для передвижения и то, что он не является текущей позицией. Так надо.
                    if (!tempNode.AvailableToMove || tempNode == A_star_code.currentPosition) continue;
                    //Если открытый и закрытый листы уже содержат выбранный тайл, то не пропускает дальше.
                    if (ClosedList.Contains(tempNode)) continue;
                    //У стартовой точки не должно быть родителя, иначе бесконечная рекурсия в MoveToParent();
                    if (tempNode != startPoint && tempNode.ParentNode == null)
                    {
                        tempNode.ParentNode = A_star_code.currentPosition;
                    }
                    if (!(OpenList.Contains(tempNode)))
                    {
                        OpenList.Add(tempNode);
                        GetG(tempNode);
                        GetH(tempNode);
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        #region LULS
        private static void ClearOpenList()
        {
            OpenList.Clear();
        }
        private static void AddToClosedList(Node targetNode)
        {
            ClosedList.Add(targetNode);
        }
        private static void RemoveFromClosedList(Node targetNode) //just for luls
        {
            ClosedList.Remove(targetNode);
        }
        private static void AddToOpenList(Node targetNode)
        {
            OpenList.Add(targetNode);
        }
        private static void RemoveFromOpenList(Node targetNode)
        {
            OpenList.Remove(targetNode);
        }
        #endregion

        internal static void GetG(Node targetNode)
        {
            if(targetNode.ParentNode != null)
            {
                int localG = Convert.ToInt32(Math.Round(Math.Sqrt(Math.Pow(startPoint.Position.X - targetNode.Position.X, 2) + Math.Pow(startPoint.Position.Y - targetNode.Position.Y, 2)) * 10));
                targetNode.G = localG + targetNode.ParentNode.G;
                //targetNode.G = Convert.ToInt32(Math.Round(Math.Sqrt(Math.Pow(startPoint.Position.X - targetNode.Position.X, 2) + Math.Pow(startPoint.Position.Y - targetNode.Position.Y, 2)) * 10));
            }
            else { return; }
        }

        private static void GetH(Node targetNode)
        {
            int vector = (targetNode.Position.X - endPoint.Position.X) + (targetNode.Position.Y - endPoint.Position.Y);
            targetNode.H = vector < 0 ? -vector : vector;
        }

        internal static void MoveToParent(Node child)
        {
            if (child.Position != Program.MainHero.Position & child.Position != Program.Item.Position)
            {
                Console.SetCursorPosition(child.Position.X, child.Position.Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('X');
            }

            if (child.ParentNode != null)
            {
                Path.Add(child);
                MoveToParent(child.ParentNode);
            }

            if(child.ParentNode == null)
            {
                Path.Reverse();
            }
        }

        private static void CheckForShorterPath()
        {
            if (tempNode.G < currentPosition.G)
            {
                tempNode.ParentNode = currentPosition.ParentNode; //ya ebal eto vse
            }
        }

        private static void Visualize(Node node)
        {
            Console.SetCursorPosition(node.Position.X, node.Position.Y);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write('O');
        }
    }
}
