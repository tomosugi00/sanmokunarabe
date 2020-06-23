using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sanmoku.Models.Category;

namespace Sanmoku.Models
{
	public interface IXmokuModel
	{
		int BoardSize { get; }

		string GetSquare((int row, int culumn) square);

		void SetSquare((int row, int culumn) square);
	}
}
