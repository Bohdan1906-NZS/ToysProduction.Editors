using Common.Data;
using ToysProduction.Data.Interfaces;
using ToysProduction.Data.Testing;
using System;
using System.Collections.Generic;
using Common.Data.Interfaces;
using Common.Data.IO.Interfaces;
using ToysProduction.Data.Formatting;
using ToysProduction.Entities;

namespace ToysProduction.Data {
    /// <summary>
    /// Контекст даних для предметної області ToysProduction.
    /// </summary>
    public class DataContext : BaseDataContext<IDataSet>, IDataContext {
        /// <summary>
        /// Ініціалізує новий екземпляр із контролером.
        /// </summary>
        /// <param name="fileIoController">Контролер введення/виведення.</param>
        public DataContext(IFileIoController<IDataSet> fileIoController)
            : this(fileIoController, new DataSet(), "", "ToysProduction") {
        }

        /// <summary>
        /// Ініціалізує новий екземпляр із параметрами.
        /// </summary>
        /// <param name="fileIoController">Контролер введення/виведення.</param>
        /// <param name="dataSet">Набір даних.</param>
        /// <param name="directoryName">Ім'я каталогу.</param>
        /// <param name="fileName">Ім'я файлу.</param>
        public DataContext(IFileIoController<IDataSet> fileIoController,
            IDataSet dataSet, string directoryName, string fileName)
            : base(fileIoController, dataSet, directoryName, fileName) {
        }

        /// <summary>
        /// Створює тестові дані.
        /// </summary>
        /// <returns>True, якщо створення успішне; інакше false.</returns>
        public bool CreateTestingData() {
            bool result = DataSet.CreateTestingData();
            if (result) OnDataChanged();
            return result;
        }

        /// <summary>
        /// Копіює дані до іншого набору.
        /// </summary>
        /// <param name="dataSet">Цільовий набір даних.</param>
        public void CopyTo(IDataSet dataSet) {
            DataSet.CopyTo(dataSet);
        }

        /// <summary>
        /// Колекція виробників.
        /// </summary>
        public ICollection<Producer> Producers {
            get { return DataSet.Producers; }
        }

        /// <summary>
        /// Колекція іграшок.
        /// </summary>
        public ICollection<Toy> Toys {
            get { return DataSet.Toys; }
        }

        /// <summary>
        /// Повертає текстове представлення контексту.
        /// </summary>
        /// <returns>Текстовий опис даних.</returns>
        public override string ToString() {
            return DataSet.ToDataString("Toys Production Data");
        }
    }
}