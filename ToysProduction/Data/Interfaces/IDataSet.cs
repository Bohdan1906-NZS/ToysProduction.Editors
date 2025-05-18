using System.Collections.Generic;
using ToysProduction.Entities;

namespace ToysProduction.Data.Interfaces {
    public interface IDataSet {
        ICollection<Producer> Producers { get; }
        ICollection<Toy> Toys { get; }
        void Clear();
        void CopyTo(IDataSet dataSet);
        bool IsEmpty();
    }
}