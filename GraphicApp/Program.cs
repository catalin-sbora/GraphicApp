using ConsoleMenuComponent;
using ConsoleMenuComponent.Abstractions;
using System;
using System.Collections.Generic;

namespace GraphicApp
{
    class Program
    {
        private static GraphicAppMenu appMenu = new GraphicAppMenu();
        static void Main(string[] args)
        {
            try
            {
                appMenu.Initialize("./plugins");
                appMenu.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ooops, something went wrong: {e.Message}");
            }

        }
    }
}
