using Sanmoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models
{
	/// <summary>
	/// 三目並べ用ボード
	/// </summary>
	public class Board<T>
	{
		private readonly List<List<T>> board;

		public int Size { get; private set; }

		public Board(int size, T state)
		{
			this.Size = size;
			this.board = new List<List<T>>();
			for (var row = 0; row < size; row++)
			{
				var list = new List<T>();
				for (var column = 0; column < size; column++)
				{
					list.Add(state);
				}
				this.board.Add(list);
			}
		}

		/// <summary>
		/// <paramref name="square"/>の位置の<typeparamref name="T"/>を取得します。
		/// </summary>
		/// <param name="square"></param>
		/// <returns></returns>
		public T GetAt((int row, int culumn) square)
		{
			if (square.row < 0 || square.row > this.Size - 1 || square.culumn < 0 || square.culumn > this.Size - 1)
			{
				throw new ArgumentOutOfRangeException(nameof(square));
			}
			return this.board[square.row][square.culumn];
		}

		/// <summary>
		/// <paramref name="square"/>の位置に<paramref name="state"/>をセットします。
		/// </summary>
		/// <param name="square"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public T SetAt((int row, int culumn) square, T state)
		{
			if (square.row < 0 || square.row > this.Size - 1 || square.culumn < 0 || square.culumn > this.Size - 1)
			{
				throw new ArgumentOutOfRangeException(nameof(square));
			}
			this.board[square.row][square.culumn] = state;
			return this.GetAt(square);
		}

		public List<T> GetRow(int row)
		{
			if (row < 0 || row > this.Size - 1)
			{
				throw new ArgumentOutOfRangeException(nameof(row));
			}
			var list = new List<T>();
			for (int i = 0; i < this.Size; i++)
			{
				list.Add(this.board[row][i]);
			}
			return list;
		}

		public List<T> GetCulumn(int column)
		{
			if (column < 0 || column > this.Size - 1)
			{
				throw new ArgumentOutOfRangeException(nameof(column));
			}
			var list = new List<T>();
			for (int i = 0; i < this.Size; i++)
			{
				list.Add(this.board[i][column]);
			}
			return list;
		}
	}
}
