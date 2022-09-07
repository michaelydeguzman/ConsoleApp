using FlareExam.Helpers;
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
            int selection;

            do
            {
                DisplayMenu();

                selection = InputValidationHelper.GetValidIntegerInput("Enter selected option: ");

                switch (selection)
                {
                    case 1:
                        CreateGrid();
                        break;
                    case 2:
                    case 3:
                    case 4:
                        if (IsGridCreated())
                        {
                            switch (selection)
                            {
                                case 2:
                                    AddRectangleToGrid();
                                    break;
                                case 3:
                                    var rect = FindRectangleViaPosition();
                                    ProcessRectangle(rect);
                                    break;
                                case 4:
                                    RenderGrid();
                                    break;
                            }
                        }
                        else
                        {
                            UnrecognizedOption();
                        }
                        break;
                    default:
                        UnrecognizedOption();
                        break;
                };
            }
            while (selection != 5);
        }


        private void DisplayMenu()
        {
            Console.WriteLine("Please choose an option below:");
            Console.WriteLine("1) Create new grid.");

            if (IsGridCreated())
            {
                Console.WriteLine("2) Add a rectangle. ");
                Console.WriteLine("3) Find a rectangle based on a given coordinate.");
                Console.WriteLine("4) View a render of current Grid with all existing rectangles.");
            }

            Console.WriteLine("5) Exit");
            Console.WriteLine();
        }

        private void ShowGridDetails()
        {
            Console.WriteLine();
            Console.WriteLine("Current grid details:");
            Console.WriteLine($"Width: {_gridWorker.Grid.Width}");
            Console.WriteLine($"Height: {_gridWorker.Grid.Height}");
            Console.WriteLine($"Total # of Rectangles: {_rectangleWorker.Rectangles.Count}");
            Console.WriteLine();
        }

        private int GetGridWidth()
        {
            var width = InputValidationHelper.GetValidIntegerInput("Please specify new Grid Width: ", 5, 25);
            _gridWorker.SetGridWidth(width);

            return width;
        }

        private int GetGridHeight()
        {
            var height = InputValidationHelper.GetValidIntegerInput("Please specify new Grid Height: ", 5, 25);
            _gridWorker.SetGridHeight(height);

            return height;
        }

        private void CreateGrid()
        {
            _gridWorker.InitializeGrid();
            var width = GetGridWidth();
            var height = GetGridHeight();

            Console.WriteLine();
            Console.WriteLine($"Grid created with width {width} & height {height}.");
            PressAnyKeyToContinue();
        }

        private void RenderGrid()
        {
            _gridWorker.RenderGrid(_rectangleWorker.Rectangles);
            ShowGridDetails();
        }

        private void AddRectangleToGrid()
        {
            string coordinate;

            Console.WriteLine();
            Console.Write("Please specify a Rectangle name: ");
            var name = Console.ReadLine();

            var coordinatesList = new List<string>();

            do
            {
                coordinate = InputValidationHelper.GetValidInputStringCoordinate();

                if (!coordinatesList.Contains(coordinate))
                {
                    coordinatesList.Add(coordinate);
                }
                else
                {
                    Console.WriteLine("ERR: Cannot have duplicate coordinates.");
                }

            }

            while (coordinate != "ok");

            _rectangleWorker.AddRectangle(name, coordinatesList.ToArray());

            Console.WriteLine();
            Console.WriteLine($"Rectangle \"{name}\" added successfully.");

            PressAnyKeyToContinue();
        }

        private Rectangle FindRectangleViaPosition()
        {
            var position = InputValidationHelper.GetValidInputStringCoordinate(false);

            return _rectangleWorker.FindRectangle(position);
        }

        private void ProcessRectangle(Rectangle rectangle)
        {
            if (rectangle != null)
            {
                Console.WriteLine($"Rectangle \"{rectangle.Name}\" found.");
                Console.WriteLine();
                var rectToRender = new List<Rectangle> { rectangle };
                _gridWorker.RenderGrid(rectToRender);

                Console.WriteLine();
                Console.Write("Do you want to remove the rectangle (Y/N)? ");

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

        private void PressAnyKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        private void UnrecognizedOption()
        {
            Console.WriteLine("ERR: Unrecognized option entered.");
        }

        private bool IsGridCreated()
        {
            return _gridWorker.Grid != null;
        }
    }
}
