namespace SUELIB.CollectionExtensions
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions to collections or enumerables in the <c>System.Collections</c> Namespace.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds an item to a collection if the item does not already exist in the collection.<para/>
        /// Returns <c>true</c> it the object was added successfully.
        /// </summary>
        /// <typeparam name="T">The Type contained in the collection.</typeparam>
        /// <param name="collection">The collection to be modified.</param>
        /// <param name="item">The Object to be added to the collection.</param>
        /// <returns></returns>
        public static bool AddDistinct<T>(this ICollection<T> collection, T item)
        {
            bool added = false;
            if (!collection.Contains(item)) { collection.Add(item); added = true; }
            return added;
        }

        /// <summary>
        /// Adds an object only if the object does not already exist in the collection. <para/>
        /// Returns an <c>int</c> representing the number of items successfully added to the collection.
        /// </summary>
        /// <typeparam name="T">The Type of object contained within the collection.</typeparam>
        /// <param name="collection">The collection to be modified.</param>
        /// <param name="range">The range of Objects to be added to the collection.</param>
        /// <returns></returns>
        public static int AddRangeDistinct<T>(this ICollection<T> collection, ICollection<T> range) where T : class
        {
            if (collection is null || range is null) { return 0; }
            int itemsAdded = 0;
            foreach (T item in range)
            {
                if (!collection.Contains(item)) { collection.Add(item); itemsAdded++; }
            }
            return itemsAdded;
        }

        /// <summary>
        /// Adds a group of objects the collection. <para/>
        /// Returns an <c>int</c> representing the number of items successfully added to the collection.
        /// </summary>
        /// <typeparam name="T">The Type of object contained within the collection.</typeparam>
        /// <param name="collection">The ConcurrentBag&lt;T&gt; to be modified.</param>
        /// <param name="range">The range of objects to be added</param>
        public static int AddRange<T>(this ConcurrentBag<T> collection, IEnumerable<T> range) where T : class
        {
            if (collection is null || range is null) { return 0; }
            int itemsAdded = 0;
            foreach (T item in range)
            {
                collection.Add(item);
                itemsAdded++;
            }
            return itemsAdded;
        }

        /// <summary>
        /// Adds an item to a collection if the item does not already exist in the collection.<para/>
        /// Returns <c>true</c> it the object was added successfully.
        /// </summary>
        /// <typeparam name="T">The Type contained in the collection.</typeparam>
        /// <param name="collection">The ConcurrentBag&lt;T&gt; to be modified.</param>
        /// <param name="item">The Object to be added to the collection.</param>
        /// <returns></returns>
        public static bool AddDistinct<T>(this ConcurrentBag<T> collection, T item)
        {
            bool added = false;
            if (!collection.Contains(item)) { collection.Add(item); added = true; }
            return added;
        }

        /// <summary>
        /// Adds an object only if the object does not already exist in the collection. <para/>
        /// Returns an <c>int</c> representing the number of items successfully added to the collection.
        /// </summary>
        /// <typeparam name="T">The Type of object contained within the collection.</typeparam>
        /// <param name="collection">The ConcurrentBag&lt;T&gt; to be modified.</param>
        /// <param name="range">The range of Objects to be added to the collection.</param>
        /// <returns></returns>
        public static int AddRangeDistinct<T>(this ConcurrentBag<T> collection, IEnumerable<T> range) where T : class
        {
            if (collection is null || range is null) { return 0; }
            int itemsAdded = 0;
            foreach (T item in range)
            {
                if (!collection.Contains(item)) { collection.Add(item); itemsAdded++; }
            }
            return itemsAdded;
        }

        /// <summary>
        /// Merges up to two additional collections together with the current collection and returns a single collection with only one instance of each collection item from any of the collections. Whew!
        /// </summary>
        /// <typeparam name="T">The Type contained in the collection.</typeparam>
        /// <param name="collection">The collection to be modified.</param>
        /// <param name="collectionA"></param>
        /// <param name="collectionB"></param>
        /// <returns></returns>
        public static ICollection<T> Merge<T>(this ICollection<T> collection, ICollection<T> collectionA, ICollection<T> collectionB = null) where T : class
        {
            if (collectionA is null && collectionB is null) { throw new ArgumentNullException("You must provide at least one collection for the merge command."); }
            ICollection<T> tempCol = new List<T>(collection);
            if (collectionA != null) { _ = tempCol.AddRangeDistinct(collectionA); }
            if (collectionB != null) { _ = tempCol.AddRangeDistinct(collectionB); }

            return tempCol;
        }
    }
}