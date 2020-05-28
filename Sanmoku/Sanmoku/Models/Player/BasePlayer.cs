using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Player
{
	public abstract class BasePlayer
	{
		protected readonly XmokuModel xmokuModel;

		public BasePlayer(XmokuModel model)
		{
			this.xmokuModel = model ?? throw new ArgumentNullException();
		}

		/// <summary>
		/// プレイヤーの操作を開始する
		/// </summary>
		public abstract Task StartAsync();
	}
}
