using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sanmoku.Models.Category;

namespace Sanmoku.Models.Player
{
	public class Cpu : BasePlayer
	{
		public Cpu(XmokuModel model) : base(model) { }

		public override async Task StartAsync()
		{
			//ボタン操作を不可能にする
			if (this.xmokuModel.CanManual)
			{
				this.xmokuModel.CanManual = false;
			}
			//置けるまで無限ループ
			while(true)
			{
				var rondom = new Random((int)DateTime.UtcNow.Ticks);
				var r = rondom.Next(0, this.xmokuModel.Size);
				var c = rondom.Next(0, this.xmokuModel.Size);

				if (this.xmokuModel.GetAt((r, c)) == Mark.Empty.ToString())
				{
					await Task.Delay(TimeSpan.FromSeconds(1));
					this.xmokuModel.SetAt((r, c));
					return;
				}
			}
		}
	}
}
