using FlareExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlareExam.Tasks.Interfaces
{
    public class GridWorker : IGridWorker
    {
        public Grid Grid { get; set; }

        public void InitializeGrid()
        {
            Grid = new Grid();
        }

        public void SetGridWidth(int width)
        {
            Grid.Width = width;
        }

        public void SetGridHeight(int height)
        {
            Grid.Height = height;
        }

        public bool IsRectangleInsideGrid(Rectangle rectangle)
        {
            foreach (var coordinate in rectangle.Coordinates)
            {
                //if (!GetGridCoordinates().Contains(coordinate))
                //{
                //    return false;
                //}

                if (!IsCoordinateInsideBound(int.Parse(coordinate.Split(',')[0]), int.Parse(coordinate.Split(',')[1])))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsCoordinateInsideBound(int x, int y)
        {
            if (x >= Grid.Width)
            {
                return false;
            }
            if (y >= Grid.Height)
            {
                return false;
            }

            return true;
        }

        public void RenderGrid(List<Rectangle> rectangles)
        {
            Console.WriteLine();
            for (int i = 0; i < Grid.Height; i++)
            {
                for (int j = 0; j < Grid.Width; j++)
                {
                    Console.Write("|");

                    bool isRectangleCoordinate = false;

                    foreach (Rectangle rectangle in rectangles)
                    {
                        string coordinate = string.Join(',', new int[] { j, i });

                        if (rectangle.Coordinates.Contains(coordinate))
                        {
                            Console.Write("#");
                            isRectangleCoordinate = true;
                        }
                    }

                    if (!isRectangleCoordinate)
                        Console.Write(" ");

                    if (j == Grid.Width - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
