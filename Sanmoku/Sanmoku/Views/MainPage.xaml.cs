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
	/// <see cref="MainPage"/>のコードビハインド
	/// </summary>
	public sealed partial class MainPage : Page
	{
		/// <summary>
		/// 対応するViewModel
		/// </summary>
		private MainPageViewModel _mainPageViewModel;

		public MainPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			this._mainPageViewModel = new MainPageViewModel(e.Parameter);
			this._mainPageViewModel.RepaintEventHandler += new EventHandler(this.RepaintEvent);

			//テスト
			this.InitializeBoard();
		}

		private void InitializeBoard()
		{
			var boardSize = this._mainPageViewModel.BoardSize;

			for (var i = 0; i < boardSize; i++)
			{
				this.BoardGrid.RowDefinitions.Add(new RowDefinition());
				this.BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
			}

			for (int row = 0; row < boardSize; row++)
			{
				for (int col = 0; col < boardSize; col++)
				{
					var btn = CreateSquareButton(row, col);
					this.BoardGrid.Children.Add(btn);
				}
			}
		}

		private Button CreateSquareButton(int row, int col)
		{
			var btn = new Button()
			{
				Content = this._mainPageViewModel.GetSquareFrom((row, col)),
				BorderThickness = new Thickness(5, 5, 5, 5),
				HorizontalAlignment = HorizontalAlignment.Stretch,
				VerticalAlignment = VerticalAlignment.Stretch
			};
			btn.Click += new RoutedEventHandler(this.SqureButton_Click);
			Grid.SetColumn(btn, col);
			Grid.SetRow(btn, row);
			return btn;
		}

		#region イベントハンドラー
		private void RepaintEvent(object sender, EventArgs e)
		{

		}
		#endregion

		#region アクション
		private void RetryButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void SqureButton_Click(object sender, RoutedEventArgs e)
		{
			this.TurnLabel.Text = DateTime.UtcNow.ToString();
		}
		#endregion
	}
}
