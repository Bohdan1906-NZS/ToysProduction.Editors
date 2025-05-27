using Common.Data.IO;
using Common.Data.IO.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ToysProduction.Data.Interfaces;

namespace ToysProduction.Data.IO {
    /// <summary>
    /// Контролер для операцій введення-виведення у двійковому форматі.
    /// </summary>
    public class BinaryFileIoController : BaseFileTypeInformer, IFileIoController<IDataSet> {
        /// <summary>
        /// Ініціалізує новий екземпляр для двійкових файлів.
        /// </summary>
        public BinaryFileIoController() : base("Двійкові файли", ".bin") { }

        /// <summary>
        /// Зберігає набір даних у двійковий файл.
        /// </summary>
        /// <param name="dataSet">Набір даних.</param>
        /// <param name="filePath">Шлях до файлу.</param>
        public void Save(IDataSet dataSet, string filePath) {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fStream = File.OpenWrite(filePath)) {
                formatter.Serialize(fStream, dataSet);
            }
        }

        /// <summary>
        /// Завантажує набір даних із двійкового файлу.
        /// </summary>
        /// <param name="dataSet">Набір даних для заповнення.</param>
        /// <param name="filePath">Шлях до файлу.</param>
        /// <returns>True, якщо завантаження успішне; інакше false.</returns>
        public bool Load(IDataSet dataSet, string filePath) {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            if (!File.Exists(filePath)) {
                return false;
            }
            IDataSet newDataSet = null;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fStream = File.OpenRead(filePath)) {
                newDataSet = (IDataSet)formatter.Deserialize(fStream);
            }
            if (newDataSet == null) {
                return false;
            }
            newDataSet.CopyTo(dataSet);
            return true;
        }
    }
}