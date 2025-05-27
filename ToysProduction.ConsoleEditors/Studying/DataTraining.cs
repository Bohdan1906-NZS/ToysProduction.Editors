using System;
using System.Linq;
using Common.Interfaces.Extensions;
using ToysProduction.Data;
using ToysProduction.Data.Formatting;
using ToysProduction.Data.Interfaces;
using ToysProduction.Data.Testing;

namespace ToysProduction.ConsoleEditors.Studying {
    internal static class DataTraining {
        internal static void Run() {
            Console.WriteLine(" === DataTraining ===");
            StudyIKeyable();
            StudyDataSet();
        }

        private static void StudyDataSet() {
            Console.WriteLine(" --- StudyDataSet ---");

            DataSet dataSet = new DataSet();
            Console.WriteLine(dataSet.ToDataString("DataString"));

            Console.WriteLine(new string('-', Console.BufferWidth - 1));

            dataSet.CreateTestingData();
            Console.WriteLine("dataSet.Producers.First():\n" +
                             (dataSet.Producers.FirstOrDefault() != null ? dataSet.Producers.FirstOrDefault().ToString() : "No producers"));
            Console.WriteLine("dataSet.Toys.FirstOrDefault():\n" +
                             (dataSet.Toys.FirstOrDefault() != null ? dataSet.Toys.FirstOrDefault().ToString() : "No toys"));

            Console.WriteLine(new string('=', Console.BufferWidth - 1));

            Console.WriteLine(dataSet.ToStatisticsString("StatisticsString"));
            Console.WriteLine("dataSet.IsEmpty():\t" + dataSet.IsEmpty());
            bool result1 = dataSet.CreateTestingData();
            Console.WriteLine("result1:\t" + result1);

            dataSet.Clear();
            Console.WriteLine(dataSet.ToStatisticsString("StatisticsString"));
            Console.WriteLine("dataSet.IsEmpty():\t" + dataSet.IsEmpty());
            bool result2 = dataSet.CreateTestingData();
            Console.WriteLine("result2:\t" + result2);
        }

        private static void StudyIKeyable() {
            Console.WriteLine(" --- StudyIKeyable ---");

            IDataSet dataSet = new DataSet();
            Console.WriteLine(dataSet.Producers.ToKeyList("Producers"));
            dataSet.CreateTestingData();
            Console.WriteLine(dataSet.Producers.ToKeyList("Producers"));
            Console.WriteLine(dataSet.Producers.ToKeyLine("Producers"));
            Console.WriteLine(dataSet.Toys.ToKeyList("Toys"));
            //Console.WriteLine(dataSet.ToKeyList("KeyList:"));
        }
    }
}