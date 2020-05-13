using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console
{
	public abstract class AsciiLineStyle
	{
		private int _fullInsideWidth;

		internal AsciiLineJustification Justification { get; set; }
		internal int Padding { get; set; }
		internal int FullTextWidth
		{
			set
			{
				_fullInsideWidth = value + Padding * 2;
			}
		}

		internal char TopLtCorner { get { return _topper[0]; } }
		internal char TopHorizntl { get { return _topper[1]; } }
		internal char TopJunction { get { return _topper[2]; } }
		internal char TopRtCorner { get { return _topper[3]; } }

		internal char MidLeftEdge { get { return _middle[0]; } }
		// no middle horizontal, this is a space where content goes
		internal char MidVerticle { get { return _middle[2]; } }
		internal char MidRghtEdge { get { return _middle[3]; } }

		internal char HdrLeftJunc { get { return _hdrsep[0]; } }
		internal char HdrHorizntl { get { return _hdrsep[1]; } }
		internal char HdrJunction { get { return _hdrsep[2]; } }
		internal char HdrRghtJunc { get { return _hdrsep[3]; } }

		internal char LinLeftJunc { get { return _linsep[0]; } }
		internal char LinHorizntl { get { return _linsep[1]; } }
		internal char LinMidJunct { get { return _linsep[2]; } }
		internal char LinRghtJunc { get { return _linsep[3]; } }

		internal char BotLtCorner { get { return _bottom[0]; } }
		internal char BotHorizntl { get { return _bottom[1]; } }
		internal char BotJunction { get { return _bottom[2]; } }
		internal char BotRtCorner { get { return _bottom[3]; } }


		internal char HorizontalRuleChar { get { return HdrHorizntl; } }

		abstract protected string Topper { get; }
		abstract protected string Middle { get; }
		abstract protected string HdrSep { get; }
		abstract protected string LinSep { get; }
		abstract protected string Bottom { get; }

		public static AsciiLineStyle Text { get { return new AsciiLineStyleText(); } }
		public static AsciiLineStyle SolidThin { get { return new AsciiLineStyleSolidThin(); } }
		public static AsciiLineStyle SolidMedium { get { return new AsciiLineStyleSolidMedium(); } }
		public static AsciiLineStyle SolidThick { get { return new AsciiLineStyleSolidThick(); } }
		public static AsciiLineStyle DoubleLine { get { return new AsciiLineStyleDoubleLine(); } }
		public static AsciiLineStyle DoubleHorizontalLine { get { return new AsciiLineStyleDoubleHorizontalLine(); } }
		public static AsciiLineStyle DoubleVerticalLine { get { return new AsciiLineStyleDoubleVerticalLine(); } }
		public static AsciiLineStyle ShadeLight { get { return new AsciiLineStyleShadeLight(); } }
		public static AsciiLineStyle ShadeMedium { get { return new AsciiLineStyleShadeMedium(); } }
		public static AsciiLineStyle ShadeHeavy { get { return new AsciiLineStyleShadeHeavy(); } }
		public static AsciiLineStyle DoubleHeaderLine { get { return new AsciiLineStyleDoubleHeaderLine(); } }
		public static AsciiLineStyle DoubleSeparatorLine { get { return new AsciiLineStyleDoubleSeparatorLine(); } }

		private string _topper, _middle, _hdrsep, _linsep, _bottom;

		public AsciiLineStyle()
		{
			_topper = Topper;
			_middle = Middle;
			_hdrsep = HdrSep;
			_linsep = LinSep;
			_bottom = Bottom;
		}

		internal void SetOptions(AsciiBoxOptions options)
		{
			Justification = options.Justify;
			Padding = options.Padding;
		}

		internal string GetTextLine(string text)
		{
			return GetTextLine(text, Justification);
		}

		internal string GetTextLine(string text, AsciiLineJustification justification)
		{
			int txtLength = text.Length;
			int totalPad = _fullInsideWidth - txtLength;
			int ltPad, rtPad;
			ltPad = rtPad = 0;
			if(totalPad > 0)
			{
				switch(justification)
				{
					case AsciiLineJustification.Left:
						ltPad = Padding;
						rtPad = totalPad - Padding;
						break;
					case AsciiLineJustification.Center:
						ltPad = totalPad / 2;
						rtPad = totalPad - ltPad;
						break;
					case AsciiLineJustification.Right:
						ltPad = totalPad - Padding;
						rtPad = Padding;
						break;
				}
			}
			return string.Format("{0}{1}{2}{3}{4}", MidLeftEdge, new string(' ', ltPad), text, new string(' ', rtPad), MidRghtEdge);
		}

		internal string GetSeparatorLine()
		{
			return GetLine(LinLeftJunc, LinHorizntl, LinRghtJunc);
		}

		internal string GetTopLine()
		{
			return GetLine(TopLtCorner, TopHorizntl, TopRtCorner);
		}

		internal string GetBotLine()
		{
			return GetLine(BotLtCorner, BotHorizntl, BotRtCorner);
		}

		private string GetLine(char left, char mid, char right)
		{
			return string.Format("{0}{1}{2}", left, new string(mid, _fullInsideWidth), right);
		}

	}

	internal class AsciiLineStyleText : AsciiLineStyle
	{
		internal AsciiLineStyleText() { }
		protected override string Topper { get { return "+-++"; } }
		protected override string Middle { get { return "| ||"; } }
		protected override string HdrSep { get { return "+-++"; } }
		protected override string LinSep { get { return "+-++"; } }
		protected override string Bottom { get { return "+-++"; } }
	}


	internal class AsciiLineStyleSolidThin : AsciiLineStyle
	{
		internal AsciiLineStyleSolidThin() { }
		protected override string Topper { get { return "┌─┬┐"; } }
		protected override string Middle { get { return "│ ││"; } }
		protected override string HdrSep { get { return "├─┼┤"; } }
		protected override string LinSep { get { return "├─┼┤"; } }
		protected override string Bottom { get { return "└─┴┘"; } }
	}

	internal class AsciiLineStyleSolidMedium : AsciiLineStyle
	{
		internal AsciiLineStyleSolidMedium() { }
		protected override string Topper { get { return "▄▄▄▄"; } }
		protected override string Middle { get { return "▌  ▐"; } }
		protected override string HdrSep { get { return "████"; } }
		protected override string LinSep { get { return "████"; } }
		protected override string Bottom { get { return "▀▀▀▀"; } }
	}

	internal class AsciiLineStyleSolidThick : AsciiLineStyle
	{
		internal AsciiLineStyleSolidThick() { }
		protected override string Topper { get { return "████"; } }
		protected override string Middle { get { return "█  █"; } }
		protected override string HdrSep { get { return "████"; } }
		protected override string LinSep { get { return "████"; } }
		protected override string Bottom { get { return "████"; } }
	}

	internal class AsciiLineStyleDoubleLine : AsciiLineStyle
	{
		internal AsciiLineStyleDoubleLine() { }
		protected override string Topper { get { return "╔═╦╗"; } }
		protected override string Middle { get { return "║ ║║"; } }
		protected override string HdrSep { get { return "╠═╬╣"; } }
		protected override string LinSep { get { return "╠═╬╣"; } }
		protected override string Bottom { get { return "╚═╩╝"; } }
	}

	internal class AsciiLineStyleDoubleHorizontalLine : AsciiLineStyle
	{
		internal AsciiLineStyleDoubleHorizontalLine() { }
		protected override string Topper { get { return "╒═╤╕"; } }
		protected override string Middle { get { return "│ ││"; } }
		protected override string HdrSep { get { return "╞═╪╡"; } }
		protected override string LinSep { get { return "╞═╪╡"; } }
		protected override string Bottom { get { return "╘═╧╛"; } }
	}

	internal class AsciiLineStyleDoubleVerticalLine : AsciiLineStyle
	{
		internal AsciiLineStyleDoubleVerticalLine() { }
		protected override string Topper { get { return "╓─╥╖"; } }
		protected override string Middle { get { return "║ ║║"; } }
		protected override string HdrSep { get { return "╟─╫╢"; } }
		protected override string LinSep { get { return "╟─╫╢"; } }
		protected override string Bottom { get { return "╙─╨╜"; } }
	}

	internal class AsciiLineStyleDoubleHeaderLine : AsciiLineStyle
	{
		internal AsciiLineStyleDoubleHeaderLine() { }
		protected override string Topper { get { return "┌─┬┐"; } }
		protected override string Middle { get { return "│ ││"; } }
		protected override string HdrSep { get { return "╞═╪╡"; } }
		protected override string LinSep { get { return "├─┼┤"; } }
		protected override string Bottom { get { return "└─┴┘"; } }
	}

	internal class AsciiLineStyleDoubleSeparatorLine : AsciiLineStyle
	{
		internal AsciiLineStyleDoubleSeparatorLine() { }
		protected override string Topper { get { return "┌─┬┐"; } }
		protected override string Middle { get { return "│ ││"; } }
		protected override string HdrSep { get { return "├─┼┤"; } }
		protected override string LinSep { get { return "╞═╪╡"; } }
		protected override string Bottom { get { return "└─┴┘"; } }
	}


	internal class AsciiLineStyleShadeLight : AsciiLineStyle
	{
		internal AsciiLineStyleShadeLight() { }
		protected override string Topper { get { return "░░░░"; } }
		protected override string Middle { get { return "░ ░░"; } }
		protected override string HdrSep { get { return "░░░░"; } }
		protected override string LinSep { get { return "░░░░"; } }
		protected override string Bottom { get { return "░░░░"; } }
	}

	internal class AsciiLineStyleShadeMedium : AsciiLineStyle
	{
		internal AsciiLineStyleShadeMedium() { }
		protected override string Topper { get { return "▒▒▒▒"; } }
		protected override string Middle { get { return "▒ ▒▒"; } }
		protected override string HdrSep { get { return "▒▒▒▒"; } }
		protected override string LinSep { get { return "▒▒▒▒"; } }
		protected override string Bottom { get { return "▒▒▒▒"; } }
	}

	internal class AsciiLineStyleShadeHeavy : AsciiLineStyle
	{
		internal AsciiLineStyleShadeHeavy() { }
		protected override string Topper { get { return "▓▓▓▓"; } }
		protected override string Middle { get { return "▓ ▓▓"; } }
		protected override string HdrSep { get { return "▓▓▓▓"; } }
		protected override string LinSep { get { return "▓▓▓▓"; } }
		protected override string Bottom { get { return "▓▓▓▓"; } }
	}

	public enum AsciiLineJustification
	{
		Left,
		Center,
		Right,
	}

}
