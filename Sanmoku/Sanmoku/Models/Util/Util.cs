using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Util
{
	public static class Util
	{
		/// <summary>
		/// 要素<paramref name="t"/>のみを含んでいるか判定します。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ts"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static bool ContainsOnly<T>(this IEnumerable<T> ts, T t)
		{
			if (ts == null)
				throw new ArgumentNullException(nameof(ts));
			var part = ts.Distinct();
			return part.Count() == 1 && EqualityComparer<T>.Default.Equals(part.First(), t);
		}
	}
}
