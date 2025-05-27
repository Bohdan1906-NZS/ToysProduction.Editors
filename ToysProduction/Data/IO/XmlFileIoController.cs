using Common.Data.IO;
using Common.Data.IO.Interfaces;
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
    public class XmlFileIoController : BaseFileTypeInformer, IFileIoController<IDataSet> {
        /// <summary>
        /// Ініціалізує новий екземпляр для XML-файлів.
        /// </summary>
        public XmlFileIoController() : base("Файли формату XML", ".xml") { }

        //public string FileExtension { get { return ".xml"; } }

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

        protected virtual void WriteData(IDataSet dataSet, XmlWriter writer) {
            WriteProducers(dataSet.Producers, writer);
            WriteToys(dataSet.Toys, writer);
        }

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

        protected virtual void ReadData(IDataSet dataSet, XmlReader reader) {
            switch (reader.Name) {
                case "Producer":
                    ReadProducer(reader, dataSet.Producers);
                    break;
                case "Toy":
                    ReadToy(reader, dataSet);
                    break;
            }
        }

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