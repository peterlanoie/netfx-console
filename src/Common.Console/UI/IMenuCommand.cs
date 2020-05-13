using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console.UI
{
	public interface IMenuCommand
	{
		void Run(string[] args, MenuItem menuItem);
	}
}
