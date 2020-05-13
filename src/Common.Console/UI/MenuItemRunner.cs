using System;
using Sys = System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console.UI
{
	public static class MenuItemRunner
	{
		const string CHOICES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";

		public static void RunMenuItem(string[] args, MenuItem callingMenuItem)
		{
			MenuItem[] menuItems;
			ConsoleKeyInfo keyInput;
			string availableChoices, choiceChar;
			MenuItem choiceMenuItem, parent;
			IMenuCommand menuCommand;
			AsciiBoxOptions menuOptions = callingMenuItem.GetMenuOptions();
			bool drawMenu = true;

			menuOptions.HeaderJustification = AsciiLineJustification.Left;

			// check the menu item for a sub menu
			menuItems = callingMenuItem.GetSubMenu();
			if(menuItems.Length > 0)
			{
				List<string> menuLines = new List<string>();
				List<string> headerLines = new List<string>();

				headerLines.Add(callingMenuItem.MenuText);
				parent = callingMenuItem.ActiveParent;
				while(parent != null)
				{
					headerLines.Add(parent.MenuText);
					parent = parent.ActiveParent;
				}
				headerLines.Reverse();
				for(int i = 1; i < headerLines.Count; i++)
				{
					headerLines[i] = string.Concat(new string(' ', 2 * i), "└─", headerLines[i]);
				}
				for(int i = 0; i < menuItems.Length; i++)
				{
					menuLines.Add(string.Format("({0}) {1}", CHOICES[i], menuItems[i].MenuText));
				}
				availableChoices = CHOICES.Substring(0, menuLines.Count);
				menuLines.Add(string.Format("(ESC) {0}", callingMenuItem.EscapeText));
				while(true)
				{
					if(drawMenu)
					{
						ConsoleExt.WriteAsciiMenu(menuOptions, headerLines.ToArray(), menuLines.ToArray());
						Sys.Console.Write(" > ");
						drawMenu = false;
					}
					keyInput = Sys.Console.ReadKey(true);
					choiceChar = keyInput.KeyChar.ToString().ToUpper();
					if(availableChoices.Contains(choiceChar) || keyInput.Key == ConsoleKey.Escape)
					{
						if(keyInput.Key == ConsoleKey.Escape)
						{
							Sys.Console.WriteLine();
							return;
						}
						Sys.Console.WriteLine(keyInput.KeyChar);
						// a menu item has been chosen, run it
						choiceMenuItem = menuItems[availableChoices.IndexOf(choiceChar)];
						choiceMenuItem.ActiveParent = callingMenuItem;
						RunMenuItem(args, choiceMenuItem);
						choiceMenuItem.ActiveParent = null;
						drawMenu = true;
					}
				}
			}
			else
			{
				// this menu item doesn't have any sub menu, run its command
				menuCommand = callingMenuItem.GetCommand();
				if(menuCommand == null)
				{
					Sys.Console.Error.WriteLine(string.Format("!! The menu item {0} does not provide a command to process or a sub menu to display. !!", callingMenuItem.GetType().Name));
					return;
				}
				menuCommand.Run(args, callingMenuItem);
			}
		}

	}
}
