using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Console.UI;

namespace Common.Console.Tests.CLI.Menus
{
	class CommandItem : DerivedMenuItem, IMenuCommand
	{
		public override IMenuCommand GetCommand()
		{
			return this;
		}

		public void Run(string[] args, MenuItem menuItem)
		{
			System.Console.WriteLine("Here's the menu stack:");
			WriteMenuStack(menuItem);
			System.Console.WriteLine();
		}

		private void WriteMenuStack(MenuItem menuItem)
		{
			if(menuItem.ActiveParent != null)
			{
				WriteMenuStack(menuItem.ActiveParent);
				System.Console.Write(" > ");
			}
			System.Console.Write(menuItem.MenuText);
		}
	}
}
