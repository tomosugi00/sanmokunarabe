using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace Sanmoku.Models
{
	public class XmokuModel
	{
		private readonly int BoardSize;
		private readonly int MokuNumber;

		private Board<Mark> board;
		private Mark currentTurn;
		private Mark winner;

		public event EventHandler SquareChangedEventHandler;
		public event EventHandler TurnChangedEventHandler;
		public event EventHandler RetryEventHandler;
		public event EventHandler FinishedEventHandler;

		public bool IsFinished { get; private set; }

		public XmokuModel(int size, int moku)
		{
			if (size < moku)
				throw new ArgumentException();
			this.BoardSize = size;
			this.MokuNumber = moku;

			this.board = new Board<Mark>(size, Mark.Empty);
			this.currentTurn = Mark.Maru;
			this.IsFinished = false;
			this.winner = Mark.Empty;
		}

		public string GetCurrentTurn()
		{
			return ConvertFrom(this.currentTurn);
		}
		public string GetWinner()
		{
			return ConvertFrom(this.winner);
		}

		public string GetAt((int row, int culumn) square)
		{
			return ConvertFrom(this.board.GetAt(square));
		}

		public void SetAt((int row, int culumn) square)
		{
			if(this.board.GetAt(square)!=Mark.Empty)
			{
				return;
			}
			this.board.SetAt(square, this.currentTurn);
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
			this.board = new Board<Mark>(this.BoardSize, Mark.Empty);
			this.currentTurn = Mark.Maru;
			this.winner = Mark.Empty;
			this.RetryEventHandler?.Invoke(this, null);
			return;
		}

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
			this.TurnChangedEventHandler?.Invoke(this, null);
			return;
		}

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
			if (this.IsDraw())
			{
				this.IsFinished = true;
				this.winner = Mark.Empty;
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
			for (var column = 0; column < this.BoardSize; column++)
			{
				if (this.board.GetAt((0, column)) == mark && this.board.GetAt((1, column)) == mark && this.board.GetAt((2, column)) == mark)
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
			for (var row = 0; row < this.BoardSize; row++)
			{
				if (this.board.GetAt((row, 0)) == target && this.board.GetAt((row, 1)) == target && this.board.GetAt((row, 2)) == target)
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
			if (this.board.GetAt((0, 0)) == target && this.board.GetAt((1, 1)) == target && this.board.GetAt((2, 2)) == target)
			{
				return true;
			}
			else if (this.board.GetAt((2, 0)) == target && this.board.GetAt((1, 1)) == target && this.board.GetAt((0, 2)) == target)
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
			for (var row = 0; row < this.BoardSize; row++)
			{
				for (var column = 0; column < this.BoardSize; column++)
				{
					if (this.board.GetAt((row, column)) == Mark.Empty)
					{
						return false;
					}
				}
			}
			return true;
		}

		private static string ConvertFrom(Mark state)
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
	}
}
