using System;
using Common.ConsoleUI;
using ToysProduction.Data;
using ToysProduction.Data.IO;

namespace ToysProduction.ConsoleEditors {
    internal class Program {
        private static MainController _mainController = null;
        private static DataContext _dataContext = null;

        private static void RunProgram() {
            _dataContext = new DataContext(new BinaryFileIoController());
            _dataContext.DirectoryName = @"..\..\files";
            _mainController = new MainController(_dataContext);
            _mainController.TestingDataCreation += MainController_TestingDataCreation;
            _mainController.Run();
        }

        private static void MainController_TestingDataCreation(object sender, EventArgs e) {
            _dataContext.CreateTestingData();
        }

        private static void Main(string[] args) {
            // FileIoTraining.Run();
            RunProgram();
        }
    }
}