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
        }
    }
}
