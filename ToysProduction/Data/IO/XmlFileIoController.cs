using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ToysProduction.Data;
using ToysProduction.Data.Interfaces;
using ToysProduction.Entities;

namespace ToysProduction.Data.IO {
    /// <summary>
    /// Контролер для операцій введення-виведення даних у форматі XML.
    /// </summary>
    public sealed class XmlFileIoController {
        /// <summary>
        /// Розширення файлу для XML.
        /// </summary>
        public string FileExtension { get { return ".xml"; } }

        /// <summary>
        /// Записує дані колекції виробників у потік XML.
        /// </summary>
        /// <param name="collection">Колекція виробників.</param>
        /// <param name="writer">Потік запису XML.</param>
        private void WriteProducers(IEnumerable<Producer> collection, XmlWriter writer) {
            writer.WriteStartElement("ProducersData");
            foreach (var obj in collection) {
                writer.WriteStartElement("Producer");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("Country", obj.Country);
                writer.WriteElementString("Address", obj.Address);
                writer.WriteElementString("Phone", obj.Phone);
                writer.WriteElementString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        /// <summary>
        /// Записує дані колекції іграшок у потік XML.
        /// </summary>
        /// <param name="collection">Колекція іграшок.</param>
        /// <param name="writer">Потік запису XML.</param>
        private void WriteToys(IEnumerable<Toy> collection, XmlWriter writer) {
            writer.WriteStartElement("ToysData");
            foreach (var obj in collection) {
                writer.WriteStartElement("Toy");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("ProducerId", obj.Producer != null ? obj.Producer.Id.ToString() : "0");
                writer.WriteElementString("Price", obj.Price.ToString());
                writer.WriteElementString("Category", obj.Category);
                writer.WriteElementString("Material", obj.Material);
                writer.WriteElementString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        /// <summary>
        /// Записує всі дані набору даних у потік XML.
        /// </summary>
        /// <param name="dataSet">Набір даних.</param>
        /// <param name="writer">Потік запису XML.</param>
        private void WriteData(IDataSet dataSet, XmlWriter writer) {
            WriteProducers(dataSet.Producers, writer);
            WriteToys(dataSet.Toys, writer);
        }

        /// <summary>
        /// Зберігає дані набору даних у XML-файл.
        /// </summary>
        /// <param name="dataSet">Набір даних для збереження.</param>
        /// <param name="filePath">Шлях до файлу.</param>
        public void Save(IDataSet dataSet, string filePath) {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            settings.Indent = true;
            XmlWriter writer = null;
            try {
                writer = XmlWriter.Create(filePath, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("ToysProduction");
                WriteData(dataSet, writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            catch (Exception) {
                throw;
            }
            finally {
                if (writer != null) {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Зчитує дані виробника з потоку XML і додає його до колекції.
        /// </summary>
        /// <param name="reader">Потік читання XML.</param>
        /// <param name="collection">Колекція для додавання виробника.</param>
        private void ReadProducer(XmlReader reader, ICollection<Producer> collection) {
            Producer obj = new Producer();
            reader.ReadStartElement("Producer");
            obj.Id = reader.ReadElementContentAsInt();
            obj.Name = reader.ReadElementContentAsString();
            obj.Country = reader.ReadElementContentAsString();
            obj.Address = reader.ReadElementContentAsString();
            obj.Phone = reader.ReadElementContentAsString();
            obj.Description = reader.ReadElementContentAsString();
            collection.Add(obj);
        }

        /// <summary>
        /// Зчитує дані іграшки з потоку XML і додає її до набору даних.
        /// </summary>
        /// <param name="reader">Потік читання XML.</param>
        /// <param name="dataSet">Набір даних для додавання іграшки.</param>
        private void ReadToy(XmlReader reader, IDataSet dataSet) {
            Toy obj = new Toy();
            reader.ReadStartElement("Toy");
            obj.Id = reader.ReadElementContentAsInt();
            obj.Name = reader.ReadElementContentAsString();
            int producerId = reader.ReadElementContentAsInt();
            obj.Producer = dataSet.Producers.FirstOrDefault(p => p.Id == producerId);
            obj.Price = decimal.Parse(reader.ReadElementContentAsString());
            obj.Category = reader.ReadElementContentAsString();
            obj.Material = reader.ReadElementContentAsString();
            obj.Description = reader.ReadElementContentAsString();
            dataSet.Toys.Add(obj);
        }

        /// <summary>
        /// Зчитує всі дані з потоку XML і додає їх до набору даних.
        /// </summary>
        /// <param name="dataSet">Набір даних для заповнення.</param>
        /// <param name="reader">Потік читання XML.</param>
        private void ReadData(IDataSet dataSet, XmlReader reader) {
            switch (reader.Name) {
                case "Producer":
                    ReadProducer(reader, dataSet.Producers);
                    break;
                case "Toy":
                    ReadToy(reader, dataSet);
                    break;
            }
        }

        /// <summary>
        /// Завантажує дані з XML-файлу в набір даних.
        /// </summary>
        /// <param name="dataSet">Набір даних для заповнення.</param>
        /// <param name="filePath">Шлях до файлу.</param>
        /// <returns>True, якщо файл існує і дані завантажено; false, якщо файл відсутній.</returns>
        public bool Load(IDataSet dataSet, string filePath) {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            if (!File.Exists(filePath)) {
                return false;
            }
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            using (XmlReader reader = XmlReader.Create(filePath, settings)) {
                while (reader.Read()) {
                    if (reader.NodeType == XmlNodeType.Element) {
                        ReadData(dataSet, reader);
                    }
                }
            }
            return true;
        }
    }
}