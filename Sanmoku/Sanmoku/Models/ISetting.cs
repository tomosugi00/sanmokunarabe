using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models
{
	public interface ISetting
	{
		/// <summary>
		/// ボードサイズの設定最大値
		/// </summary>
		int MaximumBoardSize { get; }
		/// <summary>
		/// ボードサイズの設定最小値
		/// </summary>
		int MinimumBoardSize { get; }
		/// <summary>
		/// X目(勝利マーク数)の設定最大値
		/// </summary>
		int MaximumXmoku { get; }
		/// <summary>
		/// X目(勝利マーク数)の設定最小値
		/// </summary>
		int MinimumXmoku { get; }
		/// <summary>
		/// プレイヤー1の種別
		/// </summary>
		int Player1 { get; }
		/// <summary>
		/// プレイヤー2の種別
		/// </summary>
		int Player2 { get; }
		/// <summary>
		/// ボードサイズ
		/// </summary>
		int BoardSize { get; }
		/// <summary>
		/// X目数
		/// </summary>
		int Xmoku { get; }
	}
}
