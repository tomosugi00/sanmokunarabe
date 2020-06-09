using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Sanmoku.Models;
using Sanmoku.ViewModels.Base;
using Sanmoku.ViewModels.Event;

namespace Sanmoku.ViewModels
{
	/// <summary>
	/// タイトル画面のViewModel
	/// </summary>
	public class SettingPageViewModel : BaseViewModel
	{
		private readonly SettingModel settingModel = new SettingModel();

		#region ゲーム設定(変更不可)
		public int MaximumSize => this.settingModel.MaximumSize;
		public int MinimumSize => this.settingModel.MinimumSize;
		public int MaximumXmoku => this.settingModel.MaximumXmoku;
		public int MinimumXmoku => this.settingModel.MinimumXmoku;
		public IReadOnlyList<string> Player1TypeList => SettingModel.PlayerTypeList;
		public IReadOnlyList<string> Player2TypesList => SettingModel.PlayerTypeList;
		#endregion

		#region イベント関連

		private event EventHandler NavigateViewEventHandler;
		
		/// <summary>
		/// 画面遷移時に実行する処理を追加
		/// </summary>
		/// <param name="handler"></param>
		public void SetNavigateViewEvent(EventHandler handler)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));
			this.NavigateViewEventHandler += handler;
		}
		/// <summary>
		/// 再描画時に実行する処理を追加
		/// </summary>
		/// <param name="handler"></param>
		public void SetRepaintEvent(EventHandler handler)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));
			this.settingModel.UpdateSettingEventHandler += handler;
		}
		#endregion

		#region ゲーム設定(変更可)

		public int Player1
		{
			get => this.settingModel.Player1;
		}
		public void SetPlayer1(int index)
		{
			this.settingModel.SetPlayer1(index);
		}

		public int Player2
		{
			get => this.settingModel.Player2;
		}
		public void SetPlayer2(int index)
		{
			this.settingModel.SetPlayer2(index);
		}

		public int BoardSize => this.settingModel.BoardSize;
		public void SetBoardSize(double size)
		{
			this.settingModel.SetBoardSize((int)size);
		}

		public int Xmoku => this.settingModel.Xmoku;
		public void SetXmoku(double xmoku)
		{
			this.settingModel.SetXmoku((int)xmoku);
		}
		#endregion

		/// <summary>
		/// 対戦画面への画面遷移を行う
		/// </summary>
		public void NavigateMainPage()
		{
			//画面遷移の方法はView依存
			this.NavigateViewEventHandler?.Invoke(this, new NavigateMainPageEventArg(this.settingModel));
		}
	}
}
