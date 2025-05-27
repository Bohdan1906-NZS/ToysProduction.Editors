using System;
using Common.ConsoleIO;
using Common.Data.IO;
using Common.Data.IO.Interfaces;
using Common.Interfaces;
using Common.Interfaces.Extensions;
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
            //StudyXmlFileIo();
            //StudyBinaryFileIo();
            //StudyFileTypeSelection();
            StudyDataContextIo();
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

        /// <summary>
        /// Тестує операції введення-виведення у двійковому форматі.
        /// </summary>
        private static void StudyBinaryFileIo() {
            Console.WriteLine(" --- StudyBinaryFileIo ---");
            BinaryFileIoController binaryController = new BinaryFileIoController();
            IDataSet dataSet = new DataSet();
            dataSet.CreateTestingData();
            Console.WriteLine(dataSet.ToDataString("dataSet"));
            string fileName = "ToysProduction";
            binaryController.Save(dataSet, fileName);
            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            dataSet.Clear();
            Console.WriteLine(dataSet.ToDataString("dataSet"));
            binaryController.Load(dataSet, fileName);
            Console.WriteLine(dataSet.ToDataString("dataSet"));
        }

        /// <summary>
        /// Тестує вибір типу файлу та операції введення-виведення.
        /// </summary>
        private static void StudyFileTypeSelection() {
            Console.WriteLine(" --- StudyFileTypeSelection ---");
            BinaryFileIoController binaryController = new BinaryFileIoController();
            XmlFileIoController xmlController = new XmlFileIoController();
            IFileTypeInformer[] fileTypeInformers = new IFileTypeInformer[] {
                binaryController, xmlController
            };
            Console.WriteLine(fileTypeInformers.ToKeyList("fileTypeInformers"));
            Func<IKeyable[], string, IKeyable> keyableSelector = SelectionMethods.SelectKeyable;
            FileTypeSelector fileTypeSelector = new FileTypeSelector(fileTypeInformers, keyableSelector);
            fileTypeSelector.Select();
            IFileTypeInformer selectedInformer = fileTypeSelector.CurrentInformer;
            Console.WriteLine("selectedInformer.Key: " + selectedInformer.Key);
            IFileIoController<IDataSet> fileIoController = selectedInformer as IFileIoController<IDataSet>;
            IDataSet dataSet = new DataSet();
            dataSet.CreateTestingData();
            Console.WriteLine(dataSet.ToDataString("dataSet"));
            string fileName = "ToysProduction";
            fileIoController.Save(dataSet, fileName);
            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            dataSet.Clear();
            Console.WriteLine(dataSet.ToDataString("dataSet"));
            fileIoController.Load(dataSet, fileName);
            Console.WriteLine(dataSet.ToDataString("dataSet"));
        }

        /// <summary>
        /// Тестує операції з контекстом даних.
        /// </summary>
        private static void StudyDataContextIo() {
            Console.WriteLine(" --- StudyDataContextIo ---");
            IFileIoController<IDataSet> fileIoController = new BinaryFileIoController();
            DataContext dataContext = new DataContext(fileIoController);
            Console.WriteLine("dataContext:\n" + dataContext);

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            dataContext.DataChanged += DataContext_DataChanged;
            Console.WriteLine("dataContext.IsEmpty():\t" + dataContext.IsEmpty());
            dataContext.CreateTestingData();
            Console.WriteLine("dataContext.IsEmpty():\t" + dataContext.IsEmpty());

            Console.WriteLine(dataContext.DataSet.ToStatisticsString("StatisticsString")); // Змінено: виклик на DataSet
            DataContext dataContext2 = new DataContext(fileIoController);
            Console.WriteLine("dataContext2.IsEmpty():\t" + dataContext2.IsEmpty());
            dataContext.CopyTo(dataContext2);
            Console.WriteLine("dataContext2.IsEmpty():\t" + dataContext2.IsEmpty());
            dataContext.Clear();
            Console.WriteLine("dataContext.IsEmpty():\t" + dataContext.IsEmpty());
            Console.WriteLine("dataContext2:\n" + dataContext2);

            Console.WriteLine(new string('=', Console.BufferWidth - 1));
            string directoryName = @"..\..\files";
            dataContext2.DirectoryName = directoryName;
            dataContext2.Save();
            dataContext.DirectoryName = directoryName;
            dataContext.Load();
            Console.WriteLine("dataContext.IsEmpty():\t" + dataContext.IsEmpty());
            Console.WriteLine("dataContext:\n" + dataContext);
        }

        private static void DataContext_DataChanged(object sender, EventArgs e) {
            Console.WriteLine("DataContext_DataChanged");
        }
    }
}