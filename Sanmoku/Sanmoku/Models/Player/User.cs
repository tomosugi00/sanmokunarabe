using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Player
{
	public class User : BasePlayer
	{
		public User(XmokuModel model) : base(model) { }

		public override void Action()
		{
			throw new NotImplementedException();
		}


		public override async Task StartAsync()
		{
			//ボタン操作を可能にする
			if(!this.xmokuModel.CanManual)
			{
				this.xmokuModel.CanManual = true;
			}
		}
	}
}
