using Common.Collection;
using Common.Interfaces.Extensions;
using ToysProduction.Data.Interfaces;
using System;

namespace ToysProduction.Data.Formatting {
    public static class FormattingMethods {
        public static string ToDataString(this IDataSet dataSet, string header) {
            if (header == null) {
                header = "Toys Production Data";
            }
            return string.Concat(
                header, "\n",
                dataSet.Producers.ToLineList("  Producers", "\t"),
                "\n",
                dataSet.Toys.ToLineList("  Toys", "\t")
            );
        }

        public static string ToStatisticsString(this IDataSet dataSet, string header) {
            if (header == null) {
                header = "Статистичні дані про об'єкти ПО";
            }
            return string.Format(
                "{0}\n" +
                "  Представлено:\n" +
                "  {1,7} виробників\n" +
                "  {2,7} іграшок",
                header,
                dataSet.Producers.Count,
                dataSet.Toys.Count
            );
        }

        public static string ToKeyList(this IDataSet dataSet, string header) {
            if (header == null) {
                header = "Список значень ключових полів об’єктів ПО";
            }
            return string.Concat(
                header, "\n",
                dataSet.Producers.ToKeyList("  Виробники"),
                "\n",
                dataSet.Toys.ToKeyList("  Іграшки")
            );
        }
    }
}