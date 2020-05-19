using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Sanmoku.ViewModels;

namespace Sanmoku.Views
{
	/// <summary>
	/// <see cref="TitlePage"/>のコードビハインド
	/// </summary>
	public sealed partial class TitlePage : Page
	{
		/// <summary>
		/// 対応するViewModel
		/// </summary>
		private readonly TitlePageViewModel _titlePageViewModel = new TitlePageViewModel();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TitlePage()
		{
			this.InitializeComponent();
			this.SettingCompornent();
			this._titlePageViewModel.RepaintEventHandler += new EventHandler(this.RepaintEvent);
			this._titlePageViewModel.NavigateViewEventHandler += new EventHandler(this.NavigateMainPageEvent);
		}

		#region イベント
		/// <summary>
		/// 画面の再描画を行います。
		/// </summary>
		private void RepaintEvent(object sender, EventArgs e)
		{
			//ここで二回イベントが呼ばれている。適切なプロパティを選ぶべし
			//ViewModelの状態を反映
			this.Player1ComboBox.SelectedIndex = this._titlePageViewModel.Player1;
			this.Player2ComboBox.SelectedIndex = this._titlePageViewModel.Player2;
			this.SizeSlider.Value = this._titlePageViewModel.Size;
			this.XmokuSlider.Value = this._titlePageViewModel.Xmoku;
		}
		/// <summary>
		/// 対戦画面へ画面遷移します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NavigateMainPageEvent(object sender, EventArgs e)
		{
			this.Frame.Navigate(typeof(MainPage), e);
		}
		#endregion

		#region 初期設定
		/// <summary>
		/// コンポーネントの各種設定を行う
		/// </summary>
		private void SettingCompornent()
		{
			//コンポーネントの初期化メソッドで構成
			this.SettingPlayer1Combobox();
			this.SettingPlayer2Combobox();
			this.SettingSizeSlider();
			this.SettingXmokuSlider();
			return;
		}
		/// <summary>
		/// プレイヤー1を指定するコンボボックスの初期設定を行います。
		/// </summary>
		private void SettingPlayer1Combobox()
		{
			foreach (var type in this._titlePageViewModel.Player1TypeStringList)
			{
				this.Player1ComboBox.Items.Add(type);
			}
			this.Player1ComboBox.SelectedIndex = this._titlePageViewModel.Player1;
			return;
		}
		/// <summary>
		/// プレイヤー2を指定するコンボボックスの初期設定を行います。
		/// </summary>
		private void SettingPlayer2Combobox()
		{
			foreach (var type in this._titlePageViewModel.Player2TypesStringList)
			{
				this.Player2ComboBox.Items.Add(type);
			}
			this.Player2ComboBox.SelectedIndex = this._titlePageViewModel.Player2;
			return;
		}
		/// <summary>
		/// ボードサイズを変更するスライダーの初期設定を行います。
		/// </summary>
		private void SettingSizeSlider()
		{
			this.SizeSlider.Maximum = this._titlePageViewModel.MaximumSize;
			this.SizeSlider.Minimum = this._titlePageViewModel.MinimumSize;
			this.SizeSlider.Value = this._titlePageViewModel.Size;
			return;
		}
		/// <summary>
		/// X目(勝利マーク数)を変更するスライダーの初期設定を行います。
		/// </summary>
		private void SettingXmokuSlider()
		{
			this.XmokuSlider.Maximum = this._titlePageViewModel.MaximumXmoku;
			this.XmokuSlider.Minimum = this._titlePageViewModel.MinimumXmoku;
			this.XmokuSlider.Value = this._titlePageViewModel.Xmoku;
			return;
		}
		#endregion

		#region UIアクション
		/// <summary>
		/// プレイヤー1を指定した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Player1ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this._titlePageViewModel.SetPlayer1(this.Player1ComboBox.SelectedIndex);
		}
		/// <summary>
		/// プレイヤー2を指定した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Player2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this._titlePageViewModel.SetPlayer2(this.Player2ComboBox.SelectedIndex);
		}
		/// <summary>
		/// サイズを指定した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SizeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			this._titlePageViewModel.SetSize(this.SizeSlider.Value);
		}
		/// <summary>
		/// X目(勝利マーク数)を指定した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void XmokuSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			this._titlePageViewModel.SetXmoku(this.XmokuSlider.Value);
		}
		/// <summary>
		/// スタートボタンを押下した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			//画面遷移。モデルを手渡し。
			this._titlePageViewModel.NavigateMainPage();
		}
		#endregion
	}
}
