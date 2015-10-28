using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeoUrlSausage
{
	public class PathSqueezer
	{
		public static readonly string[] RESERVED_CHARS = new string[] 
		{
			"-", ".", "_", "~", ":", "/", "?", "#", "[", "]",
			"@", "!", "$", "&", "'", "(", ")", "*", "+", ",",
			";", "=", "}", ";"
		};

		public string Squeeze(string title)
		{
			if (string.IsNullOrEmpty(title))
				return "";

			string path = title;

			// Remove reserved chars - doing this as an array reads a lot better than a regex
			foreach (string token in RESERVED_CHARS)
			{
				path = path.Replace(token, "");
			}

			// Remove multiple spaces
			path = Regex.Replace(path, @"\s+", " ");

			// Turn spaces into dashes
			path = path.Replace(" ", "-");

			// Grab letters and numbers only
			var regex = new Regex("^([a-zA-Z0-9])+$");
			if (regex.IsMatch(path))
			{
				path = regex.Matches(path)[0].Value;
			}

			return path;
		}
	}
}
