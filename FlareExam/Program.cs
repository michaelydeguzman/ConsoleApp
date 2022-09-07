using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FlareExam.Models;
using FlareExam.Tasks.Interfaces;
using FlareExam.Workers;

namespace FlareExam
{

    class Program
    {
        static void Main(string[] args)
        {
            MainApplication mainApp = new MainApplication();

            mainApp.Run();

            //var gridWidth = 5; // 5 to 25

            //var newGrid = new Grid();

            //var rectangles = new List<Rectangle>();

            //var rect1 = new Rectangle("Rectangle 1", new string[] { "0,0", "0,1", "0,2" });
            //var rect2 = new Rectangle("Rectangle 2", new string[] { "2,2", "3,2", "2,3", "3,3" });

            //rectangles.Add(rect1);
            //rectangles.Add(rect2);
        }
    }
}
