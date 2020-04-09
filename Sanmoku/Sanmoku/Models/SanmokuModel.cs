using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models
{
	public class SanmokuModel
	{
		private SanmokuState _currentTurn;
		private SanmokuBoard sanmokuBoard;
		private string _result;

		public event EventHandler SquareChangedEventHandler;
		public event EventHandler TurnChangedEventHandler;
		public event EventHandler RetryEventHandler;
		public event EventHandler FinishedEventHandler;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SanmokuModel()
		{
			//初期化
			this.sanmokuBoard = new SanmokuBoard();
			this._currentTurn = SanmokuState.Maru;
			this._result = string.Empty;
		}

		public string CurrentTurn
		{
			//文字列変換
			get { return this._currentTurn.ToString(); }
		}
		public string Result
		{
			get
			{
				return this._result;
			}
			private set
			{
				this._result = value ?? string.Empty;
			}
		}

		/// <summary>
		/// 引数のマスの状態を取得する
		/// </summary>
		/// <param name="square"></param>
		/// <returns></returns>
		public string GetAt((int row, int culumn) square)
		{
			//文字列変換
			return this.sanmokuBoard.GetAt(square).ToString();
		}

		/// <summary>
		/// 引数のマスの状態を変化させる
		/// </summary>
		/// <param name="square"></param>
		/// <returns></returns>
		public string ChangeAt((int row, int culumn) square)
		{
			//文字列変換
			var result = this.sanmokuBoard.ChangeAt(square).ToString();
			this.SquareChangedEventHandler?.Invoke(this, null);
			return result;
		}

		public void Retry()
		{
			//コンストラクタと同様
			this.sanmokuBoard = new SanmokuBoard();
			this._currentTurn = SanmokuState.Maru;
			this.Result = string.Empty;
			this.RetryEventHandler?.Invoke(this, null);
			return;
		}

		/// <summary>
		/// ゲーム終了か判定し、違う場合はターンを進める
		/// </summary>
		public void FinishOrTurnChanged()
		{
			//結果確認
			if (this.sanmokuBoard.IsFinish(out var result))
			{
				this.Result = result;
				this.FinishedEventHandler?.Invoke(this, null);
				return;
			}

			this.Result = string.Empty;
			//手番交代
			if (this._currentTurn == SanmokuState.Maru)
				this._currentTurn = SanmokuState.Batsu;
			else
				this._currentTurn = SanmokuState.Maru;

			this.TurnChangedEventHandler?.Invoke(this, null);
			return;
		}
	}
}
