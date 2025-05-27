using System;
using Common.Interfaces;
using Common.Data.IO.Interfaces;

namespace Common.Data.IO {
    /// <summary>
    /// Реалізує вибір типу файлу.
    /// </summary>
    public class FileTypeSelector : IFileTypeSelector {
        private readonly IFileTypeInformer[] _fileTypeInformers;

        /// <summary>
        /// Поточний інформатор типу файлу.
        /// </summary>
        public IFileTypeInformer CurrentInformer { get; private set; }

        /// <summary>
        /// Делегат для вибору ключового об’єкта.
        /// </summary>
        public Func<IKeyable[], string, IKeyable> KeyableSelector { get; set; }

        /// <summary>
        /// Ініціалізує новий екземпляр із масивом інформаторів і селектором.
        /// </summary>
        /// <param name="fileTypeInformers">Масив інформаторів типу файлу.</param>
        /// <param name="keySelector">Делегат вибору.</param>
        public FileTypeSelector(IFileTypeInformer[] fileTypeInformers,
            Func<IKeyable[], string, IKeyable> keySelector) {
            _fileTypeInformers = fileTypeInformers;
            CurrentInformer = _fileTypeInformers[0];
            KeyableSelector = keySelector;
        }

        /// <summary>
        /// Виконує вибір типу файлу.
        /// </summary>
        public void Select() {
            IKeyable selectedObj = KeyableSelector(_fileTypeInformers, 
                $"Виберіть тип файлу ({CurrentInformer.Key})");
            if (selectedObj == null)
                return;
            CurrentInformer = selectedObj as IFileTypeInformer;
        }
    }
}