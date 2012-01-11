using System;
using System.Collections.Generic;

namespace vsrunnerjs
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			if (items == null)
				throw new ArgumentNullException("items", "collection cannot be null");
			foreach (T obj in items)
				action(obj);
			return items;
		}

	}
}