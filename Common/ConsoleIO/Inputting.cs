using System;
using System.Text.RegularExpressions;

namespace Common.ConsoleIO {
    public static class Inputting {
        private static string format = "{0,40}: ";

        public static int InputInt32(string prompt) {
            Console.Write(format, prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result)) {
                return result;
            }
            throw new FormatException($"Invalid integer input for {prompt}");
        }

        public static int InputInt32(string prompt, int min, int max) {
            int result = InputInt32(prompt);
            if (result < min || result > max) {
                throw new ArgumentOutOfRangeException($"Value must be between {min} and {max}");
            }
            return result;
        }

        public static string InputString(string prompt) {
            Console.Write(format, prompt);
            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) {
                throw new ArgumentException($"Input for {prompt} cannot be empty");
            }
            return input;
        }

        public static string InputString(string prompt, int minLength, int maxLength) {
            string input = InputString(prompt);
            if (input.Length < minLength || input.Length > maxLength) {
                throw new ArgumentException($"Length must be between {minLength} and {maxLength}");
            }
            return input;
        }

        public static string InputString(string prompt, string pattern) {
            string input = InputString(prompt);
            if (!Regex.IsMatch(input, pattern)) {
                throw new ArgumentException($"Input for {prompt} does not match pattern {pattern}");
            }
            return input;
        }

        public static decimal? InputNullableDecimal(string prompt) {
            Console.Write(format, prompt);
            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) {
                return null;
            }
            if (decimal.TryParse(input, out decimal result)) {
                return result;
            }
            throw new FormatException($"Invalid decimal input for {prompt}");
        }

        public static string InputText(string prompt) {
            Console.WriteLine($"{prompt}: (Enter empty line to finish)");
            string line;
            System.Text.StringBuilder text = new System.Text.StringBuilder();
            while (!string.IsNullOrEmpty(line = Console.ReadLine())) {
                text.AppendLine(line);
            }
            return text.ToString().TrimEnd('\n');
        }
    }
}