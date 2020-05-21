using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Sanmoku.Models;
using Sanmoku.ViewModels.Base;
using Sanmoku.ViewModels.Event;

namespace Sanmoku.ViewModels
{
	public class MainPageViewModel : BaseViewModel
	{
		private readonly XmokuModel _xmokuModel;

		private string _turn;
		private string _winner;
		private bool _isFinished;

		#region イベントハンドラー
		/// <summary>
		/// 再描画がする際発火するイベント
		/// </summary>
		public event EventHandler RepaintEventHandler;
		#endregion

		public int BoardSize
		{
			get => this._xmokuModel.Size;
		}


		public MainPageViewModel(object obj)
		{
			//遷移元からModel受け取り
			XmokuModel model;
			if (obj is NavigateMainPageEventArg e && e.XmokuModel != null)
			{
				model = e.XmokuModel;
			}
			else
			{
				throw new ArgumentException(nameof(obj));
			}

			this._xmokuModel = model;

			//this.Turn = model.GetCurrentTurn();
			//this.IsFinished = false;
			//this.Winner = string.Empty;

			//イベントハンドラ
			//this._xmokuModel.SquareChangedEventHandler += new EventHandler(this.SquareChanged);
			//this._xmokuModel.TurnChangedEventHandler += new EventHandler(this.TurnChanged);
			//this._xmokuModel.RetryEventHandler += new EventHandler(this.Retry);
			//this._xmokuModel.FinishedEventHandler += new EventHandler(this.Finished);

			//リペイント？

		}

		public string GetSquareFrom((int, int) square)
		{
			return this._xmokuModel.GetAt(square);
		}

		public void SetSquareTo((int, int) square)
		{
			if (this._xmokuModel.IsFinished)
			{
				return;
			}
			this._xmokuModel.SetAt(square);
			RepaintEventHandler?.Invoke(this, null);
		}

		public string GetCurrentTurn()
		{
			return this._xmokuModel.GetCurrentTurn() + "のターンです";
		}
		public string GetWinner()
		{
			if (string.IsNullOrWhiteSpace(this._xmokuModel.GetWinner()))
			{
				return this._xmokuModel.GetWinner();
			}
			return this._xmokuModel.GetWinner() + "の勝利です";
		}
	}
}
