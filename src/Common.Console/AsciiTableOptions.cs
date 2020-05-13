using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console
{
	public class AsciiTableOptions : AsciiLineOptions
	{
		public bool IncludeHorizontalSeparator { get; set; }

		public string Title { get; set; }

	}
}
