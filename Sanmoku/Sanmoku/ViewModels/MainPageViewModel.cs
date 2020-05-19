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
		private readonly XmokuModel _sanmokuModel;

		private string _turn;
		private string _winner;
		private bool _isFinished;

		private string _square00;
		private string _square01;
		private string _square02;
		private string _square10;
		private string _square11;
		private string _square12;
		private string _square20;
		private string _square21;
		private string _square22;

		private ICommand _square00Command;
		private ICommand _square01Command;
		private ICommand _square02Command;
		private ICommand _square10Command;
		private ICommand _square11Command;
		private ICommand _square12Command;
		private ICommand _square20Command;
		private ICommand _square21Command;
		private ICommand _square22Command;
		private ICommand _retryCommand;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		public MainPageViewModel(object obj)
		{
			var model = null as XmokuModel;
			if (obj is NavigateMainPageEventArg e && e.XmokuModel != null)
			{
				model = e.XmokuModel;
			}
			else
			{
				throw new ArgumentException(nameof(obj));
			}

			this._sanmokuModel = model;
			this.Turn = model.GetCurrentTurn();
			this.Square00 = model.GetAt((0, 0));
			this.Square01 = model.GetAt((0, 1));
			this.Square02 = model.GetAt((0, 2));
			this.Square10 = model.GetAt((1, 0));
			this.Square11 = model.GetAt((1, 1));
			this.Square12 = model.GetAt((1, 2));
			this.Square20 = model.GetAt((2, 0));
			this.Square21 = model.GetAt((2, 1));
			this.Square22 = model.GetAt((2, 2));
			this.IsFinished = false;
			this.Winner = string.Empty;

			model.SquareChangedEventHandler += new EventHandler(this.SquareChanged);
			model.TurnChangedEventHandler += new EventHandler(this.TurnChanged);
			model.RetryEventHandler += new EventHandler(this.Retry);
			model.FinishedEventHandler += new EventHandler(this.Finished);
		}

		#region 状態管理
		public bool IsFinished
		{
			get
			{
				return this._isFinished;
			}
			private set
			{
				if (this._isFinished != value)
				{
					this._isFinished = value;
					RaisePropertyChanged("IsFinished");
				}
			}
		}

		#endregion

		#region ラベル
		public string Turn
		{
			get
			{
				return this._turn + "のターンです";
			}
			private set
			{
				if (this._turn != value)
				{
					this._turn = value;
					RaisePropertyChanged("Turn");
				}
			}
		}
		public string Winner
		{
			get
			{
				return this._winner;
			}
			private set
			{
				if (this._winner != value)
				{
					this._winner = value;
					RaisePropertyChanged("Winner");
				}
			}
		}
		#endregion

		#region やり直しボタン
		public ICommand RetryCommand
		{
			get
			{
				if (this._retryCommand == null)
					this._retryCommand = new DelegateCommand
					{
						ExecuteHandler = RetryCommandExecute,
						CanExecuteHandler = null,
					};
				return this._retryCommand;
			}
		}
		private void RetryCommandExecute(object parameter)
		{
			this._sanmokuModel.Retry();
		}
		private bool RetryCommandCanExecute(object obj)
		{
			return IsFinished;
		}
		#endregion

		#region マス00
		private bool SquareCommandCanExecute(object obj)
		{
			return !IsFinished;
		}
		public string Square00
		{
			get
			{
				return this._square00;
			}
			private set
			{
				if (this._square00 != value)
				{
					this._square00 = value;
					RaisePropertyChanged("Square00");
				}
			}
		}
		public ICommand Square00Command
		{
			get
			{
				if (this._square00Command == null)
					this._square00Command = new DelegateCommand
					{
						ExecuteHandler = Square00CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square00Command;
			}
		}
		private void Square00CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((0, 0));
		}
		#endregion
		#region マス01
		public string Square01
		{
			get
			{
				return this._square01;
			}
			private set
			{
				if (this._square01 != value)
				{
					this._square01 = value;
					RaisePropertyChanged("Square01");
				}
			}
		}
		public ICommand Square01Command
		{
			get
			{
				if (this._square01Command == null)
					this._square01Command = new DelegateCommand
					{
						ExecuteHandler = Square01CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square01Command;
			}
		}
		private void Square01CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((0, 1));

		}
		#endregion
		#region マス02
		public string Square02
		{
			get
			{
				return this._square02;
			}
			private set
			{
				if (this._square02 != value)
				{
					this._square02 = value;
					RaisePropertyChanged("Square02");
				}
			}
		}
		public ICommand Square02Command
		{
			get
			{
				if (this._square02Command == null)
					this._square02Command = new DelegateCommand
					{
						ExecuteHandler = Square02CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square02Command;
			}
		}
		private void Square02CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((0, 2));

		}
		#endregion
		#region マス10
		public string Square10
		{
			get
			{
				return this._square10;
			}
			private set
			{
				if (this._square10 != value)
				{
					this._square10 = value;
					RaisePropertyChanged("Square10");
				}
			}
		}
		public ICommand Square10Command
		{
			get
			{
				if (this._square10Command == null)
					this._square10Command = new DelegateCommand
					{
						ExecuteHandler = Square10CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square10Command;
			}
		}
		private void Square10CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((1, 0));

		}
		#endregion
		#region マス11
		public string Square11
		{
			get
			{
				return this._square11;
			}
			private set
			{
				if (this._square11 != value)
				{
					this._square11 = value;
					RaisePropertyChanged("Square11");
				}
			}
		}
		public ICommand Square11Command
		{
			get
			{
				if (this._square11Command == null)
					this._square11Command = new DelegateCommand
					{
						ExecuteHandler = Square11CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square11Command;
			}
		}
		private void Square11CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((1, 1));
		}
		#endregion
		#region マス12
		public string Square12
		{
			get
			{
				return this._square12;
			}
			private set
			{
				if (this._square12 != value)
				{
					this._square12 = value;
					RaisePropertyChanged("Square12");
				}
			}
		}
		public ICommand Square12Command
		{
			get
			{
				if (this._square12Command == null)
					this._square12Command = new DelegateCommand
					{
						ExecuteHandler = Square12CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square12Command;
			}
		}
		private void Square12CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((1, 2));
		}
		#endregion
		#region マス20
		public string Square20
		{
			get
			{
				return this._square20;
			}
			private set
			{
				if (this._square20 != value)
				{
					this._square20 = value;
					RaisePropertyChanged("Square20");
				}
			}
		}
		public ICommand Square20Command
		{
			get
			{
				if (this._square20Command == null)
					this._square20Command = new DelegateCommand
					{
						ExecuteHandler = Square20CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square20Command;
			}
		}
		private void Square20CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((2, 0));
		}
		#endregion
		#region マス21
		public string Square21
		{
			get
			{
				return this._square21;
			}
			private set
			{
				if (this._square21 != value)
				{
					this._square21 = value;
					RaisePropertyChanged("Square21");
				}
			}
		}
		public ICommand Square21Command
		{
			get
			{
				if (this._square21Command == null)
					this._square21Command = new DelegateCommand
					{
						ExecuteHandler = Square21CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square21Command;
			}
		}
		private void Square21CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((2, 1));
		}
		#endregion
		#region マス22
		public string Square22
		{
			get
			{
				return this._square22;
			}
			private set
			{
				if (this._square22 != value)
				{
					this._square22 = value;
					RaisePropertyChanged("Square22");
				}
			}
		}
		public ICommand Square22Command
		{
			get
			{
				if (this._square22Command == null)
					this._square22Command = new DelegateCommand
					{
						ExecuteHandler = Square22CommandExecute,
						CanExecuteHandler = SquareCommandCanExecute,
					};
				return this._square22Command;
			}
		}
		private void Square22CommandExecute(object parameter)
		{
			this._sanmokuModel.SetAt((2, 2));
		}
		#endregion

		#region イベント
		private void SquareChanged(object sender, EventArgs e)
		{
			this.Square00 = this._sanmokuModel.GetAt((0, 0));
			this.Square01 = this._sanmokuModel.GetAt((0, 1));
			this.Square02 = this._sanmokuModel.GetAt((0, 2));
			this.Square10 = this._sanmokuModel.GetAt((1, 0));
			this.Square11 = this._sanmokuModel.GetAt((1, 1));
			this.Square12 = this._sanmokuModel.GetAt((1, 2));
			this.Square20 = this._sanmokuModel.GetAt((2, 0));
			this.Square21 = this._sanmokuModel.GetAt((2, 1));
			this.Square22 = this._sanmokuModel.GetAt((2, 2));
		}

		private void TurnChanged(object sender, EventArgs e)
		{
			this.Turn = this._sanmokuModel.GetCurrentTurn();
		}

		private void Retry(object sender, EventArgs e)
		{
			this.Turn = this._sanmokuModel.GetCurrentTurn();
			this.Square00 = this._sanmokuModel.GetAt((0, 0));
			this.Square01 = this._sanmokuModel.GetAt((0, 1));
			this.Square02 = this._sanmokuModel.GetAt((0, 2));
			this.Square10 = this._sanmokuModel.GetAt((1, 0));
			this.Square11 = this._sanmokuModel.GetAt((1, 1));
			this.Square12 = this._sanmokuModel.GetAt((1, 2));
			this.Square20 = this._sanmokuModel.GetAt((2, 0));
			this.Square21 = this._sanmokuModel.GetAt((2, 1));
			this.Square22 = this._sanmokuModel.GetAt((2, 2));
			this.IsFinished = false;
			this.Winner = string.Empty;
		}

		private void Finished(object sender, EventArgs e)
		{
			this.IsFinished = true;
			if (this._sanmokuModel.GetWinner() != string.Empty)
			{
				this.Winner = this._sanmokuModel.GetWinner() + "の勝利です";
			}
			else
			{
				this.Winner = "引き分けです";
			}
		}

		#endregion
	}
}
