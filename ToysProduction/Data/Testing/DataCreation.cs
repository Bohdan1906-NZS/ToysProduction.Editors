using System.Collections.Generic;
using System.Linq;
using ToysProduction.Data.Interfaces;
using ToysProduction.Entities;

namespace ToysProduction.Data.Testing {
    public static class DataCreation {
        public static bool CreateTestingData(this IDataSet dataSet) {
            if (!dataSet.IsEmpty()) {
                return false;
            }

            var lego = new Producer("LEGO", "Denmark", "Billund", "+45 12345678", "Global toy manufacturer") { Id = 1 };
            var mattel = new Producer("Mattel", "USA", "El Segundo", "+1 98765432", "Maker of Barbie and Hot Wheels") { Id = 2 };

            dataSet.Producers.Add(lego);
            dataSet.Producers.Add(mattel);

            dataSet.Toys.Add(new Toy("LEGO City", lego, 29.99m, "Building Set", "Plastic", "City-themed building set") { Id = 1 });
            dataSet.Toys.Add(new Toy("Barbie Doll", mattel, 19.99m, "Doll", "Plastic", "Fashion doll") { Id = 2 });

            return true;
        }
    }
}