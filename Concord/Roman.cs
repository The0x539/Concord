using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace Concord {
	public static class Roman {
		public static readonly Dictionary<char, int> RomanNumberDictionary;
		public static readonly Dictionary<int, string> NumberRomanDictionary;
		public static readonly Regex pattern;

		static Roman() {
			RomanNumberDictionary = new Dictionary<char, int> {
				{ 'I', 1 },
				{ 'V', 5 },
				{ 'X', 10 },
				{ 'L', 50 },
				{ 'C', 100 },
				{ 'D', 500 },
				{ 'M', 1000 },
			};

			NumberRomanDictionary = new Dictionary<int, string> {
				{ 1000, "M" },
				{ 900, "CM" },
				{ 500, "D" },
				{ 400, "CD" },
				{ 100, "C" },
				{ 50, "L" },
				{ 40, "XL" },
				{ 10, "X" },
				{ 9, "IX" },
				{ 5, "V" },
				{ 4, "IV" },
				{ 1, "I" },
			};

			pattern = new Regex("(^|\\W)(M{1,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})|M{0,4}(CM|C?D|D?C{1,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})|M{0,4}(CM|CD|D?C{0,3})(XC|X?L|L?X{1,3})(IX|IV|V?I{0,3})|M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|I?V|V?I{1,3}))($|\\W)");
		}

		public static string To(int number) {
			var roman = new StringBuilder();

			foreach (var item in NumberRomanDictionary) {
				while (number >= item.Key) {
					roman.Append(item.Value);
					number -= item.Key;
				}
			}

			return roman.ToString();
		}

		public static int From(string roman) {
			int total = 0;

			int current, previous = 0;
			char currentRoman, previousRoman = '\0';

			for (int i = 0; i < roman.Length; i++) {
				currentRoman = roman[i];

				previous = previousRoman != '\0' ? RomanNumberDictionary[previousRoman] : '\0';
				current = RomanNumberDictionary[currentRoman];

				if (previous != 0 && current > previous) {
					total = total - (2 * previous) + current;
				} else {
					total += current;
				}

				previousRoman = currentRoman;
			}

			return total;
		}

		public static string Replace(string text) {
			string[] foo = pattern.Matches(text).Cast<Match>().Select(m => m.Value).ToArray();
			foreach (string x in foo) {
				string y = x.Substring(1, x.Length - 2);
				text = text.Replace(y, From(y).ToString());
			}
			return text;
		}
	}
}
