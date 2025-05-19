using System.Collections.Generic;
using ToysProduction.Data.Interfaces;
using ToysProduction.Entities;
using ToysProduction.Data.Formatting;

namespace ToysProduction.Data {
    public class DataSet : IDataSet {
        protected readonly List<Producer> _producers;
        protected readonly List<Toy> _toys;

        public DataSet(List<Producer> producers, List<Toy> toys) {
            _producers = producers;
            _toys = toys;
        }

        public DataSet() : this(new List<Producer>(), new List<Toy>()) { }

        public DataSet(IEnumerable<Producer> producers, IEnumerable<Toy> toys) {
            _producers = new List<Producer>(producers);
            _toys = new List<Toy>(toys);
        }

        public DataSet(IDataSet dataSet) : this(dataSet.Producers, dataSet.Toys) { }

        public virtual ICollection<Producer> Producers {
            get { return _producers; }
        }

        public virtual ICollection<Toy> Toys {
            get { return _toys; }
        }

        public virtual void Clear() {
            _producers.Clear();
            _toys.Clear();
        }

        public virtual void CopyTo(IDataSet dataSet) {
            foreach (var p in _producers) {
                dataSet.Producers.Add(p);
            }
            foreach (var t in _toys) {
                dataSet.Toys.Add(t);
            }
        }

        public virtual bool IsEmpty() {
            return _producers.Count == 0 && _toys.Count == 0;
        }

        public override string ToString() {
            return this.ToDataString("Toys Production Data");
        }
    }
}