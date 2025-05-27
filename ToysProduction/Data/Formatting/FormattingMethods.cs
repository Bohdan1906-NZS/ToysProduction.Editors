using Common.Interfaces.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using ToysProduction.Data.Interfaces;
using ToysProduction.Entities;

namespace ToysProduction.Data.Formatting {
    /// <summary>
    /// Методи розширення для форматування даних.
    /// </summary>
    public static class FormattingMethods {
        /// <summary>
        /// Повертає текстове представлення набору даних.
        /// </summary>
        /// <param name="dataSet">Набір даних.</param>
        /// <param name="title">Заголовок.</param>
        /// <returns>Текстовий опис.</returns>
        public static string ToDataString(this IDataSet dataSet, string title = "") {
            List<string> lines = new List<string>();
            if (!string.IsNullOrEmpty(title)) {
                lines.Add(title);
            }
            lines.Add("  Producers:");
            if (dataSet.Producers.Any()) {
                lines.AddRange(dataSet.Producers.Select(p => "        " + p.ToString()));
            }
            else {
                lines.Add("        No items");
            }
            lines.Add("  Toys:");
            if (dataSet.Toys.Any()) {
                lines.AddRange(dataSet.Toys.Select(t => "        " + t.ToString()));
            }
            else {
                lines.Add("        No items");
            }
            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// Повертає статистичне представлення набору даних.
        /// </summary>
        /// <param name="dataSet">Набір даних.</param>
        /// <param name="title">Заголовок.</param>
        /// <returns>Статистичний опис.</returns>
        public static string ToStatisticsString(this IDataSet dataSet, string title = "") {
            List<string> lines = new List<string>();
            if (!string.IsNullOrEmpty(title)) {
                lines.Add(title);
            }
            lines.Add($"  Producers count: {dataSet.Producers.Count}");
            lines.Add($"  Toys count: {dataSet.Toys.Count}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}