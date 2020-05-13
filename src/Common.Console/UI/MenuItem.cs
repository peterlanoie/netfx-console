using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console.UI
{
	public abstract class MenuItem
	{
		/// <summary>
		/// The parent <see cref="MenuItem"/> that invoked this menu item.
		/// </summary>
		public MenuItem ActiveParent { get; internal set; }

		public virtual string MenuText { get { return this.GetType().Name; } }

		/// <summary>
		/// The text to use for the ESC menu option. Defailt is 'Back'.
		/// </summary>
		public virtual string EscapeText { get { return "Back"; } }

		public virtual IMenuCommand GetCommand()
		{
			return null;
		}

		public virtual MenuItem[] GetSubMenu()
		{
			return new MenuItem[] { };
		}
		public virtual AsciiBoxOptions GetMenuOptions()
		{
			return new AsciiBoxOptions() { Justify = AsciiLineJustification.Left, Style = AsciiLineStyle.SolidThin };
		}

	}
}
