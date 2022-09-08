using FlareExam.Models;
using FlareExam.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlareExam.Workers
{
    public class RectangleWorker : IRectangleWorker
    {
        public List<Rectangle> Rectangles { get; set; }

        public RectangleWorker()
        {
            Rectangles = new List<Rectangle>();
        }

        public void AddRectangle(Rectangle rectangle)
        {
            Rectangles.Add(rectangle);
        }

        public Rectangle FindRectangleViaCoordinate(string position)
        {
            foreach (var rectangle in Rectangles)
            {
                foreach (var coordinate in rectangle.Coordinates)
                {
                    if (coordinate.Contains(position))
                    {
                        return rectangle;
                    }
                }
            }

            return null;
        }

        public void RemoveRectangle(Rectangle rectangle)
        {
            if (Rectangles != null)
            {
                Rectangles.Remove(rectangle);
            }
        }

        public bool IsValidRectangle(Rectangle rectangle)
        {
            if (rectangle.Coordinates.Length == 0)
            {
                return false;
            };

            int minX;
            int minY;
            int maxX;
            int maxY;

            List<int> xcoordinates = new List<int>();
            List<int> ycoordinates = new List<int>();

            for (int i = 0; i < rectangle.Coordinates.Length; i++)
            {
                int x = int.Parse(rectangle.Coordinates[i].Split(',')[0]);
                int y = int.Parse(rectangle.Coordinates[i].Split(',')[1]);

                if (!xcoordinates.Contains(x))
                {
                    xcoordinates.Add(x);
                }
                if (!ycoordinates.Contains(y))
                {
                    ycoordinates.Add(y);
                }

                // must not overlap with any existing rectangle

                if (Rectangles.Any(x => x.Coordinates.Contains(rectangle.Coordinates[i])))
                {
                    return false;
                }
            }

            minX = xcoordinates.Min();
            maxX = xcoordinates.Max();

            minY = ycoordinates.Min();
            maxY = ycoordinates.Max();

            if (minX == maxX || minY == maxY)
            {
                return false;
            }

            // Generate 4 corner points of the rectangle
            var p1 = new Point() { X = minX, Y = minY }; // Top left corner
            var p2 = new Point() { X = maxX, Y = minY }; // Top right corner
            var p3 = new Point() { X = minX, Y = maxY }; // Bottom left corner
            var p4 = new Point() { X = maxY, Y = maxY }; // Bottom right corner

            // There should be no unfilled gaps in the rectangle.
            // Check if the all coordinates needed to form the rectangle formed are part of input.

            var rectCoordinatesFromPoints = new List<string>();

            for (int j = minY; j <= maxY; j++)
            {
                for (int i = minX; i <= maxX; i++)
                {
                    var coordinate = i + "," + j;

                    if (!rectangle.Coordinates.Contains(coordinate))
                    {
                        return false;
                    }

                    rectCoordinatesFromPoints.Add(coordinate);
                }
            }

            return true;
        }
    }
}
