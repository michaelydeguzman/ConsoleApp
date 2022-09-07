using FlareExam.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlareExam.Tasks.Interfaces
{
    public interface IRectangleWorker
    {
        List<Rectangle> Rectangles { get; set; }

        void AddRectangle(string name, string[] coordinates);

        Rectangle FindRectangle(string position);

        void RemoveRectangle(Rectangle rectangle);
    }
}
