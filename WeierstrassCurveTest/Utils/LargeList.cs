using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeierstrassCurveTest.Utils
{
    internal static class LargeListExtensions
    {
        public static long Count<T>(this LargeList<T> largeList, Predicate<T> match)
        {
            long count = 0;
            foreach (var chunk in largeList.Chunks)
            {
                foreach (var item in chunk)
                {
                    if (match(item))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }

    internal class LargeList<T>
    {
        private List<List<T>> chunks = new List<List<T>>();
        private const int chunkSize = Int32.MaxValue;
        public List<List<T>> Chunks => chunks;

        public LargeList() { }

        public LargeList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public T this[long index]
        {
            get
            {
                int outerIndex = (int)(index / chunkSize);
                int innerIndex = (int)(index % chunkSize);
                return chunks[outerIndex][innerIndex];
            }
            set
            {
                int outerIndex = (int)(index / chunkSize);
                int innerIndex = (int)(index % chunkSize);
                while (outerIndex >= chunks.Count)
                {
                    chunks.Add(new List<T>());
                }
                if (innerIndex >= chunks[outerIndex].Count)
                {
                    chunks[outerIndex].AddRange(new T[innerIndex + 1 - chunks[outerIndex].Count]);
                }
                chunks[outerIndex][innerIndex] = value;
            }
        }

        public long Count
        {
            get
            {
                long count = 0;
                foreach (var chunk in chunks)
                {
                    count += chunk.Count;
                }
                return count;
            }
        }

        public T Last()
        {
            if (chunks.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            var lastChunk = chunks.Last();
            return lastChunk.Last();
        }

        public long FindIndex(Predicate<T> match)
        {
            long index = 0;
            foreach (var chunk in chunks)
            {
                int localIndex = chunk.FindIndex(match);
                if (localIndex != -1)
                    return index + localIndex;
                index += chunkSize;
            }
            return -1; // Not found
        }

        public T Find(Predicate<T> match)
        {
            foreach (var chunk in chunks)
            {
                T foundItem = chunk.Find(match);
                if (!EqualityComparer<T>.Default.Equals(foundItem, default(T)))
                    return foundItem;
            }
            return default(T); // Not found
        }

        public void Add(T item)
        {
            if (chunks.Count == 0 || chunks[chunks.Count - 1].Count == chunkSize)
            {
                chunks.Add(new List<T>());
            }
            chunks[chunks.Count - 1].Add(item);
        }
    }
}
