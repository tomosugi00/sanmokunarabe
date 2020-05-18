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
	/// <see cref="TitlePage"/>のコードビハインドです。
	/// <para>画面の初期化と更新を担当します。</para>
	/// </summary>
	public sealed partial class TitlePage : Page
	{
		private readonly TitlePageViewModel titlePageViewModel = new TitlePageViewModel();

		public TitlePage()
		{
			this.InitializeComponent();
			this.SettingCompornent();
		}

		/// <summary>
		/// コンポーネントの各種設定を行う
		/// </summary>
		public void SettingCompornent()
		{
			this.SettingPlayer1Combobox();
			this.SettingPlayer2Combobox();

			this.SettingSizeSlider();
			this.SettingXmokuSlider();
		}

		/// <summary>
		/// 画面の再描画を行います。
		/// </summary>
		public void OnRepaint(object sender, EventArgs e)
		{
			//ViewModelの状態を参考にする
		}

		#region 初期設定
		/// <summary>
		/// プレイヤー1を指定するコンボボックスの初期設定を行います。
		/// </summary>
		private void SettingPlayer1Combobox()
		{
			foreach (var type in this.titlePageViewModel.Player1Types)
			{
				this.Player1ComboBox.Items.Add(type);
			}
			this.Player1ComboBox.SelectedIndex = 0;
		}
		/// <summary>
		/// プレイヤー2を指定するコンボボックスの初期設定を行います。
		/// </summary>
		private void SettingPlayer2Combobox()
		{
			foreach (var type in this.titlePageViewModel.Player2Types)
			{
				this.Player2ComboBox.Items.Add(type);
			}
			this.Player2ComboBox.SelectedIndex = 0;

		}
		/// <summary>
		/// ボードサイズを変更するスライダーの初期設定を行います。
		/// </summary>
		private void SettingSizeSlider()
		{
			this.SizeSlider.Maximum = this.titlePageViewModel.MaximumSize;
			this.SizeSlider.Minimum = this.titlePageViewModel.MinimumSize;
			this.SizeSlider.Value = this.SizeSlider.Minimum;
			return;
		}
		/// <summary>
		/// X目(勝利マーク数)を変更するスライダーの初期設定を行います。
		/// </summary>
		private void SettingXmokuSlider()
		{
			this.XmokuSlider.Maximum = this.titlePageViewModel.MaximumXmoku;
			this.XmokuSlider.Minimum = this.titlePageViewModel.MinimumXmoku;
			this.XmokuSlider.Value = this.XmokuSlider.Minimum;
			return;
		}
		#endregion
	}
}
