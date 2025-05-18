using System.Collections.Generic;
using System.Linq;

namespace Common.Collection {
    public static class CollectionMethods {
        public static string ToLineList<T>(this IEnumerable<T> objects, string prompt, string indent) {
            if (objects == null) {
                return string.Concat(prompt, ":\n", indent, "No items");
            }
            return string.Concat(
                prompt, ":\n",
                string.Join("\n", objects.Select(o => indent + (o != null ? o.ToString() : "null")))
            );
        }
    }
}