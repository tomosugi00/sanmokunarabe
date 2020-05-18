using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Category
{
	public enum Mark
	{
		/// <summary>
		/// カラの状態
		/// </summary>
		Empty,

		/// <summary>
		/// "○"の状態
		/// </summary>
		Maru,

		/// <summary>
		/// "×"の状態
		/// </summary>
		Batsu,
	}
}
