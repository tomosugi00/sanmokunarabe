using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Player
{
	public class Cpu : BasePlayer
	{
		public Cpu(IXmokuModel model) : base(model) { }

		public override bool CanOperate => false;

		public override async Task StartAsync()
		{
			while(true)
			{
				var rondom = new Random((int)DateTime.UtcNow.Ticks);
				var r = rondom.Next(0, this.xmokuModel.BoardSize);
				var c = rondom.Next(0, this.xmokuModel.BoardSize);

				var mark = this.xmokuModel.GetSquare((r, c));
				if (mark == string.Empty)
				{
					await Task.Delay(TimeSpan.FromMilliseconds(100));
					this.xmokuModel.SetSquare((r, c));
					return;
				}
			}
		}

		public override void Action((int row, int culumn) square)
		{
			return;
		}
	}
}
