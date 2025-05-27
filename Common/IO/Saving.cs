using System.IO;

namespace Common.IO {
    /// <summary>
    /// Методи для роботи з каталогами та файлами.
    /// </summary>
    public static class Saving {
        /// <summary>
        /// Перевіряє та створює каталог.
        /// </summary>
        /// <param name="directoryName">Ім'я каталогу.</param>
        public static void CheckAndCreateDirectory(ref string directoryName) {
            directoryName = (directoryName ?? "").Trim();
            if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName)) {
                Directory.CreateDirectory(directoryName);
            }
        }

        /// <summary>
        /// Перевіряє та готує шлях до файлу.
        /// </summary>
        /// <param name="path">Шлях до файлу.</param>
        /// <returns>True, якщо підготовка успішна; інакше false.</returns>
        public static bool CheckAndPrepareFilePath(ref string path) {
            path = (path ?? "").Trim();
            if (string.IsNullOrEmpty(path)) {
                return false;
            }
            string directoryName = Path.GetDirectoryName(path);
            CheckAndCreateDirectory(ref directoryName);
            return true;
        }
    }
}