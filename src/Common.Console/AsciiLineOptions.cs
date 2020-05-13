using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console
{
	/// <summary>
	/// Defines the options used for drawing simple ASCII lines.
	/// </summary>
	public class AsciiLineOptions
	{

		/// <summary>
		/// Line style.
		/// </summary>
		public AsciiLineStyle Style { get; set; }

		public AsciiLineOptions()
		{
			Style = AsciiLineStyle.SolidThin;
		}

	}
}
