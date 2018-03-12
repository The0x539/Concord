using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Concord {
	public class ListItem : IComparable {
		public ListItem(IStorageItem item) {
			Name = item.Name;
			StorageItem = item;
		}

		public static readonly Dictionary<string, string[]> groups = new Dictionary<string, string[]> {
			["star wars"] = new string[] {
				"the phantom menace",
				"attack of the clones",
				"revenge of the sith",
				"a new hope",
				"the empire strikes back",
				"return of the jedi",
				"the force awakens",
				"the last jedi",
				"rogue one",
				"solo"
			},
			["tron"] = new string[] {" legacy"},
			["blade runner"] = new string[] {" 2049"},
			["pirates of the caribbean"] = new string[] {
				"the curse of the black pearl",
				"dead man's chest",
				"at world's end",
				"on stranger tides",
				"dead men tell no tales"
			},
			["monsters"] = new string[] {
				", inc.",
				" university"
			},/*
			["jojo's bizarre adventure"] = new string[] {
				"phantom blood",
				"battle tendency",
				"stardust crusaders",
				"diamond is unbreakable",
				"vento aureo",
				"stone ocean",
				"steel ball run",
				"jojolion",
				"thus spoke kishibe rohan"
			}*/
		};

		public string Name { get; private set; }
		public IStorageItem StorageItem { get; }

		public bool IsFolder => StorageItem is StorageFolder;

		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		private static extern int StrCmpLogicalW(string psz1, string psz2);

		public int CompareTo(object that) {
			if (!(that is ListItem))
				return 0;

			string a = Name;
			string b = ((ListItem) that).Name;

			a = Roman.Replace(a);
			b = Roman.Replace(b);

			a = a.ToLower();
			b = b.ToLower();

			foreach (KeyValuePair<string, string[]> pair in groups) {
				string series = pair.Key;
				string[] entries = pair.Value;
				if (a.StartsWith(series) && b.StartsWith(series)) {
					for (int i = 0; i < entries.Length; i++) {
						string entry = entries[i];
						a = a.Replace(entry, i.ToString());
						b = b.Replace(entry, i.ToString());
					}
					break;
				}
			}
			if (a.StartsWith("the ")) {
				a = a.Substring(4);
			}
			if (b.StartsWith("the ")) {
				b = b.Substring(4);
			}

			return StrCmpLogicalW(a, b);
		}
	}
}
