using Common.Data.Interfaces;
using ToysProduction.Data.Interfaces;

namespace ToysProduction.Data.Interfaces {
    /// <summary>
    /// Визначає контекст даних для предметної області ToysProduction.
    /// </summary>
    public interface IDataContext : IBaseDataContext, IDataSet {
    }
}