using System;
using Common.ConsoleIO;
using ToysProduction.ConsoleEditors.Studying;

namespace ToysProduction.ConsoleEditors {
    internal class Program {
        static void Main(string[] args) {
            Console.Title = "WorldDividing.ConsoleEditor (Стець А. М.)";

            ConsoleSettings.SetConsoleParam();

            Console.WriteLine(" Реалізація редактора даних ПО \"Поділ Світу\"");

            EntitiesTraining.Run();
            DataTraining.Run();

            Console.ReadKey(true);
        }
    }
}