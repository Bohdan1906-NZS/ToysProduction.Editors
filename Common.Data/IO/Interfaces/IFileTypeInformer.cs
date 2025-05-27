using Common.Interfaces;

namespace Common.Data.IO.Interfaces {
    /// <summary>
    /// Визначає інформацію про тип файлу.
    /// </summary>
    public interface IFileTypeInformer : IKeyable {
        /// <summary>
        /// Опис типу файлу.
        /// </summary>
        string FileTypeCaption { get; }

        /// <summary>
        /// Розширення файлу.
        /// </summary>
        string FileExtension { get; }
    }
}