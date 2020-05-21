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
using Sanmoku.Views.Control;

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

			this.InitializeBoard();
		}

		/// <summary>
		/// ボードを生成します。
		/// </summary>
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
					var btn = CreateSquareButton((row, col));
					this.BoardGrid.Children.Add(btn);
				}
			}
		}

		/// <summary>
		/// マス用のボタンを生成します。
		/// </summary>
		/// <param name="square">配置するボード上の座標</param>
		/// <returns></returns>
		private Button CreateSquareButton((int row, int col) square)
		{
			var btn = new SquareButton(square)
			{
				Content = this._mainPageViewModel.GetSquareFrom((square.row, square.col)),
				BorderThickness = new Thickness(5, 5, 5, 5),
				HorizontalAlignment = HorizontalAlignment.Stretch,
				VerticalAlignment = VerticalAlignment.Stretch
			};
			//ボタン押下時
			btn.Click += new RoutedEventHandler(this.SqureButton_Click);
			//ボタン状態反映
			this._mainPageViewModel.RepaintEventHandler +=
				(s, e) => { btn.Content = this._mainPageViewModel.GetSquareFrom(btn.Square); };
			Grid.SetRow(btn, square.row);
			Grid.SetColumn(btn, square.col);
			return btn;
		}

		#region イベントハンドラー
		private void RepaintEvent(object sender, EventArgs e)
		{
			this.TurnLabel.Text = this._mainPageViewModel.GetCurrentTurn();
			this.WinnerLabel.Text = this._mainPageViewModel.GetWinner();
		}
		#endregion

		#region アクション
		private void RetryButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void SqureButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is SquareButton btn)
			{
				//this.TurnLabel.Text = string.Format("{0},{1}", btn.Row, btn.Columm);
				this._mainPageViewModel.SetSquareTo((btn.Row, btn.Columm));
			}
		}
		#endregion
	}
}
