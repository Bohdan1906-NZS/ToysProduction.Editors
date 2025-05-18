using System;
using Common.ConsoleIO;
using ToysProduction.ConsoleEditors.Studying;

namespace ToysProduction.ConsoleEditors {
    internal class Program {
        static void Main(string[] args) {
            Console.Title = "ToysProduction.ConsoleEditors (Кошелєв Б. В.)";

            ConsoleSettings.SetConsoleParam();

            Console.WriteLine(" Реалізація редактора даних ПО \"Виробництво іграшок\"");

            EntitiesTraining.Run();

            Console.ReadKey(true);
        }
    }
}