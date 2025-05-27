using Common.Data.Interfaces;
using Common.Data.IO.Interfaces;
using Common.IO; // Додано: простір імен для Saving
using System;
using System.IO;

namespace Common.Data {
    /// <summary>
    /// Базовий узагальнений клас для контексту даних.
    /// </summary>
    /// <typeparam name="TData">Тип набору даних, що реалізує IBaseDataSet.</typeparam>
    public class BaseDataContext<TData> : IBaseDataContext, IDataChangeable
        where TData : IBaseDataSet {
        private string _directoryName = "";
        private IFileIoController<TData> _fileIoController;

        /// <summary>
        /// Подія зміни даних.
        /// </summary>
        public event EventHandler<EventArgs> DataChanged;

        /// <summary>
        /// Набір даних.
        /// </summary>
        public TData DataSet { get; private set; }

        /// <summary>
        /// Ім'я каталогу.
        /// </summary>
        public string DirectoryName {
            get { return _directoryName; }
            set {
                _directoryName = value;
                Saving.CheckAndCreateDirectory(ref _directoryName);
            }
        }

        /// <summary>
        /// Ім'я файлу.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Контролер введення/виведення.
        /// </summary>
        public IFileIoController<TData> FileIoController {
            get { return _fileIoController; }
            set {
                if (value == null) throw new ArgumentNullException("value");
                _fileIoController = value;
            }
        }

        /// <summary>
        /// Повний шлях до файлу.
        /// </summary>
        public string FilePath {
            get {
                if (FileIoController == null) throw new InvalidOperationException("FileIoController is not initialized.");
                return Path.Combine(DirectoryName, FileName + FileIoController.FileExtension);
            }
        }

        /// <summary>
        /// Ініціалізує новий екземпляр контексту даних.
        /// </summary>
        /// <param name="fileIoController">Контролер введення/виведення.</param>
        /// <param name="dataSet">Набір даних.</param>
        /// <param name="directoryName">Ім'я каталогу.</param>
        /// <param name="fileName">Ім'я файлу.</param>
        public BaseDataContext(IFileIoController<TData> fileIoController,
            TData dataSet, string directoryName, string fileName) {
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (fileIoController == null) throw new ArgumentNullException("fileIoController");
            DataSet = dataSet;
            FileIoController = fileIoController;
            DirectoryName = directoryName;
            FileName = fileName;
        }

        /// <summary>
        /// Очищає набір даних.
        /// </summary>
        public void Clear() {
            if (DataSet == null) throw new InvalidOperationException("DataSet is not initialized.");
            DataSet.Clear();
            OnDataChanged();
        }

        /// <summary>
        /// Перевіряє, чи набір даних порожній.
        /// </summary>
        /// <returns>True, якщо набір порожній; інакше false.</returns>
        public bool IsEmpty() {
            if (DataSet == null) throw new InvalidOperationException("DataSet is not initialized.");
            return DataSet.IsEmpty();
        }

        /// <summary>
        /// Зберігає дані у файл.
        /// </summary>
        public void Save() {
            Save(FilePath);
        }

        /// <summary>
        /// Зберігає дані у файл за вказаним шляхом.
        /// </summary>
        /// <param name="filePath">Шлях до файлу.</param>
        public void Save(string filePath) {
            if (DataSet == null || FileIoController == null) throw new InvalidOperationException("DataSet or FileIoController is not initialized.");
            Saving.CheckAndPrepareFilePath(ref filePath);
            FileIoController.Save(DataSet, filePath);
        }

        /// <summary>
        /// Завантажує дані з файлу.
        /// </summary>
        /// <returns>True, якщо завантаження успішне; інакше false.</returns>
        public bool Load() {
            return Load(FilePath);
        }

        /// <summary>
        /// Завантажує дані з файлу за вказаним шляхом.
        /// </summary>
        /// <param name="filePath">Шлях до файлу.</param>
        /// <returns>True, якщо завантаження успішне; інакше false.</returns>
        public bool Load(string filePath) {
            if (DataSet == null || FileIoController == null) throw new InvalidOperationException("DataSet or FileIoController is not initialized.");
            bool result = FileIoController.Load(DataSet, filePath);
            if (result) OnDataChanged();
            return result;
        }

        /// <summary>
        /// Генерує подію DataChanged.
        /// </summary>
        protected void OnDataChanged() {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}