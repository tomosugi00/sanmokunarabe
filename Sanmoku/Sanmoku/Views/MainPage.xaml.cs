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
			this._mainPageViewModel = new MainPageViewModel(e.Parameter);
			base.OnNavigatedTo(e);
		}
	}
}
