using System;
using System.Collections.Generic;
using System.Text;

namespace FlareExam.Helpers
{
    public static class InputValidationHelper
    {
        public static int GetValidIntegerInput(string message)
        {
            bool isValid;
            int inputValue;

            do
            {
                Console.Write(message);
                isValid = int.TryParse(Console.ReadLine(), out inputValue);

                if (!isValid)
                {
                    Console.WriteLine("ERR: Input must be an integer value.");
                    Console.WriteLine();
                }
            }
            while (!isValid);

            return inputValue;
        }

        public static int GetValidIntegerInput(string message, int min, int max)
        {
            bool isValid;
            bool isWithinRange = false;
            int inputValue;

            do
            {
                Console.Write(message);
                isValid = int.TryParse(Console.ReadLine(), out inputValue);

                if (isValid)
                {
                    isWithinRange = inputValue >= min && inputValue <= max;

                    if (!isWithinRange)
                    {
                        Console.WriteLine($"ERR: Input must be an integer between {min} & {max}.");
                        Console.WriteLine();
                    }
                }
                else
                { 

                    Console.WriteLine("ERR: Input must be an integer value.");
                    Console.WriteLine();
                }
            }
            while (!isValid || !isWithinRange);

            return inputValue;
        }


        public static string GetValidInputStringCoordinate(bool recursive = true)
        {
            bool isValid;
            string inputValue;

            do
            {
                Console.Write("Please input a non-negative coordinate in this format x,y (e.g 1,2). " + (recursive ? "Enter 'ok' to stop. " : string.Empty));
                inputValue = Console.ReadLine();

                if (recursive && inputValue == "ok")
                {
                    isValid = true;
                }
                else
                {
                    isValid = inputValue.Split(',').Length == 2 && int.TryParse(inputValue.Split(',')[0].ToString(), out int x)
                        && int.TryParse(inputValue.Split(',')[1].ToString(), out int y) && x > 0 && y > 0;
                }

                if (!isValid)
                {
                    Console.WriteLine("ERR: Input must be a non-negative coordinate.");
                    Console.WriteLine();
                }
            }
            while (!isValid);

            return inputValue;
        }
    }
}
