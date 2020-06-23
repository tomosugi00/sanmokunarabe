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
		private readonly XmokuModel xmokuModel;

		public int BoardSize => this.xmokuModel.BoardSize;

		public string CurrentTurn => this.xmokuModel.GetCurrentTurn() + "のターンです";

		public string Winner
		{
			get
			{
				if (!this.xmokuModel.IsFinished)
				{
					return string.Empty; //対戦中
				}
				else if (string.IsNullOrWhiteSpace(this.xmokuModel.GetWinner()))
				{
					return "引き分けです。";   //引き分け
				}
				return this.xmokuModel.GetWinner() + "の勝利です";
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="obj"></param>
		public MainPageViewModel(object obj)
		{
			if (obj is NavigateMainPageEventArg e && e.ISettingModel != null)
			{
				this.xmokuModel = new XmokuModel(e.ISettingModel);
			}
			else
			{
				throw new ArgumentException(nameof(obj));
			}
		}

		/// <summary>
		/// 再描画時の処理を登録します。
		/// </summary>
		/// <param name="handler"></param>
		public void SetRepaintEvent(EventHandler handler)
		{
			this.xmokuModel.RepaintEventHandler += handler;
		}

		/// <summary>
		/// ゲームを開始します。
		/// </summary>
		public void GameStart()
		{
			this.xmokuModel.GameStart();
		}

		/// <summary>
		/// ゲームをやり直します。
		/// </summary>
		public void GameRetry()
		{
			this.xmokuModel.GameRetry();
		}

		/// <summary>
		/// <paramref name="square"/>の位置のマークを取得します。
		/// </summary>
		/// <param name="square"></param>
		/// <returns></returns>
		public string GetSquare((int, int) square)
		{
			return this.xmokuModel.GetSquare(square);
		}

		/// <summary>
		/// <paramref name="square"/>の位置に現在のターンのマークをセットします。
		/// 操作不能の状態の場合、処理は行われません。
		/// </summary>
		/// <param name="square"></param>
		public void SetSquare((int, int) square)
		{
			this.xmokuModel.SetSquareIfCan(square);
		}
	}
}
