using Common.Data.IO.Interfaces;

namespace Common.Data.Interfaces {
    /// <summary>
    /// Визначає базові методи контексту даних.
    /// </summary>
    public interface IBaseDataContext : IBaseDataSet {
        /// <summary>
        /// Завантажує дані.
        /// </summary>
        /// <returns>True, якщо завантаження успішне; інакше false.</returns>
        bool Load();

        /// <summary>
        /// Зберігає дані.
        /// </summary>
        void Save();
    }
}