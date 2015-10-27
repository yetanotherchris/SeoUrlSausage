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
		public string Squeeze(string title)
		{
			// Remove reserved chars - this reads a lot better than a regex
			string[] reserved =
			{
				"-", ".", "_", "~", ":", "/", "?", "#", "[", "]",
				"@", "!", "$", "&", "'", "(", ")", "*", "+", ",",
				";", "=", "}", ";"
			};
			foreach (string token in reserved)
			{
				title = title.Replace(token, "");
			}

			// Remove multiple spaces
			title = Regex.Replace(title, @"\s+", " ");
			var builder = new StringBuilder();

			// These checks looks convulated but are needed for Unicode characters that 
			// aren't in the BMP (Basic Multilingual Plane)
			foreach (char t in title)
			{
				if (!Char.IsPunctuation(t) && !Char.IsSeparator(t) && !Char.IsSymbol(t) && !Char.IsControl(t))
				{
					builder.Append(t);
				}
				else if (Char.IsWhiteSpace(t))
				{
					builder.Append("-");
				}
			}

			return builder.ToString();
		}
	}
}
