using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

using Sanmoku.Models.Category;
using Sanmoku.Models.Util;
using Sanmoku.Models.Player;

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
			//ボードを新規作成
			this.board = new Board<Mark>(this.Size, Mark.Empty);
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
		//public event EventHandler SquareChangedEventHandler;
		//public event EventHandler TurnChangedEventHandler;
		//public event EventHandler FinishedEventHandler;
		public event EventHandler RepaintEventHandler;
		#endregion

		private Board<Mark> board;
		private Mark currentTurn;
		private Mark winner;
		private BasePlayer player1;
		private BasePlayer player2;

		public bool IsFinished { get; private set; }
		public bool CanManual { get; set; }
		public bool CanOperate => !this.IsFinished && CanManual;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public XmokuModel()
		{
			//初期値設定(タイトル画面で変更可)
			this.Size = MinimumSize;
			this.Xmoku = MinimumXmoku;
			this.CanManual = false;
			this.IsFinished = false;

			this.board = new Board<Mark>(MinimumSize, Mark.Empty);
			this.currentTurn = Mark.Maru;
			this.winner = Mark.Empty;
		}

		public string GetCurrentTurn()
		{
			return ConvertStringFrom(this.currentTurn);
		}

		public string GetWinner()
		{
			return ConvertStringFrom(this.winner);
		}

		public string GetAt((int row, int culumn) square)
		{
			return ConvertStringFrom(this.board.GetAt(square));
		}

		public void SetAt((int row, int culumn) square)
		{
			if (this.board.GetAt(square) != Mark.Empty)
			{
				return;
			}
			this.board.SetAt(square, this.currentTurn);
			//this.SquareChangedEventHandler?.Invoke(this, null);

			if (this.CheckFinished())
			{
				//this.FinishedEventHandler?.Invoke(this, null);
				this.RepaintEventHandler?.Invoke(this, null);
				return;
			}
			this.ChangeTurn();
			this.RepaintEventHandler?.Invoke(this,null);
			return;
		}

		/// <summary>
		/// ゲームを開始します
		/// </summary>
		public void GameStart()
		{
			this.player1 = PlayerFactory.GetPlayer1(this);
			this.player2 = PlayerFactory.GetPlayer2(this);
			this.player1.StartAsync();
		}

		/// <summary>
		/// ゲームを対戦開始直後に戻します。
		/// </summary>
		public void RetryGame()
		{
			this.board = new Board<Mark>(this.Size, Mark.Empty);
			this.currentTurn = Mark.Maru;
			this.winner = Mark.Empty;
			this.CanManual = false;
			this.IsFinished = false;
			this.RepaintEventHandler?.Invoke(this, null);

			this.GameStart();
			return;
		}

		/// <summary>
		/// ターンを交代します。
		/// </summary>
		private void ChangeTurn()
		{
			switch (this.currentTurn)
			{
				case Mark.Maru:
					this.currentTurn = Mark.Batsu;
					break;
				case Mark.Batsu:
					this.currentTurn = Mark.Maru;
					break;
				default:
					throw new NotImplementedException();
			}
			//this.TurnChangedEventHandler?.Invoke(this, null);

			switch (this.currentTurn)
			{
				case Mark.Maru:
					this.player1.StartAsync();
					return;
				case Mark.Batsu:
					this.player2.StartAsync();
					return;
				default:
					throw new NotImplementedException();
			}
		}

		/// <summary>
		/// ゲームが終了したか判定します。
		/// </summary>
		/// <returns></returns>
		private bool CheckFinished()
		{
			if (IsFinishedBy(Mark.Maru))
			{
				this.IsFinished = true;
				this.winner = Mark.Maru;
				return true;
			}
			if (IsFinishedBy(Mark.Batsu))
			{
				this.IsFinished = true;
				this.winner = Mark.Batsu;
				return true;
			}
			if (this.CheckDraw())
			{
				this.IsFinished = true;
				this.winner = Mark.Empty;
				return true;
			}
			this.IsFinished = false;
			return false;
		}

		#region 勝利判定
		/// <summary>
		/// <paramref name="mark"/>が勝利したか判定します。
		/// <paramref name="mark"/>に<seealso cref="Mark.Empty"/>を指定した場合はfalseを返します。
		/// </summary>
		/// <param name="mark"></param>
		/// <returns></returns>
		private bool IsFinishedBy(Mark mark)
		{
			if (mark == Mark.Empty)
			{
				return false;
			}
			return CheckVertical(mark) || CheckHorizontal(mark) || CheckLowerRight(mark) || CheckUpperRight(mark);
		}
		/// <summary>
		/// 縦が揃ったか
		/// </summary>
		/// <param name="mark"></param>
		/// <returns></returns>
		private bool CheckVertical(Mark mark)
		{
			for (var r = 0; r <= this.Size - this.Xmoku; r++)
			{
				for (var c = 0; c < this.Size; c++)
				{
					var list = new List<Mark>();
					for (var i = r; i < r + this.Xmoku; i++)
					{
						list.Add(this.board.GetAt((i, c)));
					}
					if (list.ContainsOnly(mark))
					{
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// 横が揃ったか
		/// </summary>
		/// <param name="mark"></param>
		/// <returns></returns>
		private bool CheckHorizontal(Mark mark)
		{
			for (var r = 0; r < this.Size; r++)
			{
				for (var c = 0; c <= this.Size - this.Xmoku; c++)
				{
					var list = new List<Mark>();
					for (var i = c; i < c + this.Xmoku; i++)
					{
						list.Add(this.board.GetAt((r, i)));
					}
					if (list.ContainsOnly(mark))
					{
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// 斜め方向(右下がり)も揃ったか
		/// </summary>
		/// <param name="mark"></param>
		/// <returns></returns>
		private bool CheckLowerRight(Mark mark)
		{
			for (var r = 0; r <= this.Size - this.Xmoku; r++)
			{
				for (var c = 0; c <= this.Size - this.Xmoku; c++)
				{
					var list = new List<Mark>();
					for (var i = 0; i < this.Xmoku; i++)
					{
						list.Add(this.board.GetAt((r + i, c + i)));
					}
					if (list.ContainsOnly(mark))
					{
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// 斜め方向(右上がり)に揃ったか
		/// </summary>
		/// <param name="mark"></param>
		/// <returns></returns>
		private bool CheckUpperRight(Mark mark)
		{
			for (var r = this.Size - 1; r >= this.Xmoku - 1; r--)
			{
				for (var c = 0; c <= this.Size - this.Xmoku; c++)
				{
					var list = new List<Mark>();
					for (var i = 0; i < this.Xmoku; i++)
					{
						list.Add(this.board.GetAt((r - i, c + i)));
					}
					if (list.ContainsOnly(mark))
					{
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// 引き分けか
		/// </summary>
		/// <returns></returns>
		private bool CheckDraw()
		{
			for (var r = 0; r < this.Size; r++)
			{
				for (var c = 0; c < this.Size; c++)
				{
					if (this.board.GetAt((r, c)) == Mark.Empty)
					{
						return false;
					}
				}
			}
			return true;
		}
		#endregion

		#region ViewModel用変換
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
		#endregion
	}
}
