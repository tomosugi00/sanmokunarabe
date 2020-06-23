using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Player
{
	public class User : BasePlayer
	{
		public User(IXmokuModel model) : base(model) { }

		public override bool CanOperate => true;

		public override async Task StartAsync()
		{
			await Task.Delay(TimeSpan.Zero);
			return;
		}

		public override void Action((int row, int culumn) square)
		{
			this.xmokuModel.SetSquare(square);
		}
	}
}
