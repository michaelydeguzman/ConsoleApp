using FlareExam.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlareExam.Tasks.Interfaces
{
    public interface IGridWorker
    {
        Grid Grid { get; set; }

        void InitializeGrid();
        void SetGridWidth(int width);

        void SetGridHeight(int height);

        bool IsRectangleInsideGrid(Rectangle rectangle);

        void RenderGrid(List<Rectangle> rectangles);
    }
}
