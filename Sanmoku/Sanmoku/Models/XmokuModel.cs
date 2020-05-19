using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

using Sanmoku.Models.Category;

namespace Sanmoku.Models
{
	public class XmokuModel
	{
		#region ゲーム設定定数(変更不可)
		/// <summary>
		/// ボードサイズの設定最大値
		/// </summary>
		public static int MaximumSize { get; } = 10;
		/// <summary>
		/// ボードサイズの設定最小値
		/// </summary>
		public static int MinimumSize { get; } = 3;

		/// <summary>
		/// X目(勝利マーク数)の設定最大値
		/// </summary>
		public static int MaximumXmoku { get; } = 10;
		/// <summary>
		/// X目(勝利マーク数)の設定最小値
		/// </summary>
		public static int MinimumXmoku { get; } = 3;

		/// <summary>
		/// プレイヤータイプ。順番保証。
		/// </summary>
		private static readonly List<PlayerType> PlayerTypes = new List<PlayerType>
		{
			PlayerType.Player,
			PlayerType.CPU,
			PlayerType.NetWork
		};
		/// <summary>
		/// プレイヤー1で指定可能なプレイヤータイプ
		/// </summary>
		public static IReadOnlyList<string> Player1Types => ConvertStringListFrom(PlayerTypes);
		/// <summary>
		/// プレイヤー2で指定可能なプレイヤータイプ
		/// </summary>
		public static IReadOnlyList<string> Player2Types => ConvertStringListFrom(PlayerTypes);

		#endregion
		#region ゲーム設定プロパティ(タイトル画面で変更可)
		/// <summary>
		/// プレイヤー1の対応インデックスを返します。
		/// </summary>
		public int Player1 { get; private set; }
		public void SetPlayer1(int index)
		{
			if (index < 0 || index > PlayerTypes.Count)
				throw new ArgumentOutOfRangeException(nameof(index));
			this.Player1 = index;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}
		/// <summary>
		/// プレイヤー2の対応インデックスを返します。
		/// </summary>
		public int Player2 { get; private set; }
		public void SetPlayer2(int index)
		{
			if (index < 0 || index > PlayerTypes.Count)
				throw new ArgumentOutOfRangeException(nameof(index));
			this.Player2 = index;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}
		/// <summary>
		/// ボードサイズを返します。
		/// </summary>
		public int Size { get; private set; }
		public void SetSize(int size)
		{
			if (size < MinimumSize || size > MaximumSize)
				throw new ArgumentOutOfRangeException(nameof(size));

			//サイズはX目より小さい値と取らない
			this.Size = size < this.Xmoku ? this.Xmoku : size;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}
		/// <summary>
		/// X目(勝利マーク数)を返します。
		/// </summary>
		public int Xmoku { get; private set; }
		public void SetXmoku(int xmoku)
		{
			if (xmoku < MinimumXmoku || xmoku > MaximumXmoku)
				throw new ArgumentOutOfRangeException(nameof(xmoku));

			//X目はサイズより大きな値をとらない
			this.Xmoku = xmoku > this.Size ? this.Size : xmoku;
			this.UpdateSettingEventHandler?.Invoke(this, null);
		}
		#endregion
		#region イベントハンドラー(タイトル画面)
		public event EventHandler UpdateSettingEventHandler;
		#endregion



		#region イベントハンドラー(対戦画面)
		public event EventHandler SquareChangedEventHandler;
		public event EventHandler TurnChangedEventHandler;
		public event EventHandler RetryEventHandler;
		public event EventHandler FinishedEventHandler;
		#endregion


		private Board<Mark> _board;
		private Mark _currentTurn;
		private Mark _winner;

		public bool IsFinished { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public XmokuModel()
		{
			//初期値設定(タイトル画面で変更可)
			this.Size = MinimumSize;
			this.Xmoku = MinimumXmoku;
			this._board = new Board<Mark>(MinimumSize, Mark.Empty);
			this._currentTurn = Mark.Maru;
			this.IsFinished = false;
			this._winner = Mark.Empty;
		}

		public string GetCurrentTurn()
		{
			return ConvertStringFrom(this._currentTurn);
		}
		public string GetWinner()
		{
			return ConvertStringFrom(this._winner);
		}

		public string GetAt((int row, int culumn) square)
		{
			return ConvertStringFrom(this._board.GetAt(square));
		}

		public void SetAt((int row, int culumn) square)
		{
			if (this._board.GetAt(square) != Mark.Empty)
			{
				return;
			}
			this._board.SetAt(square, this._currentTurn);
			this.SquareChangedEventHandler?.Invoke(this, null);

			if (this.CheckFinished())
			{
				this.FinishedEventHandler?.Invoke(this, null);
				return;
			}
			this.ChangeTurn();
			return;
		}

		public void Retry()
		{
			this._board = new Board<Mark>(this.Size, Mark.Empty);
			this._currentTurn = Mark.Maru;
			this._winner = Mark.Empty;
			this.RetryEventHandler?.Invoke(this, null);
			return;
		}

		private void ChangeTurn()
		{
			switch (this._currentTurn)
			{
				case Mark.Maru:
					this._currentTurn = Mark.Batsu;
					break;
				case Mark.Batsu:
					this._currentTurn = Mark.Maru;
					break;
				default:
					throw new NotImplementedException();
			}
			this.TurnChangedEventHandler?.Invoke(this, null);
			return;
		}

		private bool CheckFinished()
		{
			if (IsFinishedBy(Mark.Maru))
			{
				this.IsFinished = true;
				this._winner = Mark.Maru;
				return true;
			}
			if (IsFinishedBy(Mark.Batsu))
			{
				this.IsFinished = true;
				this._winner = Mark.Batsu;
				return true;
			}
			if (this.IsDraw())
			{
				this.IsFinished = true;
				this._winner = Mark.Empty;
				return true;
			}
			this.IsFinished = false;
			return false;
		}

		private bool IsFinishedBy(Mark mark)
		{
			if (mark == Mark.Empty)
			{
				return false;
			}
			return CheckVertical(mark) || CheckHorizontal(mark) || CheckDiagonal(mark);
		}


		/// <summary>
		/// 縦が揃ったか
		/// </summary>
		/// <param name="mark"></param>
		/// <returns></returns>
		private bool CheckVertical(Mark mark)
		{
			for (var column = 0; column < this.Size; column++)
			{
				if (this._board.GetAt((0, column)) == mark && this._board.GetAt((1, column)) == mark && this._board.GetAt((2, column)) == mark)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 横が揃ったか
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private bool CheckHorizontal(Mark target)
		{
			for (var row = 0; row < this.Size; row++)
			{
				if (this._board.GetAt((row, 0)) == target && this._board.GetAt((row, 1)) == target && this._board.GetAt((row, 2)) == target)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 斜めが揃ったか
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private bool CheckDiagonal(Mark target)
		{
			if (this._board.GetAt((0, 0)) == target && this._board.GetAt((1, 1)) == target && this._board.GetAt((2, 2)) == target)
			{
				return true;
			}
			else if (this._board.GetAt((2, 0)) == target && this._board.GetAt((1, 1)) == target && this._board.GetAt((0, 2)) == target)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 引き分けか
		/// </summary>
		/// <returns></returns>
		private bool IsDraw()
		{
			for (var row = 0; row < this.Size; row++)
			{
				for (var column = 0; column < this.Size; column++)
				{
					if (this._board.GetAt((row, column)) == Mark.Empty)
					{
						return false;
					}
				}
			}
			return true;
		}

		private static string ConvertStringFrom(Mark state)
		{
			switch (state)
			{
				case Mark.Maru:
					return "○";
				case Mark.Batsu:
					return "×";
				case Mark.Empty:
					return string.Empty;
				default:
					throw new NotImplementedException();
			}
		}

		private static IReadOnlyList<string> ConvertStringListFrom<T>(IEnumerable<T> objs)
		{
			var list = new List<string>();
			foreach (var obj in objs)
			{
				list.Add(obj.ToString());
			}
			return list;
		}
	}
}
