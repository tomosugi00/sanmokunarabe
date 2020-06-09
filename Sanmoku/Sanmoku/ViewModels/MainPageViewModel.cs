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
		public void SetRepaintEvent(EventHandler handler)
		{
			this.xmokuModel.RepaintEventHandler += handler;
		}

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

		public void GameStart()
		{
			this.xmokuModel.GameStart();
		}
		public void GameRetry()
		{
			this.xmokuModel.GameRetry();
		}

		public string GetSquare((int, int) square)
		{
			return this.xmokuModel.GetSquare(square);
		}
		public void SetSquare((int, int) square)
		{
			if (!this.xmokuModel.CanOperate)
			{
				return;
			}
			this.xmokuModel.SetSquare(square);
		}
	}
}
