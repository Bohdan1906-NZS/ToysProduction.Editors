namespace Common.Data.IO.Interfaces {
    /// <summary>
    /// Визначає загальні методи наборів даних.
    /// </summary>
    public interface IBaseDataSet {
        /// <summary>
        /// Очищає набір даних.
        /// </summary>
        void Clear();

        /// <summary>
        /// Перевіряє, чи набір даних порожній.
        /// </summary>
        /// <returns>True, якщо набір порожній; інакше false.</returns>
        bool IsEmpty();
    }
}