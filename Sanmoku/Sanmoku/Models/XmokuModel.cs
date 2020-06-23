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
		/// <summary>
		/// 設定情報
		/// </summary>
		private readonly ISetting setting;

		private Board<Mark> board;
		private Mark currentTurn;
		private Mark winner;
		private BasePlayer player1;
		private BasePlayer player2;

		public int BoardSize => this.setting.BoardSize;
		public int Player1 => this.setting.Player1;
		public int Player2 => this.setting.Player2;

		public bool IsFinished { get; private set; }
		public bool CanManual { get; set; }
		public bool CanOperate => !this.IsFinished && CanManual;

		public event EventHandler RepaintEventHandler;

		public XmokuModel(ISetting settingModel)
		{
			this.setting = settingModel;

			this.CanManual = false;
			this.IsFinished = false;

			this.board = new Board<Mark>(settingModel.BoardSize, Mark.Empty);
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

		public string GetSquare((int row, int culumn) square)
		{
			return ConvertStringFrom(this.board.GetAt(square));
		}

		public void SetSquare((int row, int culumn) square)
		{
			if (this.board.GetAt(square) != Mark.Empty)
			{
				return;
			}
			this.board.SetAt(square, this.currentTurn);

			if (this.CheckFinished())
			{
				this.RepaintEventHandler?.Invoke(this, null);
				return;
			}
			this.ChangeTurn();
			this.RepaintEventHandler?.Invoke(this, null);
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
		public void GameRetry()
		{
			this.board = new Board<Mark>(this.setting.BoardSize, Mark.Empty);
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
			for (var r = 0; r <= this.setting.BoardSize - this.setting.Xmoku; r++)
			{
				for (var c = 0; c < this.setting.BoardSize; c++)
				{
					var list = new List<Mark>();
					for (var i = r; i < r + this.setting.Xmoku; i++)
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
			for (var r = 0; r < this.setting.BoardSize; r++)
			{
				for (var c = 0; c <= this.setting.BoardSize - this.setting.Xmoku; c++)
				{
					var list = new List<Mark>();
					for (var i = c; i < c + this.setting.Xmoku; i++)
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
			for (var r = 0; r <= this.setting.BoardSize - this.setting.Xmoku; r++)
			{
				for (var c = 0; c <= this.setting.BoardSize - this.setting.Xmoku; c++)
				{
					var list = new List<Mark>();
					for (var i = 0; i < this.setting.Xmoku; i++)
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
			for (var r = this.setting.BoardSize - 1; r >= this.setting.Xmoku - 1; r--)
			{
				for (var c = 0; c <= this.setting.BoardSize - this.setting.Xmoku; c++)
				{
					var list = new List<Mark>();
					for (var i = 0; i < this.setting.Xmoku; i++)
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
			for (var r = 0; r < this.setting.BoardSize; r++)
			{
				for (var c = 0; c < this.setting.BoardSize; c++)
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
		#endregion
	}
}
