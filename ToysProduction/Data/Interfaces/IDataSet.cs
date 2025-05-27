using Common.Data.IO.Interfaces;
using System.Collections.Generic;
using ToysProduction.Entities;

namespace ToysProduction.Data.Interfaces {
    /// <summary>
    /// Визначає набір даних для предметної області ToysProduction.
    /// </summary>
    public interface IDataSet : IBaseDataSet {
        //void Clear();
        //bool IsEmpty();
        void CopyTo(IDataSet dataSet);
        ICollection<Producer> Producers { get; }
        ICollection<Toy> Toys { get; }
    }
}