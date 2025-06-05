using Common.ConsoleIO;
using System;

namespace Common.ConsoleUI {
    /// <summary>
    /// Керує виконанням команд консольного редактора.
    /// </summary>
    public class SimpleCommandController {
        private MenuItem[] _menuItems;
        private Action _prepareScreen;
        private Action _prepareRunning;

        /// <summary>
        /// Ініціалізує новий екземпляр SimpleCommandController.
        /// </summary>
        /// <param name="menuItems">Масив елементів меню.</param>
        /// <param name="prepareScreen">Операція підготовки екрана.</param>
        /// <param name="prepareRunning">Операція підготовки виконання.</param>
        public SimpleCommandController(MenuItem[] menuItems, Action prepareScreen,
            Action prepareRunning = null) {
            _menuItems = menuItems ?? throw new ArgumentNullException(nameof(menuItems));
            _prepareScreen = prepareScreen ?? throw new ArgumentNullException(nameof(prepareScreen));
            _prepareRunning = prepareRunning;
        }

        /// <summary>
        /// Вибирає елемент меню в діалозі з користувачем.
        /// </summary>
        /// <returns>Вибраний елемент меню.</returns>
        private MenuItem SelectMenuItem() {
            Console.WriteLine("\n Список команд меню:");
            for (int i = 0; i < _menuItems.Length; i++) {
                Console.WriteLine("\t{0,2} - {1}", i, _menuItems[i].CommandName);
            }
            int number = Inputting.InputInt32(
                "\n Введіть номер команди меню", 0, _menuItems.Length - 1);
            return _menuItems[number];
        }

        /// <summary>
        /// Зупиняє виконання до натискання клавіші.
        /// </summary>
        public static void StopToView() {
            Console.WriteLine("\n\tДля продовження натисніть довільну клавішу...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Запускає цикл виконання команд.
        /// </summary>
        public void Run() {
            if (_prepareRunning != null) {
                _prepareRunning();
            }
            while (true) {
                _prepareScreen();
                MenuItem menuItem = SelectMenuItem();
                if (menuItem.Operation == null) {
                    return;
                }
                try {
                    menuItem.Operation();
                    if (menuItem.Stopping) {
                        StopToView();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    StopToView();
                }
            }
        }
    }
}