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
		protected readonly IXmokuModel xmokuModel;

		public BasePlayer(IXmokuModel model)
		{
			this.xmokuModel = model ?? throw new ArgumentNullException();
		}

		/// <summary>
		/// 画面から操作可能か
		/// </summary>
		public abstract bool CanOperate { get; }

		/// <summary>
		/// プレイヤーの操作を実行します
		/// </summary>
		public abstract Task StartAsync();

		/// <summary>
		/// 画面の操作を受け付けます
		/// </summary>
		public abstract void Action((int row, int culumn) square);
	}
}
