using System;
using System.Linq;
using Sys = System;
using System.Collections.Generic;

namespace Common.Console
{
	public static partial class ConsoleExt
	{

		public static void ClearCurrentLine()
		{
			int currentLineCursor = Sys.Console.CursorTop;
			Sys.Console.SetCursorPosition(0, Sys.Console.CursorTop);
			Sys.Console.Write(new string(' ', Sys.Console.BufferWidth - 1));
			Sys.Console.SetCursorPosition(0, currentLineCursor);
		}

		/// <summary>
		/// Clears and rewrites the current line with provided value.
		/// </summary>
		/// <param name="value"></param>
		public static string Rewrite(string value)
		{
			ClearCurrentLine();
			Sys.Console.Write(value);
			return value;
		}

		/// <summary>
		/// Rewrites the current console line with the provided formatted string.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="arg"></param>
		public static string Rewrite(string format, params object[] arg)
		{
			return Rewrite(string.Format(format, arg));
		}

		/// <summary>
		/// Writes a progress bar on the current line for the provided numeric values.
		/// </summary>
		/// <param name="total"></param>
		/// <param name="complete"></param>
		public static void WriteProgress(Int64 total, Int64 complete)
		{
			int completeWidth, totalWidth, numWidth;
			string label;
			decimal percentComplete;

			numWidth = total.ToString().Length;
			percentComplete = (complete == 0 ? 0 : (decimal)complete / (decimal)total);
			label = string.Format("{0}/{1}({2}%)", complete.ToString().PadLeft(numWidth, ' '), total, Math.Round(percentComplete * 100, 1).ToString().PadLeft(4, ' '));
			totalWidth = Sys.Console.BufferWidth - (label.Length + 3);
			completeWidth = (int)Math.Floor(percentComplete * (decimal)totalWidth);
			string bar = string.Format("▐{0}{1}▌{2}", new string('■', completeWidth), new string(' ', totalWidth - completeWidth), label);
			Rewrite(bar);
		}

		/// <summary>
		/// Writes a line to the console, automatically truncating it basedon the current console width
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static string WriteTruncatedLine(string format, params object[] args)
		{
			string line = string.Format(format, args);
			if (line.Length > Sys.Console.BufferWidth)
			{
				line = string.Concat(line.Substring(0, Sys.Console.BufferWidth - 4), "...");
			}
			Sys.Console.WriteLine(line);
			return line;
		}

		/// <summary>
		/// Writes out a horizontal rule using the standard style from the current cursor point to the end of the line.
		/// </summary>
		public static string WriteHorizontalRule()
		{
			return WriteHorizontalRule(AsciiLineStyle.DoubleLine);
		}

		/// <summary>
		/// Writes out a horizontal rule using the specified style from the current cursor point to the end of the line.
		/// </summary>
		/// <param name="style"></param>
		public static string WriteHorizontalRule(AsciiLineStyle style)
		{
			var line = new string(style.HorizontalRuleChar, Sys.Console.BufferWidth - Sys.Console.CursorLeft);
			Sys.Console.Write(line);
			return line;
		}

		/// <summary>
		/// Writes the provided string on the current line, collapsing it (in the middle) if necessary.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="args"></param>
		public static void WriteTempStatus(string value, params string[] args)
		{
			ClearCurrentLine();
			if (value.Length >= Sys.Console.BufferWidth)
			{
				var mid = Sys.Console.BufferWidth / 2;
				var first = value.Substring(0, mid - 4);
				var second = value.Substring(value.Length - mid - (Sys.Console.BufferWidth % 2));
				Sys.Console.Write("{0}...{1}", first, second);
			}
			else
			{
				Sys.Console.Write(value);
			}
		}

	}
}
