using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Console.UI;

namespace Common.Console.Tests.CLI.Menus
{
	class MainMenu : MenuItem
	{
		public override string MenuText { get { return "Console Menuing Test"; } }
		public override string EscapeText { get { return "Exit"; } }

		public override MenuItem[] GetSubMenu()
		{
			return new SubMenu1().GetSubMenu();
		}


	}
}
