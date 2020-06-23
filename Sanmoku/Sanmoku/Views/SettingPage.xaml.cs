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
	/// <see cref="SettingPage"/>のコードビハインド
	/// </summary>
	public sealed partial class SettingPage : Page
	{
		private readonly SettingPageViewModel settingPageViewModel = new SettingPageViewModel();

		public SettingPage()
		{
			this.InitializeComponent();
			this.SettingCompornent();

			this.settingPageViewModel.SetRepaintEvent(new EventHandler(this.RepaintEvent));
			this.settingPageViewModel.SetNavigateViewEvent(new EventHandler(this.NavigateMainPageEvent));
		}

		#region イベント

		/// <summary>
		/// 画面の再描画を行います。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RepaintEvent(object sender, EventArgs e)
		{
			this.XLabel.Text = this.settingPageViewModel.Xmoku.ToString();
			this.Player1ComboBox.SelectedIndex = this.settingPageViewModel.Player1;
			this.Player2ComboBox.SelectedIndex = this.settingPageViewModel.Player2;
			this.SizeSlider.Value = this.settingPageViewModel.BoardSize;
			this.XmokuSlider.Value = this.settingPageViewModel.Xmoku;
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
			this.SettingXLabelTextBox();
			this.SettingPlayer1Combobox();
			this.SettingPlayer2Combobox();
			this.SettingSizeSlider();
			this.SettingXmokuSlider();
			return;
		}
		/// <summary>
		/// ゲームタイトルの初期設定を行います。
		/// </summary>
		private void SettingXLabelTextBox()
		{
			this.XLabel.Text = this.settingPageViewModel.Xmoku.ToString();
			return;
		}
		/// <summary>
		/// プレイヤー1を指定するコンボボックスの初期設定を行います。
		/// </summary>
		private void SettingPlayer1Combobox()
		{
			foreach (var type in this.settingPageViewModel.Player1TypeList)
			{
				this.Player1ComboBox.Items.Add(type);
			}
			this.Player1ComboBox.SelectedIndex = this.settingPageViewModel.Player1;
			return;
		}
		/// <summary>
		/// プレイヤー2を指定するコンボボックスの初期設定を行います。
		/// </summary>
		private void SettingPlayer2Combobox()
		{
			foreach (var type in this.settingPageViewModel.Player2TypesList)
			{
				this.Player2ComboBox.Items.Add(type);
			}
			this.Player2ComboBox.SelectedIndex = this.settingPageViewModel.Player2;
			return;
		}
		/// <summary>
		/// ボードサイズを変更するスライダーの初期設定を行います。
		/// </summary>
		private void SettingSizeSlider()
		{
			this.SizeSlider.Maximum = this.settingPageViewModel.MaximumSize;
			this.SizeSlider.Minimum = this.settingPageViewModel.MinimumSize;
			this.SizeSlider.Value = this.settingPageViewModel.BoardSize;
			return;
		}
		/// <summary>
		/// X目(勝利マーク数)を変更するスライダーの初期設定を行います。
		/// </summary>
		private void SettingXmokuSlider()
		{
			this.XmokuSlider.Maximum = this.settingPageViewModel.MaximumXmoku;
			this.XmokuSlider.Minimum = this.settingPageViewModel.MinimumXmoku;
			this.XmokuSlider.Value = this.settingPageViewModel.Xmoku;
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
			this.settingPageViewModel.SetPlayer1(this.Player1ComboBox.SelectedIndex);
		}
		/// <summary>
		/// プレイヤー2を指定した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Player2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.settingPageViewModel.SetPlayer2(this.Player2ComboBox.SelectedIndex);
		}
		/// <summary>
		/// サイズを指定した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SizeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			this.settingPageViewModel.SetBoardSize(this.SizeSlider.Value);
		}
		/// <summary>
		/// X目(勝利マーク数)を指定した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void XmokuSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			this.settingPageViewModel.SetXmoku(this.XmokuSlider.Value);
		}
		/// <summary>
		/// スタートボタンを押下した際のアクション
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			//画面遷移。モデルを手渡し。
			this.settingPageViewModel.NavigateMainPage();
		}
		#endregion
	}
}
