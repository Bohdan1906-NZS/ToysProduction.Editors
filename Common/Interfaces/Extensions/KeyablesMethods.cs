using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;

namespace Common.Interfaces.Extensions {
    public static class KeyablesMethods {
        public static string ToKeyList(this IEnumerable<IKeyable> objects, string prompt) {
            if (objects == null) {
                return prompt + ":\n\tNo items";
            }
            return string.Format("{0}:\n\t{1}", prompt,
                string.Join("\n\t", objects.Select(e => e.Key)));
        }

        public static string ToKeyLine(this IEnumerable<IKeyable> objects, string prompt) {
            if (objects == null) {
                return prompt + ":\tNo items";
            }
            return string.Format("{0}:\t{1}", prompt,
                string.Join(", ", objects.Select(e => e.Key)));
        }
    }
}