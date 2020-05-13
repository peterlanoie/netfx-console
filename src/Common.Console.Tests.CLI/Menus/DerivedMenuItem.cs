using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Console.UI;

namespace Common.Console.Tests.CLI.Menus
{
	abstract class DerivedMenuItem : MenuItem
	{
		public override string MenuText
		{
			get { return this.GetType().Name; }
		}
	}

	abstract class SubMenuAlpha : DerivedMenuItem
	{
		public override MenuItem[] GetSubMenu()
		{
			return new MenuItem[] { new SubMenu1(), new SubMenu2(), new SubMenu3(), new CommandItem() };
		}
	}

	class SubMenuA : SubMenuAlpha { }
	class SubMenuB : SubMenuAlpha { }
	class SubMenuC : SubMenuAlpha { }

	abstract class SubMenuNumeric : DerivedMenuItem
	{
		public override MenuItem[] GetSubMenu()
		{
			return new MenuItem[] { new SubMenuA(), new SubMenuB(), new SubMenuC(), new CommandItem() };
		}
	}

	class SubMenu1 : SubMenuNumeric { }
	class SubMenu2 : SubMenuNumeric { }
	class SubMenu3 : SubMenuNumeric { }

}
