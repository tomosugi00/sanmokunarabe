using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Player
{
	/// <summary>
	/// 操作するプレイヤーの種類
	/// </summary>
	public enum PlayerType
	{
		/// <summary>
		/// ユーザー
		/// </summary>
		User,
		/// <summary>
		/// CPU
		/// </summary>
		CPU,
		/// <summary>
		/// プレイヤー(通信)
		/// </summary>
		NetWork,
	}
}
