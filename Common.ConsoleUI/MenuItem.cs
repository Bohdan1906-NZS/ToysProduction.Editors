using System;

namespace Common.ConsoleUI {
    /// <summary>
    /// Описує елемент меню консольного редактора.
    /// </summary>
    public class MenuItem {
        /// <summary>
        /// Назва команди меню.
        /// </summary>
        public string CommandName;

        /// <summary>
        /// Операція, що виконується при виборі команди.
        /// </summary>
        public Action Operation;

        /// <summary>
        /// Ознака затримки після виконання команди.
        /// </summary>
        public bool Stopping;

        /// <summary>
        /// Ініціалізує новий екземпляр MenuItem.
        /// </summary>
        /// <param name="commandName">Назва команди.</param>
        /// <param name="operation">Операція команди.</param>
        /// <param name="stopping">Ознака затримки.</param>
        public MenuItem(string commandName, Action operation, bool stopping = false) {
            this.CommandName = commandName;
            this.Operation = operation;
            this.Stopping = stopping;
        }
    }
}