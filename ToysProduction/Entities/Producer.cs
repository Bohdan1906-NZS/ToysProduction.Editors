using Common.Entities;

namespace ToysProduction.Entities {
    public class Producer : Entity {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

        public override string Key { get { return Name; } }

        public override string ToString() { //
            return string.Format(
                "\tВиробник №{0}\n" +
                "\t  Назва: {1}\n" +
                "\t  Країна: {2}\n" +
                "\t  Адреса: {3}\n" +
                "\t  Номер телефона: {4}\n" +
                "\t  Опис: {5}",
                Id,
                Name,
                Country,
                Address,
                Phone,
                Description
            );
        }

        public Producer(string name, string country, string address, string phone, string description) {
            Name = name;
            Country = country;
            Address = address;
            Phone = phone;
            Description = description;
        }

        public Producer() : this(null, null, null, null, null) { }

        public Producer(string name, string country, string address) : this(name, country, address, null, null) { }
    }
}