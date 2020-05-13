using System;
using System.Linq;
using Sys = System;
using System.Collections.Generic;

namespace Common.Console
{
	public static partial class ConsoleExt
	{

		public static void WriteAsciiBox(params string[] lines)
		{
			WriteAsciiBox(new AsciiBoxOptions(), lines);
		}

		public static void WriteAsciiBox(AsciiBoxOptions options, params string[] lines)
		{
			WriteAsciiMenu(options, new string[] { }, lines);
		}

		public static void WriteAsciiMenu(string heading, params string[] lines)
		{
			WriteAsciiMenu(new AsciiBoxOptions(), heading, lines);
		}

		public static void WriteAsciiMenu(AsciiBoxOptions options, string heading, params string[] lines)
		{
			WriteAsciiMenu(options, new string[] { heading }, lines);
		}

		public static void WriteAsciiMenu(AsciiBoxOptions options, string[] heading, string[] lines)
		{
			int maxLength = 0;
			heading = heading ?? new string[] { };

			foreach(var item in lines.Union(heading))
			{
				maxLength = Math.Max(maxLength, item.Length);
			}
			options.Style.SetOptions(options);
			options.Style.FullTextWidth = maxLength;

			Sys.Console.WriteLine(options.Style.GetTopLine());
			if(heading != null && heading.Length > 0)
			{
				foreach(var headingLine in heading)
				{
					Sys.Console.WriteLine(options.Style.GetTextLine(headingLine, options.HeaderJustification));
				}
				Sys.Console.WriteLine(options.Style.GetSeparatorLine());
			}
			foreach(var item in lines)
			{
				Sys.Console.WriteLine(options.Style.GetTextLine(item));
			}
			Sys.Console.WriteLine(options.Style.GetBotLine());
		}

	}
}
