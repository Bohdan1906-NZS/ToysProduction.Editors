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
    /// Редактор даних виробників.
    /// </summary>
    public class ProducersEditor {
        private readonly SimpleCommandController _commandController;
        private readonly IDataSet _dataSet;
        private readonly ICollection<Producer> _collection;
        private MenuItem[] _menuItems;

        /// <summary>
        /// Подія збереження даних.
        /// </summary>
        public event EventHandler<EventArgs> Saving;

        /// <summary>
        /// Ініціалізує новий екземпляр ProducersEditor.
        /// </summary>
        /// <param name="dataSet">Набір даних.</param>
        public ProducersEditor(IDataSet dataSet) {
            if (dataSet == null) {
                throw new ArgumentNullException(nameof(dataSet));
            }
            _dataSet = dataSet;
            _collection = dataSet.Producers;
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
                new MenuItem("зберегти", Save, true),
                new MenuItem("назад", null),
            };
        }

        /// <summary>
        /// Підготовка екрана.
        /// </summary>
        private void PrepareScreen() {
            Console.Clear();
            Console.WriteLine(_collection.ToTable());
        }

        /// <summary>
        /// Відображає дані як текст.
        /// </summary>
        private void ShowAsText() {
            Console.WriteLine(_collection.ToLineList("Виробники іграшок", "    "));
        }

        /// <summary>
        /// Відображає деталі про виробника.
        /// </summary>
        private void ShowObjectDetails() {
            int id = Common.ConsoleIO.Inputting.InputInt32("Введіть Id запису");
            Producer obj = _collection.FirstOrDefault(e => e.Id == id);
            if (obj != null) {
                Console.WriteLine(obj);
                return;
            }
            Console.WriteLine("В списку немає запису з Id рівним {0}", id);
        }

        /// <summary>
        /// Додає нового виробника.
        /// </summary>
        private void Add() {
            Console.WriteLine();
            Producer obj = new Producer();
            obj.Id = _collection.Any() ? _collection.Max(e => e.Id) + 1 : 1;
            obj.Name = Common.ConsoleIO.Inputting.InputString("Назва");
            if (_collection.Any(e => e.Name == obj.Name)) {
                Console.WriteLine("Виробник з такою назвою вже існує.");
                return;
            }
            obj.Country = Common.ConsoleIO.Inputting.InputString("Країна");
            obj.Address = Common.ConsoleIO.Inputting.InputString("Адреса");
            obj.Phone = Common.ConsoleIO.Inputting.InputString("Телефон");
            obj.Description = Common.ConsoleIO.Inputting.InputText("Опис");
            _collection.Add(obj);
            Console.WriteLine("Виробника додано.");
        }

        /// <summary>
        /// Видаляє виробника.
        /// </summary>
        private void Remove() {
            int id = Common.ConsoleIO.Inputting.InputInt32("Введіть Id запису");
            Producer obj = _collection.FirstOrDefault(e => e.Id == id);
            if (obj == null) {
                Console.WriteLine("В списку немає запису з Id рівним {0}", id);
                return;
            }
            if (_dataSet.Toys.Any(t => t.Producer.Id == id)) {
                Console.WriteLine("Не можна видалити виробника, оскільки на нього посилаються іграшки.");
                return;
            }
            _collection.Remove(obj);
            Console.WriteLine("Виробника видалено.");
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