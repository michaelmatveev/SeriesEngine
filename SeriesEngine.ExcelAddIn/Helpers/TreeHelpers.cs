using System;
using System.Collections.Generic;
using System.Linq;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
        public int Level { get; set; }
    }

    public static class TreeHelpers
    {
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(
            this IEnumerable<T> collection,
            Func<T, K> id_selector,
            Func<T, K> parent_id_selector,
            K root_id = default(K),
            int level = 0)
        {
            var comparer = EqualityComparer<K>.Default;
            foreach (var c in collection.Where(c => comparer.Equals(parent_id_selector(c), root_id)))
            {
                yield return new TreeItem<T>
                {
                    Item = c,
                    Level = level,
                    Children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(c), level + 1)
                };
            }
        }

        public static int GetTreeDeep<T>(this IEnumerable<TreeItem<T>> tree)
        {
            return tree.Max(t => t.Level) + 1;
        }

    }
}
