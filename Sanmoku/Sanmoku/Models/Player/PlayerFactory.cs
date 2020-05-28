using Sanmoku.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models.Player
{
	public static class PlayerFactory
	{
		public static BasePlayer GetPlayer1(XmokuModel model)
		{
			if (!Enum.IsDefined(typeof(Mark), model.Player1))
			{
				throw new ArgumentException(nameof(model));
			}
			switch ((PlayerType)model.Player1)
			{
				case PlayerType.Player:
					return new Player(model);
				case PlayerType.CPU:
					return new Cpu(model);
				case PlayerType.NetWork:
					return new Player(model);
				default:
					throw new NotImplementedException();
			}
		}
		public static BasePlayer GetPlayer2(XmokuModel model)
		{
			if (!Enum.IsDefined(typeof(Mark), model.Player1))
			{
				throw new ArgumentException(nameof(model));
			}
			switch ((PlayerType)model.Player2)
			{
				case PlayerType.Player:
					return new Player(model);
				case PlayerType.CPU:
					return new Cpu(model);
				case PlayerType.NetWork:
					return new Player(model);
				default:
					throw new NotImplementedException();
			}
		}
	}
}
