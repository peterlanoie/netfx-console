using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Console
{
	public class ConsoleChoiceArg
	{
		public string Text { get; set; }
		public ConsoleKey Key { get; set; }

		public ConsoleChoiceArg(string text)
		{
			this.Text = text;
			this.Key = ConsoleKey.NoName;
		}

		public ConsoleChoiceArg(string text, ConsoleKey key) : this(text)
		{
			this.Key = key;
		}

	}

	public class ConsoleChoiceArg<TValue> : ConsoleChoiceArg
	{
		public TValue Value { get; set; }
		public Func<TValue> ProviderFunc { get; set; }

		public ConsoleChoiceArg(string text, ConsoleKey key, TValue value)
			: base(text, key)
		{
			this.Value = value;
		}

		public ConsoleChoiceArg(string text, ConsoleKey key, Func<TValue> providerFunc)
			: base(text, key)
		{
			this.ProviderFunc = providerFunc;
		}

	}

}