using Common;
using Common.Collection;
using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data.IO.Interfaces;
using ToysProduction.Data.Interfaces;
using ToysProduction.Entities;

namespace ToysProduction.Data.Formatting {
    public static class FormattingMethods {
        public static string ToDataString(this IBaseDataSet dataSet, string header = null) {
            if (dataSet == null) {
                throw new ArgumentNullException("dataSet");
            }
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(header)) {
                sb.AppendLine(header);
            }
            IDataSet ds = dataSet as IDataSet;
            if (ds == null) {
                return sb.ToString();
            }
            sb.AppendLine("  Producers:");
            sb.AppendLine(ds.Producers.ToLineList("Виробники", "    "));
            sb.AppendLine("  Toys:");
            sb.AppendLine(ds.Toys.ToLineList("Іграшки", "    "));
            return sb.ToString();
        }

        public static string ToStatisticsString(this IDataSet dataSet, string header = null) {
            if (dataSet == null) {
                throw new ArgumentNullException("dataSet");
            }
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(header)) {
                sb.AppendLine(header);
            }
            sb.AppendLine($"  Producers count: {dataSet.Producers.Count}");
            sb.AppendLine($"  Toys count: {dataSet.Toys.Count}");
            return sb.ToString();
        }

        public static string ToTable(this IEnumerable<Producer> objects, string header = null) {
            if (objects == null) {
                throw new ArgumentNullException(nameof(objects));
            }
            if (header == null) {
                header = "Виробники іграшок";
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header);
            string format = "  {0,5} {1,-20} {2,-15} {3,-20}\n";
            sb.AppendFormat(format, "Id", "Назва", "Країна", "Телефон");
            sb.AppendFormat("  {0}\n", new string('-', 62));
            foreach (var obj in objects) {
                sb.AppendFormat(format,
                    obj.Id,
                    obj.Name,
                    obj.Country,
                    obj.Phone ?? "");
            }
            sb.Length--;
            return sb.ToString();
        }

        public static string ToTable(this IEnumerable<Toy> objects, string header = null) {
            if (objects == null) {
                throw new ArgumentNullException(nameof(objects));
            }
            if (header == null) {
                header = "Іграшки";
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(header);
            string format = "  {0,5} {1,-20} {2,-20} {3,10}\n";
            sb.AppendFormat(format, "Id", "Назва", "Виробник", "Ціна");
            sb.AppendFormat("  {0}\n", new string('-', 58));
            foreach (var obj in objects) {
                sb.AppendFormat(format,
                    obj.Id,
                    obj.Name,
                    obj.Producer.Name,
                    obj.Price?.ToString("F2") ?? "");
            }
            sb.Length--;
            return sb.ToString();
        }
    }
}