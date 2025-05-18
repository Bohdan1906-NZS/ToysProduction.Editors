using System;
using System.Text;

namespace Common.ConsoleIO {
    public static class ConsoleSettings {
        public static void SetConsoleParam() { 
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }
    }
}