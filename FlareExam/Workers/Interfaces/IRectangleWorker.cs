using FlareExam.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlareExam.Tasks.Interfaces
{
    public interface IRectangleWorker
    {
        List<Rectangle> Rectangles { get; set; }

        Rectangle FindRectangleViaCoordinate(string position);

        void AddRectangle(Rectangle rectangle);

        void RemoveRectangle(Rectangle rectangle);

        bool IsValidRectangle(Rectangle rectangle);
    }
}
