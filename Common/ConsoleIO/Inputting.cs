using System;

namespace Common.ConsoleIO {
    public static class Inputting {
        public static int InputInt32(string prompt, int min, int max) {
            while (true) {
                Console.Write($"{prompt} [{min}..{max}]: ");
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max) {
                    return value;
                }
                Console.WriteLine("Некоректне введення. Спробуйте ще раз.");
            }
        }
    }
}