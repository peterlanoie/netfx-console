using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Console.UI;
using Common.Console.Tests.CLI.Menus;
using System.Threading;

namespace Common.Console.Tests.CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			//MenuItemRunner.RunMenuItem(args, new MainMenu());
			//RenderTables();
			while (false)
			{
				System.Console.WriteLine($"BufferHeight: {System.Console.BufferHeight}");
				System.Console.WriteLine($"BufferWidth: {System.Console.BufferWidth}");
				System.Console.WriteLine($"CursorLeft: {System.Console.CursorLeft}");
				System.Console.WriteLine($"CursorTop: {System.Console.CursorTop}");
				System.Console.WriteLine($"LargestWindowHeight: {System.Console.LargestWindowHeight}");
				System.Console.WriteLine($"LargestWindowWidth: {System.Console.LargestWindowWidth}");
				System.Console.WriteLine($"WindowHeight: {System.Console.WindowHeight}");
				System.Console.WriteLine($"WindowLeft: {System.Console.WindowLeft}");
				System.Console.WriteLine($"WindowTop: {System.Console.WindowTop}");
				System.Console.WriteLine($"WindowWidth: {System.Console.WindowWidth}");
				Thread.Sleep(2000);
			}

			System.Console.SetCursorPosition(10, 10);
			System.Console.WriteLine("cheese");

		}

		private static void RenderTables()
		{
			var headers = new[] { "Column 1", "Column 2", "Column 3" };
			var options = new AsciiTableOptions { Title = "Sample Table Content" };
			var content = new List<object[]>();

			var rand = new Random();
			int value() => rand.Next(int.MinValue, int.MaxValue);

			for (int i = 0; i < 20; i++)
			{
				content.Add(new object[] { value(), value(), value() });
			}

			options.Style = AsciiLineStyle.DoubleHeaderLine;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.DoubleHorizontalLine;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.DoubleLine;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.DoubleSeparatorLine;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.DoubleVerticalLine;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.ShadeHeavy;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.ShadeLight;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.ShadeMedium;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.SolidMedium;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.SolidThick;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
			options.Style = AsciiLineStyle.SolidThin;
			System.Console.WriteLine(ConsoleExt.AsciiTable(headers, content, options));
		}
	}
}
