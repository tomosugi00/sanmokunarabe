using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Sanmoku.Models;

namespace Sanmoku.ViewModels
{
	public class TitlePageViewModel
	{
		private readonly XmokuModel xmokuModel;

		public int MaximumSize { get { return XmokuModel.MaximumSize; } }
		public int MinimumSize { get { return XmokuModel.MinimumSize; } }
		public int MaximumXmoku { get { return XmokuModel.MaximumXmoku; } }
		public int MinimumXmoku { get { return XmokuModel.MinimumXmoku; } }
		public IEnumerable<string> Player1Types { get { return XmokuModel.Player1Types; } }
		public IEnumerable<string> Player2Types { get { return XmokuModel.Player2Types; } }


		public TitlePageViewModel()
		{
			//このモデルを使いまわす。画面遷移時に渡す。
			this.xmokuModel = new XmokuModel(3, 3);
		}

	}
}
