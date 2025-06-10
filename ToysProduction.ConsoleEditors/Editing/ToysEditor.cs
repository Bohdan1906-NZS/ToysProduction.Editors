using System;
using System.Collections.Generic;
using System.Linq;
using Common.Collection;
using Common.ConsoleUI;
using ToysProduction.Data.Formatting;
using ToysProduction.Data.Interfaces;
using ToysProduction.Entities;

namespace ToysProduction.ConsoleEditors.Editing {
    /// <summary>
    /// Редактор даних іграшок.
    /// </summary>
    public class ToysEditor {
        private readonly SimpleCommandController _commandController;
        private readonly IDataSet _dataSet;
        private readonly ICollection<Toy> _collection;
        private MenuItem[] _menuItems;
        private Func<ICollection<Toy>, IEnumerable<Toy>> _sortFunction;

        /// <summary>
        /// Подія збереження даних.
        /// </summary>
        public event EventHandler<EventArgs> Saving;

        /// <summary>
        /// Ініціалізує новий екземпляр ToysEditor.
        /// </summary>
        /// <param name="dataSet">Набір даних.</param>
        public ToysEditor(IDataSet dataSet) {
            if (dataSet == null) {
                throw new ArgumentNullException(nameof(dataSet));
            }
            _dataSet = dataSet;
            _collection = dataSet.Toys;
            _sortFunction = toys => toys;
            IniMenuItems();
            _commandController = new SimpleCommandController(_menuItems, PrepareScreen);
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
                new MenuItem("дані як текст", ShowAsText, true),
                new MenuItem("детально про...", ShowObjectDetails, true),
                new MenuItem("додати", Add, true),
                new MenuItem("видалити", Remove, true),
                new MenuItem("сортувати", Sort, true),
                new MenuItem("зберегти", Save, true),
                new MenuItem("назад", null),
            };
        }

        /// <summary>
        /// Підготовка екрана.
        /// </summary>
        private void PrepareScreen() {
            Console.Clear();
            Console.WriteLine(_sortFunction(_collection).ToTable("Іграшки"));
        }

        /// <summary>
        /// Відображає дані як текст.
        /// </summary>
        private void ShowAsText() {
            Console.WriteLine(_sortFunction(_collection).ToLineList("Іграшки", "    "));
        }

        /// <summary>
        /// Відображає деталі про іграшку.
        /// </summary>
        private void ShowObjectDetails() {
            int id = Common.ConsoleIO.Inputting.InputInt32("Введіть Id запису");
            Toy obj = _collection.FirstOrDefault(e => e.Id == id);
            if (obj != null) {
                Console.WriteLine(obj);
                return;
            }
            Console.WriteLine("В списку немає запису з Id рівним {0}", id);
        }

        /// <summary>
        /// Вибирає виробника зі списку.
        /// </summary>
        private Producer SelectProducer() {
            Console.WriteLine(_dataSet.Producers.ToTable("Доступні виробники"));
            int id = Common.ConsoleIO.Inputting.InputInt32("Введіть Id виробника");
            return _dataSet.Producers.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Додає нову іграшку.
        /// </summary>
        private void Add() {
            Console.WriteLine();
            Toy obj = new Toy();
            obj.Id = _collection.Any() ? _collection.Max(e => e.Id) + 1 : 1;
            obj.Name = Common.ConsoleIO.Inputting.InputString("Назва");
            if (_collection.Any(t => t.Name == obj.Name)) {
                Console.WriteLine("Іграшка з такою назвою вже існує.");
                return;
            }
            obj.Producer = SelectProducer();
            if (obj.Producer == null) {
                Console.WriteLine("Виробника не знайдено.");
                return;
            }
            obj.Price = Common.ConsoleIO.Inputting.InputNullableDecimal("Ціна");
            obj.Category = Common.ConsoleIO.Inputting.InputString("Категорія");
            obj.Material = Common.ConsoleIO.Inputting.InputString("Матеріал");
            obj.Description = Common.ConsoleIO.Inputting.InputText("Опис");
            _collection.Add(obj);
            Console.WriteLine("Іграшку додано.");
        }

        /// <summary>
        /// Видаляє іграшку.
        /// </summary>
        private void Remove() {
            int id = Common.ConsoleIO.Inputting.InputInt32("Введіть Id запису");
            Toy obj = _collection.FirstOrDefault(e => e.Id == id);
            if (obj != null) {
                _collection.Remove(obj);
                Console.WriteLine("Іграшку видалено.");
                return;
            }
            Console.WriteLine("В списку немає запису з Id рівним {0}", id);
        }

        /// <summary>
        /// Сортує іграшки.
        /// </summary>
        private void Sort() {
            Console.WriteLine("\nВиберіть критерій сортування:");
            Console.WriteLine("\t0 - За Id");
            Console.WriteLine("\t1 - За назвою");
            Console.WriteLine("\t2 - За ціною");
            int choice = Common.ConsoleIO.Inputting.InputInt32("Введіть номер", 0, 2);
            switch (choice) {
                case 0: _sortFunction = toys => toys.OrderBy(t => t.Id); break;
                case 1: _sortFunction = toys => toys.OrderBy(t => t.Name); break;
                case 2: _sortFunction = toys => toys.OrderBy(t => t.Price ?? decimal.MaxValue); break;
            }
            Console.WriteLine("Сортування застосовано.");
        }

        /// <summary>
        /// Зберігає дані.
        /// </summary>
        private void Save() {
            Saving?.Invoke(this, EventArgs.Empty);
            Console.WriteLine("Дані збережено.");
        }
    }
}