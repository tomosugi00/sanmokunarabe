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

		/// <summary>
		/// ボードのサイズを取得します。
		/// </summary>
		public int BoardSize => this.xmokuModel.Size;

		/// <summary>
		/// 現在の操作可能なプレイヤーを示すテキストを取得します。
		/// </summary>
		public string CurrentTurn => this.xmokuModel.GetCurrentTurn() + "のターンです";

		/// <summary>
		/// 勝利者を示すテキストを取得します。
		/// </summary>
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
		/// 再描画がする際発火するイベント
		/// </summary>
		public event EventHandler RepaintEventHandler;

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

			this.xmokuModel = model;
		}

		/// <summary>
		/// ボード上の座標<paramref name="square"/>に位置するマークを取得します。
		/// </summary>
		/// <param name="square"></param>
		/// <returns></returns>
		public string GetSquareFrom((int, int) square)
		{
			return this.xmokuModel.GetAt(square);
		}

		/// <summary>
		/// ボード上の座標<paramref name="square"/>にプレイヤーのマークをセットします。
		/// <para>既に勝敗が決まっている場合は無効になります。</para>
		/// </summary>
		/// <param name="square"></param>
		public void SetSquareTo((int, int) square)
		{
			if (!this.xmokuModel.CanOperate)
			{
				return;
			}
			this.xmokuModel.SetAt(square);
			this.RepaintEventHandler?.Invoke(this, null);
		}

		/// <summary>
		/// 対戦の状態を初期化します。
		/// </summary>
		public void RetryGame()
		{
			this.xmokuModel.RetryGame();
			this.RepaintEventHandler?.Invoke(this, null);
		}


	}
}
