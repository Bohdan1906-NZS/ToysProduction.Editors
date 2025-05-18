using System;
using System.Collections.Generic;
using Common.Entities;
using ToysProduction.Entities;

namespace ToysProducer.ConsoleEditors.Studying {
    internal static class EntitiesTraining {
        internal static void Run() {
            Console.WriteLine(" === EntitiesTraining ===");

            StudyToys();
            StudyEntities();
        }


        private static void StudyToys() {
            Console.WriteLine(" --- StudyProducers ---"); 
            
            Producer obj1;
            obj1 = new Producer();
            Console.WriteLine("obj1.Key: {0}", obj1.Key);
            Console.WriteLine("obj1.ToString:\n{0}", obj1.ToString());
            obj1.Id = 1;
            obj1.Name = "Український завод іграшок";
            obj1.Country = "Україна";
            obj1.Address = "Вінниця, Келецька 122А";
            obj1.Phone = "3805012345";
            obj1.Description = "Професійний завод іграшок";
            Console.WriteLine("obj1.Key: {0}", obj1.Key);
            Console.WriteLine("obj1.ToString:\n{0}", obj1.ToString());

            Console.WriteLine(new string('-', Console.BufferWidth - 1));
            
            Console.WriteLine("obj1:\n{0}", obj1);

            Console.WriteLine(new string('=', Console.BufferWidth - 1));
            
            Producer obj2 = new Producer("Український завод іграшок2", "Україна", "Вінниця, Келецька 152А", "3809812345", "Професійний завод іграшок2") { Id = 2 };
            Console.WriteLine("obj2:\n{0}", obj2);

            Producer obj3 = new Producer("Український завод іграшок3", "Україна", "Вінниця, Келецька 52А", "3809811245", "Професійний завод іграшок3") { Id = 3 };
            Console.WriteLine("obj3:\n{0}", obj3);
            
        }

        static void StudyEntities() {
            Console.WriteLine(" --- StudyEntities ---");

            Producer obj1 = new Producer("Український завод іграшок", "Україна", "Вінниця, Келецька 122А", "3805012345", null) { Id = 1 };
            Console.WriteLine("obj1.ToString():\n{0}", obj1.ToString());
            Console.WriteLine("obj1:\n{0}", obj1);
            Console.WriteLine("obj1.Key: {0}", obj1.Key);

            Entity ent = obj1;
            Console.WriteLine("ent.Id: {0}", ent.Id);
            Object obj = obj1;
            Console.WriteLine("((Entity)obj).Id: {0}", ((Entity)obj).Id);

            Console.WriteLine(new string('-', Console.BufferWidth - 1));

            Console.WriteLine("obj1.Key: {0}", obj1.Key);
            Console.WriteLine("ent.Key: {0}", ent.Key);

            Console.WriteLine("obj1.ToString():\n{0}", obj1.ToString());
            Console.WriteLine("obj1:\n{0}", obj1);
            Console.WriteLine("ent.ToString:\n{0}", ent.ToString());
            Console.WriteLine("ent:\n{0}", ent);
            Console.WriteLine("obj:\n{0}", obj);

            Console.WriteLine(new string('=', Console.BufferWidth - 1));

            Producer obj2 = new Producer("Український завод іграшок2", "Україна", "Вінниця, Келецька 152А") { Id = 2 };
            Producer obj3 = new Producer("Український завод іграшок3", "Україна", "Вінниця, Келецька 52А") { Id = 3 };
            Toy obj4 = new Toy("Машинка на пульті", obj2, 1000, "Машинки","Пластик", null) { Id = 1 };

            List<Entity> entities = new List<Entity>() {
                obj1, obj2, obj3, obj4, new Toy("Лялька з капелюхом", obj3, 1100, "Ляльки","Пластик") { Id = 2 },
            };

            Console.WriteLine("entities:");
            foreach (Entity el in entities) {
                Console.WriteLine("\t{0}", el.Key);
            }

            ent = entities[entities.Count - 1];
            Console.WriteLine("ent:\n{0}", ent);

            List<object> objects = new List<object>() {
                obj1, obj2, obj3, obj4, ent,
            };

            Console.WriteLine("objects:");
            foreach (var el in objects) {
                Console.WriteLine("{0}", el);
            }


            
        }

    }
}
