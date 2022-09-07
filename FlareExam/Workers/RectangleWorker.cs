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

        public void AddRectangle(string name, string[] coordinates)
        {

            //TODO Validations

            // 1. Must be a valid rectangle
            // 2. Must be within grid
            // 3. Must not overlap with other rectangles

            var newRectangle = new Rectangle()
            {
                Name = name,
                Coordinates = coordinates
            };

            Rectangles.Add(newRectangle);
        }

        public Rectangle FindRectangle(string position)
        {
            foreach(var rectangle in Rectangles)
            {
                foreach(var coordinate in rectangle.Coordinates)
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
    }
}
