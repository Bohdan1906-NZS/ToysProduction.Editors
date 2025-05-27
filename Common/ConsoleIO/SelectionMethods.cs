using Common.Interfaces;
using System;

namespace Common.ConsoleIO {
    /// <summary>
    /// Методи для вибору об’єктів у консолі.
    /// </summary>
    public static class SelectionMethods {
        /// <summary>
        /// Вибирає ключовий об’єкт із масиву.
        /// </summary>
        /// <param name="objects">Масив ключових об’єктів.</param>
        /// <param name="prompt">Підказка для вибору.</param>
        /// <returns>Вибраний об’єкт або null, якщо вибір скасовано.</returns>
        public static IKeyable SelectKeyable(this IKeyable[] objects, string prompt) {
            Console.WriteLine(" {0}:", prompt);
            string format = "\t{0,2} - {1}";
            Console.WriteLine(format, 0, "відміна вибору");
            for (int i = 0; i < objects.Length; i++) {
                Console.WriteLine("\t{0,2} - {1}", i + 1, objects[i].Key);
            }
            int number = Inputting.InputInt32("\n  Введіть номер команди", 0, objects.Length);
            if (number == 0) 
                return null;
            return objects[number - 1];
        }
    }
}