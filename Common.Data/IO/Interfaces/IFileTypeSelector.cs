namespace Common.Data.IO.Interfaces {
    /// <summary>
    /// Визначає методи для вибору типу файлу.
    /// </summary>
    public interface IFileTypeSelector {
        /// <summary>
        /// Поточний інформатор типу файлу.
        /// </summary>
        IFileTypeInformer CurrentInformer { get; }

        /// <summary>
        /// Виконує вибір типу файлу.
        /// </summary>
        void Select();
    }
}