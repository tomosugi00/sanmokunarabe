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
		public ISetting ISettingModel { get; }

		public NavigateMainPageEventArg(ISetting model)
		{
			this.ISettingModel = model ?? throw new ArgumentNullException(nameof(model));
		}
	}
}
