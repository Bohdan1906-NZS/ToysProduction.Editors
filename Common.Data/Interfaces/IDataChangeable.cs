using System;

namespace Common.Data.Interfaces {
    /// <summary>
    /// Визначає подію зміни даних.
    /// </summary>
    public interface IDataChangeable {
        /// <summary>
        /// Подія, що виникає при зміні даних.
        /// </summary>
        event EventHandler<EventArgs> DataChanged;
    }
}