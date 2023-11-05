using System.Collections.Generic;
using UnityEngine;

namespace ChunkGeneration.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Выбирает случайный элемент из коллекции
        /// </summary>
        public static T GetRandom<T>(this IList<T> collection)
        {
            return collection.GetRandom(0);
        }

        /// <summary>
        /// Выбирает случайный элемент из коллекции с возможностью указать минимальный и максимальный индекс
        /// </summary>
        public static T GetRandom<T>(this IList<T> collection, int min, int max = 0)
        {
            var rng = Random.Range(min, max == 0 ? collection.Count : max);
            return collection[rng];
        }
    }
}