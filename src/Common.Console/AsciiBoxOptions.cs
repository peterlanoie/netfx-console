using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console
{
	/// <summary>
	/// Defines the options used for drawing simple ASCII boxes.
	/// </summary>
	public class AsciiBoxOptions : AsciiLineOptions
	{

		/// <summary>
		/// Text justification of lines.
		/// </summary>
		public AsciiLineJustification Justify { get; set; }

		/// <summary>
		/// Justification of the menu header.
		/// </summary>
		public AsciiLineJustification HeaderJustification { get; set; }

		/// <summary>
		/// The padding between the text and box border.
		/// </summary>
		public int Padding { get; set; }

		public AsciiBoxOptions()
		{
			Padding = 1;
			HeaderJustification = AsciiLineJustification.Center;
			Justify = AsciiLineJustification.Center;
		}
	}

}
