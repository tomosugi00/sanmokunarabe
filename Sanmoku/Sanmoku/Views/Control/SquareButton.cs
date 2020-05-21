using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Sanmoku.Views.Control
{
	/// <summary>
	/// ボード上の座標情報を持つボタン
	/// </summary>
	public class SquareButton : Button
	{
		public int Row { get; }
		public int Columm { get; }

		public SquareButton((int row, int column) square) : base()
		{
			this.Row = square.row;
			this.Columm = square.column;
		}
	}
}
