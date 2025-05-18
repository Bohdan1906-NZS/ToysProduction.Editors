using Common.Entities;

namespace ToysProduction.Entities {
    public class Toy : Entity {

        //public int Id { get; set; }
        
        public string Name { get; set; }
        public string Category { get; set; }
        public string Material { get; set; }
        public decimal? Price { get; set; }
        public Producer Producer { get; set; }
        public string Description { get; set; }

        public override string Key { get { return Name; } }

        public Toy(string name, Producer producer, decimal? price,
                    string category, string material, string description) {
            Name = name;
            Category = category;
            Material = material;
            Price = price;
            Producer = producer;
            Description = description;
        }

        public Toy(string name, Producer producer, decimal? price, string category, string material)
            : this(name, producer,price, category, material, null) { }

        public Toy() : this(null, null, null, null, null, null) { }

        public override string ToString() {
            return string.Format(
                "\tІграшка №{0}\n" +
                "\t  Назва: {1}\n" +
                "\t  Виробник: {2}\n" +
                "\t  Категорія: {3}\n" +
                "\t  Матеріал: {4}\n" +
                "\t  Ціна: {5}\n" +
                "\t  Опис: {6}",
                Id,
                Name,
                Producer?.Key,
                Category,
                Material,
                Price,
                Description
            );
        }
    }
}