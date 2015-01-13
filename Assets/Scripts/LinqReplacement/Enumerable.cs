using System;
using System.Collections;
using System.Collections.Generic;

public static class Enumerable
{
		static class EmptyOf<T> {
			public static readonly T[] Instance = new T [0];
		}

		#region Take

		public static IEnumerable<TSource> Take<TSource> (this IEnumerable<TSource> source, int count)
		{
			Check.Source (source);

			if (count <= 0)
				return EmptyOf<TSource>.Instance;

			return CreateTakeIterator (source, count);
		}

		static IEnumerable<TSource> CreateTakeIterator<TSource> (IEnumerable<TSource> source, int count)
		{
			int counter = 0;
			foreach (TSource element in source) {
				yield return element;

				if (++counter == count)
					yield break;
			}
		}

		#endregion

		#region ToArray

		public static TSource [] ToArray<TSource> (this IEnumerable<TSource> source)
		{
			Check.Source (source);

			TSource[] array;
			var collection = source as ICollection<TSource>;
			if (collection != null) {
				if (collection.Count == 0)
					return EmptyOf<TSource>.Instance;
				
				array = new TSource [collection.Count];
				collection.CopyTo (array, 0);
				return array;
			}

			int pos = 0;
			array = EmptyOf<TSource>.Instance;
			foreach (var element in source) {
				if (pos == array.Length) {
					if (pos == 0)
						array = new TSource [4];
					else
						Array.Resize (ref array, pos * 2);
				}

				array[pos++] = element;
			}

			if (pos != array.Length)
				Array.Resize (ref array, pos);
			
			return array;
		}

	#endregion

	#region Random picking

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        var array = source.ToArray();
        return ShuffleInternal(array, array.Length);
    }

    public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> source, int count)
    {
        var array = source.ToArray();
        return ShuffleInternal(array, Math.Min(count, array.Length)).Take(count);
    }

    private static IEnumerable<T> ShuffleInternal<T>(T[] array, int count)
    {
		var r = new Random();
        for (var n = 0; n < count; n++)
        {
            var k = r.Next(n, array.Length);
            var temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }

        return array;
    }

	#endregion
}
