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
	public class TitlePageViewModel : BaseViewModel
	{
		/// <summary>
		/// 連携するModelクラス
		/// </summary>
		private readonly XmokuModel _xmokuModel;

		#region ゲーム設定
		public int MaximumSize { get { return XmokuModel.MaximumSize; } }
		public int MinimumSize { get { return XmokuModel.MinimumSize; } }
		public int MaximumXmoku { get { return XmokuModel.MaximumXmoku; } }
		public int MinimumXmoku { get { return XmokuModel.MinimumXmoku; } }
		public IEnumerable<string> Player1TypeStringList { get { return XmokuModel.Player1Types; } }
		public IEnumerable<string> Player2TypesStringList { get { return XmokuModel.Player2Types; } }
		#endregion

		#region イベントハンドラー
		/// <summary>
		/// 再描画がする際発火するイベント
		/// </summary>
		public event EventHandler RepaintEventHandler;
		/// <summary>
		/// 画面遷移する際発火するイベント
		/// </summary>
		public event EventHandler NavigateViewEventHandler;
		#endregion

		#region コンポーネント状態
		/// <summary>
		/// プレイヤー1の状態
		/// </summary>
		public int Player1
		{
			get => this._xmokuModel.Player1;
		}
		public void SetPlayer1(int index)
		{
			this._xmokuModel.SetPlayer1(index);
			this.RepaintEventHandler?.Invoke(this, null);
		}
		/// <summary>
		/// プレイヤー2の状態
		/// </summary>
		public int Player2
		{
			get => this._xmokuModel.Player2;
		}
		public void SetPlayer2(int index)
		{
			this._xmokuModel.SetPlayer2(index);
			this.RepaintEventHandler?.Invoke(this, null);
		}
		/// <summary>
		/// ボードサイズの状態
		/// </summary>
		public int Size
		{
			get => this._xmokuModel.Size;
		}
		public void SetSize(double size)
		{
			this._xmokuModel.SetSize((int)size);
			this.RepaintEventHandler?.Invoke(this, null);
		}
		/// <summary>
		/// X目の状態
		/// </summary>
		public int Xmoku
		{
			get => this._xmokuModel.Xmoku;
		}
		public void SetXmoku(double xmoku)
		{
			this._xmokuModel.SetXmoku((int)xmoku);
			this.RepaintEventHandler?.Invoke(this, null);
		}
		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TitlePageViewModel()
		{
			this._xmokuModel = new XmokuModel();
		}

		/// <summary>
		/// 対戦画面への画面遷移を行う
		/// </summary>
		public void NavigateMainPage()
		{
			//画面遷移の方法はView依存
			this.NavigateViewEventHandler?.Invoke(this, new NavigateMainPageEventArg(this._xmokuModel));
		}
	}
}
