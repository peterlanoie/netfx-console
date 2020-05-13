using System;
using System.Linq;
using Sys = System;
using System.Collections.Generic;
using System.Text;

namespace Common.Console
{
	public static partial class ConsoleExt
	{

		public static string AsciiTable(string[] headers, List<object[]> data, AsciiTableOptions options, Action<string> lineWriter = null)
		{
			if (data.Count == 0)
			{
				return null;
			}
			var output = new StringBuilder();
			int[] colSizes = new int[headers.Length];
			int titleSize, colSizesSum;
			string title = options.Title;
			AsciiLineStyle style = options.Style;

			for(int i = 0; i < colSizes.Length; i++)
			{
				colSizes[i] = data.Max(x => (x[i] ?? "").ToString().Length);
				colSizes[i] = Math.Max(colSizes[i], headers[i].Length);
			}
			colSizesSum = colSizes.Sum(x => x) + colSizes.Length - 1;
			if(!string.IsNullOrEmpty(title))
			{
				titleSize = Math.Max(title.Length, colSizesSum);
				if(titleSize > colSizesSum)
				{
					titleSize = colSizesSum;
					title = title.Substring(0, titleSize - 3) + "...";
				}
				output.Append(style.TopLtCorner);
				output.Append(new string(style.TopHorizntl, titleSize));
				output.AppendLine(style.TopRtCorner.ToString());
				output.Append(TableContentLine(style.MidLeftEdge, style.MidVerticle, style.MidRghtEdge, new string[] { title }, new int[] { titleSize }));
				output.Append(TableLine(style.HdrLeftJunc, style.HdrHorizntl, style.TopJunction, style.HdrRghtJunc, colSizes));
			}
			else
			{
				output.Append(TableLine(style.TopLtCorner, style.TopHorizntl, style.TopJunction, style.TopRtCorner, colSizes));
			}
			output.Append(TableContentLine(style.MidLeftEdge, style.MidVerticle, style.MidRghtEdge, headers.Cast<object>().ToArray(), colSizes));
			output.Append(TableLine(style.HdrLeftJunc, style.HdrHorizntl, style.HdrJunction, style.HdrRghtJunc, colSizes));
			for(int i = 0; i < data.Count; i++)
			{
				if(options.IncludeHorizontalSeparator && i > 0)
				{
					output.Append(TableLine(style.LinLeftJunc, style.LinHorizntl, style.LinMidJunct, style.LinRghtJunc, colSizes));
				}
				output.Append(TableContentLine(style.MidLeftEdge, style.MidVerticle, style.MidRghtEdge, data[i], colSizes));
			}
			output.Append(TableLine(style.BotLtCorner, style.BotHorizntl, style.BotJunction, style.BotRtCorner, colSizes));

			if (lineWriter != null)
			{
				foreach (var line in output.ToString().Split(new[] { "\r\n" }, StringSplitOptions.None))
				{
					lineWriter(line);
				}
			}

			return output.ToString();
		}

		private static string TableContentLine(char start, char mid, char end, object[] contents, int[] colSizes)
		{
			var output = new StringBuilder();
			output.Append(start);
			string value;
			for(int i = 0; i < contents.Length; i++)
			{
				if(i > 0)
				{
					output.Append(mid);
				}
				value = (contents[i] != null ? contents[i].ToString() : "").PadRight(colSizes[i], ' ');
				output.Append(value);
			}
			output.AppendLine(end.ToString());
			return output.ToString();
		}

		private static string TableLine(char start, char line, char mid, char end, int[] colSizes)
		{
			var output = new StringBuilder();
			output.Append(start);
			for(int i = 0; i < colSizes.Length; i++)
			{
				if(i > 0)
				{
					output.Append(mid);
				}
				output.Append(new string(line, colSizes[i]));
			}
			output.AppendLine(end.ToString());
			return output.ToString();
		}
	}
}
