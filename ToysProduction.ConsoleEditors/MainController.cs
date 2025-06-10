using System;
using Common.ConsoleUI;
using Common.Data.IO.Interfaces;
using ToysProduction.Data.Formatting;
using ToysProduction.Data.Interfaces;
using ToysProduction.Data.IO;

namespace ToysProduction.ConsoleEditors {
    /// <summary>
    /// Головний контролер консольного редактора.
    /// </summary>
    public class MainController {
        private readonly SimpleCommandController _commandController;
        private readonly IDataContext _dataContext;
        private readonly IDataSet _dataSet;
        private MenuItem[] _menuItems;

        /// <summary>
        /// Подія створення тестових даних.
        /// </summary>
        public event EventHandler<EventArgs> TestingDataCreation;

        /// <summary>
        /// Ініціалізує новий екземпляр MainController.
        /// </summary>
        /// <param name="dataContext">Контекст даних.</param>
        public MainController(IDataContext dataContext) {
            if (dataContext == null) {
                throw new ArgumentNullException(nameof(dataContext));
            }
            _dataContext = dataContext;
            _dataSet = _dataContext;
            IniMenuItems();
            _commandController = new SimpleCommandController(_menuItems, PrepareScreen, PrepareRunning);
        }

        /// <summary>
        /// Запускає редактор.
        /// </summary>
        public void Run() {
            _commandController.Run();
        }

        /// <summary>
        /// Ініціалізує елементи меню.
        /// </summary>
        private void IniMenuItems() {
            _menuItems = new MenuItem[] {
                new MenuItem("створити тестові дані", CreateTestingData),
                new MenuItem("дані як текст", ShowAsText, true),
                new MenuItem("видалити усі дані", Clear),
                new MenuItem("зберегти дані", Save, true),
                new MenuItem("зберегти дані як текст", SaveAsText, true), // Самостійне завдання 14a
                new MenuItem("зберегти дані як...", SaveAs, true), // Самостійне завдання 15a
                new MenuItem("вийти", null),
            };
        }

        /// <summary>
        /// Підготовка екрана перед кожною ітерацією.
        /// </summary>
        private void PrepareScreen() {
            Console.Clear();
            Console.WriteLine("ПО \"Виробництво іграшок\"\n");
            Console.WriteLine(_dataSet.ToStatisticsString());
        }

        /// <summary>
        /// Підготовка перед запуском програми.
        /// </summary>
        private void PrepareRunning() {
            if (_dataContext.Load()) {
                Console.WriteLine("Дані завантажено");
            }
            else {
                Console.WriteLine("Файл з даними відсутній");
            }
            SimpleCommandController.StopToView();
        }

        /// <summary>
        /// Створює тестові дані.
        /// </summary>
        private void CreateTestingData() {
            if (!_dataSet.IsEmpty()) {
                Console.WriteLine("\nТестові дані не створені, оскільки сховище не порожнє.");
                SimpleCommandController.StopToView();
                return;
            }
            if (TestingDataCreation == null) {
                Console.WriteLine("\nОперація не підтримується.");
                SimpleCommandController.StopToView();
            }
            else {
                TestingDataCreation(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Відображає дані як текст.
        /// </summary>
        private void ShowAsText() {
            Console.WriteLine();
            Console.WriteLine(_dataContext);
        }

        /// <summary>
        /// Видаляє всі дані.
        /// </summary>
        private void Clear() {
            _dataContext.Clear();
        }

        /// <summary>
        /// Зберігає дані.
        /// </summary>
        private void Save() {
            _dataContext.Save();
            Console.WriteLine("Дані збережено");
        }

        /// <summary>
        /// Зберігає дані як текст (самостійне завдання 14a).
        /// </summary>
        private void SaveAsText() {
            try {
                Console.Write("\nВведіть шлях до файлу (наприклад, \\files\\output.txt): ");
                string filePath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(filePath)) {
                    Console.WriteLine("Шлях не вказано.");
                    SimpleCommandController.StopToView();
                    return;
                }
                System.IO.File.WriteAllText(filePath, _dataContext.ToString());
                Console.WriteLine($"Дані збережено у {filePath}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
            }
            SimpleCommandController.StopToView();
        }

        /// <summary>
        /// Зберігає дані у вибраному форматі (самостійне завдання 15a).
        /// </summary>
        private void SaveAs() {
            try {
                Console.Write("\nВведіть шлях до файлу (наприклад, \\files\\data): ");
                string filePath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(filePath)) {
                    Console.WriteLine("Шлях не вказано.");
                    SimpleCommandController.StopToView();
                    return;
                }
                Console.WriteLine("Виберіть формат:");
                Console.WriteLine("\t0 - Бінарний");
                Console.WriteLine("\t1 - XML");
                int format = Common.ConsoleIO.Inputting.InputInt32("Введіть номер формату", 0, 1);
                IFileIoController<IDataSet> controller = format == 0
                    ? new BinaryFileIoController()
                    : (IFileIoController<IDataSet>)new XmlFileIoController();
                controller.Save(_dataSet, filePath);
                Console.WriteLine($"Дані збережено у {filePath}{controller.FileExtension}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
            }
            SimpleCommandController.StopToView();
        }
    }
}