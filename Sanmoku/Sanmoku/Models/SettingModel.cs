using Sanmoku.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models
{
	public class SettingModel : ISetting
	{
		#region ゲーム設定(変更不可)

		/// <summary>
		/// ボードサイズの設定最大値
		/// </summary>
		public int MaximumBoardSize { get; } = 10;

		/// <summary>
		/// ボードサイズの設定最小値
		/// </summary>
		public int MinimumBoardSize { get; } = 3;

		/// <summary>
		/// X目(勝利マーク数)の設定最大値
		/// </summary>
		public int MaximumXmoku { get; } = 10;

		/// <summary>
		/// X目(勝利マーク数)の設定最小値
		/// </summary>
		public int MinimumXmoku { get; } = 3;

		/// <summary>
		/// 指定可能なプレイヤータイプ
		/// </summary>
		public static IReadOnlyList<string> PlayerTypeList => ConvertStringListFrom(PlayerTypes);

		/// <summary>
		/// プレイヤータイプ。順番保証。
		/// </summary>
		private static readonly List<PlayerType> PlayerTypes = new List<PlayerType>
		{
			PlayerType.User,
			PlayerType.CPU,
			PlayerType.NetWork
		};

		#endregion

		#region ゲーム設定(変更可)

		public event EventHandler UpdateSettingEventHandler;

		public int Player1 { get; private set; }
		public void SetPlayer1(int index)
		{
			if (index < 0 || index > PlayerTypes.Count)
				throw new ArgumentOutOfRangeException(nameof(index));
			this.Player1 = index;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}

		public int Player2 { get; private set; }
		public void SetPlayer2(int index)
		{
			if (index < 0 || index > PlayerTypes.Count)
				throw new ArgumentOutOfRangeException(nameof(index));
			this.Player2 = index;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}

		public int BoardSize { get; private set; }
		public void SetBoardSize(int size)
		{
			if (size < MinimumBoardSize || size > MaximumBoardSize)
				throw new ArgumentOutOfRangeException(nameof(size));

			this.BoardSize = size < this.Xmoku ? this.Xmoku : size;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}

		public int Xmoku { get; private set; }
		public void SetXmoku(int xmoku)
		{
			if (xmoku < MinimumXmoku || xmoku > MaximumXmoku)
				throw new ArgumentOutOfRangeException(nameof(xmoku));

			this.Xmoku = xmoku > this.BoardSize ? this.BoardSize : xmoku;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}

		#endregion

		public SettingModel()
		{
			this.Player1 = 0;
			this.Player2 = 0;
			this.BoardSize = MinimumBoardSize;
			this.Xmoku =  MinimumXmoku;
		}

		#region サポートメソッド

		private static IReadOnlyList<string> ConvertStringListFrom<T>(IEnumerable<T> objs)
		{
			var list = new List<string>();
			foreach (var obj in objs)
			{
				list.Add(obj.ToString());
			}
			return list;
		}

		#endregion
	}
}
