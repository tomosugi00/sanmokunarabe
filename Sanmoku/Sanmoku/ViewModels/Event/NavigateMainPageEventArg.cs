using Sanmoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.ViewModels.Event
{
	public class NavigateMainPageEventArg : EventArgs
	{
		/// <summary>
		/// Page間で送受信するModel
		/// </summary>
		public XmokuModel XmokuModel { get; }

		public NavigateMainPageEventArg(XmokuModel model)
		{
			if (model == null)
				throw new ArgumentNullException();
			this.XmokuModel = model;
		}
	}
}
