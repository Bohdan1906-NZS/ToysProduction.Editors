using Common.Interfaces;

namespace Common.Entities {
    public abstract class Entity : IKeyable {
        public int Id { get; set; }

        public abstract string Key { get; }

        public override string ToString() {
            return Key;
        }
    }
}