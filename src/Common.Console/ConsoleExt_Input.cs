using System;
using System.Linq;
using Sys = System;
using System.Collections.Generic;

namespace Common.Console
{
	public static partial class ConsoleExt
	{

		public static bool Confirm(string prompt)
		{
			return ConsoleExt.GetChoice(string.Format("{0} [Y/N] > ", prompt), ConsoleKey.Y, ConsoleKey.N) == ConsoleKey.Y;
		}

		public static string GetInput(string prompt)
		{
			string input;
			Sys.Console.Write(prompt);
			input = Sys.Console.ReadLine();
			Sys.Console.WriteLine();
			return input;
		}

		/// <summary>
		/// Prompts the user to choose one of the items in the <paramref name="choices"/> list.
		/// Entering 'exit' or 0 will exit and return a null.
		/// </summary>
		/// <param name="prompt">The user prompt before the list.</param>
		/// <param name="waitPrompt">The user prompt after the list, before the input.</param>
		/// <param name="choices">Array of choices from which to select.</param>
		/// <returns></returns>
		public static string GetChoice(string prompt, string waitPrompt, params string[] choices)
		{

			Sys.Console.WriteLine(prompt);
			for(int i = 0; i < choices.Length; i++)
			{
				Sys.Console.WriteLine("   ({0}) {1}", i + 1, choices[i]);
			}
			var choiceNum = GetChoiceNumber(waitPrompt, 1, choices.Length + 1);
			return choiceNum != null ? choices[((int)choiceNum - 1)] : null;
		}

		private static int? GetChoiceNumber(string waitPrompt, int min, int max)
		{
			string inputStr;
			int inputInt;

			while(true)
			{
				Sys.Console.Write(waitPrompt);
				inputStr = Sys.Console.ReadLine();
				if(int.TryParse(inputStr, out inputInt))
				{
					if(inputInt >= min && inputInt <= max)
					{
						return inputInt;
					}
					System.Console.Write("Input was outsize the range of possible selections");
				}
				else
				{
					if(inputStr.ToLower() == "exit")
					{
						return null;
					}
					System.Console.Write("Input wasn't valid");
				}
				System.Console.WriteLine(", try again.");
			}
		}

		public static T GetChoice<T>(string prompt, IEnumerable<T> choiceItems, Func<T, string> labelSelector)
		{
			return GetChoice(prompt, " > ", choiceItems, labelSelector);
		}

		public static T GetChoice<T>(string prompt, string waitPrompt, IEnumerable<T> choiceItems, Func<T, string> labelSelector)
		{
			Sys.Console.WriteLine(prompt);
			var items = choiceItems.ToList();
			for(int i = 0; i < items.Count(); i++)
			{
				Sys.Console.WriteLine("   {0}: {1}", i + 1, labelSelector(items[i]));
			}
			var choiceNum = GetChoiceNumber(waitPrompt, 0 + 1, items.Count);
			return choiceNum == null ? default(T) : items[((int)choiceNum - 1)];
		}

		public static ConsoleKey GetChoice(string prompt, params ConsoleChoiceArg[] choices)
		{
			return GetChoice(prompt, " > ", choices);
		}

		public static ConsoleKey GetChoice(string prompt, string waitPrompt, params ConsoleChoiceArg[] choices)
		{
			return GetChoice(prompt, waitPrompt, choices.Select(x => new ConsoleChoiceArg<ConsoleKey>(x.Text, x.Key, x.Key)).ToArray());
		}

		public static TValue GetChoice<TValue>(string prompt, params ConsoleChoiceArg<TValue>[] choices)
		{
			return GetChoice(prompt, "> ", choices);
		}

		public static TValue GetChoice<TValue>(string prompt, string waitPrompt, params ConsoleChoiceArg<TValue>[] choices)
		{
			ConsoleKeyInfo keyInput;
			ConsoleChoiceArg<TValue> choiceMatch;

			var dupeKeys = (from c in choices
							group c by c.Key
								into g
								select new { g.Key, Count = g.Count() }).Where(x => x.Count > 1);
			if(dupeKeys.Count() > 0)
			{
				throw new InvalidOperationException("GetChoice call specifies one or more options with the same key(s).");
			}

			Sys.Console.WriteLine(prompt);
			foreach(var item in choices)
			{
				Sys.Console.WriteLine("   {0}", item.Text);
			}
			Sys.Console.Write(waitPrompt);

			while(true)
			{
				keyInput = Sys.Console.ReadKey(true);
				choiceMatch = choices.SingleOrDefault(c => c.Key == keyInput.Key);
				if(choiceMatch != null)
				{
					break;
				}
			}
			Sys.Console.WriteLine(keyInput.KeyChar);

			if(choiceMatch.ProviderFunc != null)
			{
				return choiceMatch.ProviderFunc();
			}
			return choiceMatch.Value;
		}

		public static ConsoleKey GetChoice(params ConsoleKey[] consoleKeys)
		{
			return GetChoice(null, null, consoleKeys);
		}

		public static ConsoleKey GetChoice(string prompt, params ConsoleKey[] consoleKeys)
		{
			return GetChoice(prompt, null, consoleKeys);
		}

		/// <summary>
		/// Prompts the user to make a yes/no choice. Uses the Y and N keys. Y response == true;
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		public static bool GetBoolChoice(string prompt)
		{
			return ConsoleExt.GetChoice(prompt, ConsoleKey.Y, ConsoleKey.N) == ConsoleKey.Y;
		}

		public static ConsoleKey GetChoice(string prompt, string waitprompt, params ConsoleKey[] consoleKeys)
		{
			ConsoleKeyInfo keyInput;
			if(prompt != null)
			{
				Sys.Console.Write(prompt);
				if(waitprompt != null)
				{
					Sys.Console.WriteLine();
				}
			}
			if(waitprompt != null)
			{
				Sys.Console.Write(waitprompt);
			}
			while(true)
			{
				keyInput = Sys.Console.ReadKey(true);
				if(consoleKeys.Contains(keyInput.Key))
				{
					Sys.Console.WriteLine(keyInput.KeyChar);
					return keyInput.Key;
				}
			}
		}

		public static int GetIntInput(string prompt)
		{
			int result;
			do
			{
				if(int.TryParse(GetInput(prompt), out result)) break;
				System.Console.WriteLine("  >> Input not valid, please try again <<");
			} while (true);
			return result;
		}

/*
		public static ConsoleKey GetChoiceKey(ChoiceBaseOptions options)
		{

		}

		public static T GetChoiceItem<T>(ChoiceBaseOptions options)
		{

		}

		private static ConsoleChoiceArg GetChoiceItem(ChoiceBaseOptions options)
		{
			ConsoleKeyInfo keyInput;
			if(options.Prompt != null)
			{
				Sys.Console.Write(options.Prompt);
				if(options.WaitPrompt != null)
				{
					Sys.Console.WriteLine();
				}
			}

			options.ValidateChoices();

			var choices = options.GetChoices();


			if(options.WaitPrompt != null)
			{
				Sys.Console.Write(options.WaitPrompt);
			}
			while(true)
			{
				keyInput = Sys.Console.ReadKey(true);
				//				if(consoleKeys.Contains(keyInput.Key))
				{
					Sys.Console.WriteLine(keyInput.KeyChar);
					return keyInput.Key;
				}
			}


		}

		private static bool ConsoleKeysAreUnique(IEnumerable<ConsoleKey> keys)
		{
			var dupeKeys = (from c in keys
							group c by c
								into g
								select new { g.Key, Count = g.Count() })
							.Where(x => x.Count > 1);
			if(dupeKeys.Count() > 0)
			{
				throw new InvalidOperationException("ConsoleKeys specified include duplicates.");
			}
			return true;
		}

		public abstract class ChoiceBaseOptions
		{
			/// <summary>
			/// The prompt to present before the choices
			/// </summary>
			public string Prompt { get; set; }

			/// <summary>
			/// The prompt to present before user input
			/// </summary>
			public string WaitPrompt { get; set; }

			/// <summary>
			/// The default choice to select when nothing is entered.
			/// </summary>
			public object Default { get; set; }

			internal abstract bool ValidateChoices();

			internal abstract ConsoleChoiceArg[] GetChoices();

			internal abstract bool ShowChoiceLines { get; }

			internal abstract bool GenerateInputs { get; }
		}

		public class ChoiceKeyOptions : ChoiceBaseOptions
		{
			/// <summary>
			/// Array of console keys to accept as valid choices
			/// </summary>
			public ConsoleKey[] ConsoleKeysAccepted { get; set; }

			internal override bool ValidateChoices()
			{
				return ConsoleKeysAreUnique(ConsoleKeysAccepted);
			}

			internal override ConsoleChoiceArg[] GetChoices()
			{
				return ConsoleKeysAccepted.Select(x => new ConsoleChoiceArg(null, x)).ToArray();
			}

			internal override bool ShowChoiceLines
			{
				get { return false; }
			}

			internal override bool GenerateInputs
			{
				get { return false; }
			}
		}

		public class ChoiceArgOptions : ChoiceBaseOptions
		{
			/// <summary>
			/// Array of structured choice arguments to present the user
			/// </summary>
			public ConsoleChoiceArg[] ChoiceArgs { get; set; }

			internal override bool ValidateChoices()
			{
				return ConsoleKeysAreUnique(ChoiceArgs.Select(x => x.Key));
			}

			internal override ConsoleChoiceArg[] GetChoices()
			{
				return ChoiceArgs;
			}

			internal override bool ShowChoiceLines
			{
				get { return true; }
			}

			internal override bool GenerateInputs
			{
				get { return false; }
			}
		}

		public class ChoiceArgItemOptions<T> : ChoiceBaseOptions
		{
			/// <summary>
			/// Array of choice items
			/// </summary>
			public ConsoleChoiceArg<T>[] ChoiceArgItems { get; set; }

			internal override bool ValidateChoices()
			{
				return ConsoleKeysAreUnique(ChoiceArgItems.Select(x => x.Key));
			}

			internal override ConsoleChoiceArg[] GetChoices()
			{
				return ChoiceArgItems.Select(x => new ConsoleChoiceArg(x.Text, x.Key)).ToArray();
			}

			internal override bool ShowChoiceLines
			{
				get { return true; }
			}

			internal override bool GenerateInputs
			{
				get { return false; }
			}
		}

		public class ChoiceItemOptions<T> : ChoiceBaseOptions
		{
			public IEnumerable<T> ChoiceItems { get; set; }
			public Func<T, string> LabelSelector { get; set; }
			public Func<T, bool> IsDefaultPredicate { get; set; }

			internal override bool ValidateChoices()
			{
				throw new NotImplementedException();
			}

			internal override ConsoleChoiceArg[] GetChoices()
			{
				return ChoiceItems.Select(x => new ConsoleChoiceArg(LabelSelector(x))).ToArray();
			}

			internal override bool ShowChoiceLines
			{
				get { return true; }
			}

			internal override bool GenerateInputs
			{
				get { return true; }
			}
		}
*/
	}
}
