using System;
using ToysProduction.Data;
using ToysProduction.Data.Formatting;
using ToysProduction.Data.Interfaces;
using ToysProduction.Data.IO;
using ToysProduction.Data.Testing;

namespace ToysProduction.ConsoleEditors.Studying {
    /// <summary>
    /// Клас для тестування операцій введення-виведення файлів.
    /// </summary>
    internal static class FileIoTraining {
        /// <summary>
        /// Запускає тестування операцій введення-виведення.
        /// </summary>
        internal static void Run() {
            Console.WriteLine(" === FileIoTraining ===");
            StudyXmlFileIo();
        }

        /// <summary>
        /// Тестує операції введення-виведення у форматі XML.
        /// </summary>
        private static void StudyXmlFileIo() {
            Console.WriteLine(" --- StudyXmlFileIo ---");
            XmlFileIoController xmlController = new XmlFileIoController();
            IDataSet dataSet = new DataSet();
            dataSet.CreateTestingData();
            Console.WriteLine(dataSet.ToDataString("dataSet"));
            string fileName = "ToysProduction";
            xmlController.Save(dataSet, fileName);
            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            dataSet.Clear();
            Console.WriteLine(dataSet.ToDataString("dataSet"));
            xmlController.Load(dataSet, fileName);
            Console.WriteLine(dataSet.ToDataString("dataSet"));
        }
    }
}