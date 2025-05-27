namespace Common.Data.IO.Interfaces {
    /// <summary>
    /// Визначає методи контролера введення/виведення для набору даних.
    /// </summary>
    /// <typeparam name="TData">Тип набору даних, що успадковує IBaseDataSet.</typeparam>
    public interface IFileIoController<TData> : IFileTypeInformer where TData : IBaseDataSet {
        /// <summary>
        /// Зберігає набір даних у файл.
        /// </summary>
        /// <param name="dataSet">Набір даних.</param>
        /// <param name="filePath">Шлях до файлу.</param>
        void Save(TData dataSet, string filePath);

        /// <summary>
        /// Завантажує набір даних із файлу.
        /// </summary>
        /// <param name="dataSet">Набір даних для заповнення.</param>
        /// <param name="filePath">Шлях до файлу.</param>
        /// <returns>True, якщо завантаження успішне; інакше false.</returns>
        bool Load(TData dataSet, string filePath);
    }
}