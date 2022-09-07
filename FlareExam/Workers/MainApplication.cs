using FlareExam.Models;
using FlareExam.Tasks.Interfaces;
using FlareExam.Workers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlareExam.Workers
{
    public class MainApplication : IMainApplication
    {
        private IGridWorker _gridWorker;
        private IRectangleWorker _rectangleWorker;

        public MainApplication()
        {
            _gridWorker = new GridWorker();
            _rectangleWorker = new RectangleWorker();
        }

        public void Run()
        {
            int selection = 0;

            do
            {
                DisplayMenu();

                selection = GetOptionSelection("Enter selected option: ");

                switch (selection)
                {
                    case 1:
                        // Create grid
                        CreateGrid();
                        break;
                    case 2:
                        // Add Rectangle
                        AddRectangleToGrid();
                        break;
                    case 3:
                        FindRectangleViaPosition();
                        break;
                    case 4:
                        // Render Grid
                        RenderGrid();
                        break;
                    default:
                        Console.WriteLine("Unrecognized option entered. Please try again.");
                        PressAnyKeyToContinue();
                        break;
                };

                ShowGridDetails();
            }
            while (selection != 5);
        }
        private void DisplayMenu()
        {
            Console.WriteLine("Please choose an option below:");
            Console.WriteLine("1) Create new grid.");

            if (_gridWorker.Grid != null)
            {
                Console.WriteLine("2) Add a rectangle. ");
                Console.WriteLine("3) Find a rectangle based on a given coordinate.");
                Console.WriteLine("4) View a render of current Grid with all existing rectangles.");
            }

            Console.WriteLine("5) Exit");
            Console.WriteLine();
        }

        private int GetOptionSelection(string question)
        {
            Console.Write(question);
            return Convert.ToInt32(Console.ReadLine());
        }

        private void ShowGridDetails()
        {
            if (_gridWorker.Grid != null)
            {
                Console.WriteLine();
                Console.WriteLine("Current grid details:");
                Console.WriteLine($"Width: {_gridWorker.Grid.Width}");
                Console.WriteLine($"Height: {_gridWorker.Grid.Height}");
                Console.WriteLine($"Total # of Rectangles: {_rectangleWorker.Rectangles.Count}");
                Console.WriteLine();
            }
        }

        private void GetGridWidth()
        {
            Console.Write("Please specify new Grid Width: ");
            bool success = int.TryParse(Console.ReadLine(), out int width);

            if (success)
            {
                _gridWorker.SetGridWidth(width);
            }
            else
            {
                Console.WriteLine("You have entered an invalid data type. Please input an integer.");
                GetGridWidth();
            }
        }

        private void GetGridHeight()
        {
            Console.Write("Please specify new Grid Height: ");
            bool success = int.TryParse(Console.ReadLine(), out int height);

            if (success)
            {
                _gridWorker.SetGridHeight(height);
            }
            else
            {
                Console.WriteLine("You have entered an invalid data type. Please input an integer.");
                GetGridWidth();
            }
        }

        private void CreateGrid()
        {
            Console.WriteLine();

            _gridWorker.InitializeGrid();

            GetGridWidth();
            GetGridHeight();
        }

        private void RenderGrid()
        {
            Console.WriteLine();
            _gridWorker.RenderGrid(_rectangleWorker.Rectangles);
        }

        private void AddRectangleToGrid()
        {
            Console.WriteLine();
            Console.Write("Please specify a Rectangle name: ");

            var name = Console.ReadLine();
            var coordinatesList = new List<string>();

            var enterCoordinate = string.Empty;

            do
            {
                enterCoordinate = GetRectangleCoordinate();
                coordinatesList.Add(enterCoordinate);
            }

            while (enterCoordinate != "ok");

            // TODO add validation here
            // 1: Rectangle must be valid rectangle
            // 2: Rectangles must not overlap
            // 3: Rectangles must be inside the grid


            _rectangleWorker.AddRectangle(name, coordinatesList.ToArray());

            Console.WriteLine();
            Console.WriteLine($"Rectangle \"{name}\" added successfully.");

            PressAnyKeyToContinue();
        }

        private void FindRectangleViaPosition()
        {
            Console.WriteLine();
            Console.Write("Enter x position: ");

            int x = int.Parse(Console.ReadLine());

            Console.Write("Enter y position: ");

            int y = int.Parse(Console.ReadLine());

            List<int> position = new List<int> { x, y };

            var rectangle = _rectangleWorker.FindRectangle(position);

            if (rectangle != null)
            {
                Console.WriteLine($"Rectangle \"{rectangle.Name}\" found.");
                Console.WriteLine();
                var rectToRender = new List<Rectangle> { rectangle };
                _gridWorker.RenderGrid(rectToRender);

                Console.WriteLine();
                Console.WriteLine("Do you want to remove the rectangle (Y)?");

                if (Console.ReadLine() == "Y")
                {
                    _rectangleWorker.RemoveRectangle(rectangle);

                    Console.WriteLine($"Rectangle \"{rectangle.Name}\" removed successfully!");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("There is no rectangle with the specified coordinates.");
            }

            PressAnyKeyToContinue();
        }

        private string GetRectangleCoordinate()
        {
            Console.Write("Please enter a coordinate in this format x,y (e.g 1,2). Enter 'ok' to stop.   ");
            return Console.ReadLine();
        }

        private void PressAnyKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
