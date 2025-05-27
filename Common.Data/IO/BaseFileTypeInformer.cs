namespace Common.Data.IO {
    /// <summary>
    /// Базовий клас для інформаторів типу файлу.
    /// </summary>
    public abstract class BaseFileTypeInformer : Interfaces.IFileTypeInformer {
        /// <summary>
        /// Ініціалізує новий екземпляр із заданим описом і розширенням.
        /// </summary>
        /// <param name="fileTypeCaption">Опис типу файлу.</param>
        /// <param name="fileExtension">Розширення файлу.</param>
        protected BaseFileTypeInformer(string fileTypeCaption, string fileExtension) {
            FileTypeCaption = fileTypeCaption;
            FileExtension = fileExtension;
        }

        /// <summary>
        /// Опис типу файлу.
        /// </summary>
        public string FileTypeCaption { get; }

        /// <summary>
        /// Розширення файлу.
        /// </summary>
        public string FileExtension { get; }

        /// <summary>
        /// Ключ, що комбінує опис і розширення.
        /// </summary>
        public string Key {
            get { return string.Format("{0} ({1})", FileTypeCaption, FileExtension); }
        }
    }
}