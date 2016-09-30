using System.Collections.Generic;
using System.Linq;

namespace Tennis.Common.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="IEnumerable{T}"/> instance.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks whether the given items is null or empty.
        /// </summary>
        /// <param name="items">List of items.</param>
        /// <typeparam name="T">Type of item.</typeparam>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                return true;
            }

            return !items.Any();
        }

        /// <summary>
        /// Checks whether there are any items within a list.
        /// </summary>
        /// <param name="items">List of items.</param>
        /// <typeparam name="T">Type of item.</typeparam>
        public static bool IsAny<T>(this IEnumerable<T> items)
        {
            return items != null && items.Any();
        }

        /// <summary>
        /// Gets the index of the item from the list.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="items">List of items.</param>
        /// <param name="item">Item to search.</param>
        /// <param name="comparer"><see cref="IEqualityComparer{T}"/> instance.</param>
        /// <returns>Returns the index of the item from the list. If item is not found, returns <c>-1</c>.</returns>
        /// <remarks>http://stackoverflow.com/questions/1290603/how-to-get-the-index-of-an-element-in-an-ienumerable#1290657</remarks>
        public static int IndexOf<T>(this IEnumerable<T> items, T item, IEqualityComparer<T> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            var found = items.Select((value, index) => new { value, index }).FirstOrDefault(p => comparer.Equals(p.value, item));
            return found?.index ?? -1;
        }
    }
}
