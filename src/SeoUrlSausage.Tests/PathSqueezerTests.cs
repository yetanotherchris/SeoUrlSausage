using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SeoUrlSausage.Tests
{
	public class PathSqueezerTests
	{
		[Test]
		public void should_remove_non_letters_and_digits()
		{
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string title = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~:/?#[]@!$&'()*+,;=";

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			string expectedPath = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

			Assert.That(actualPath, Is.EqualTo(expectedPath));
		}

		[Test]
		public void should_not_remove_non_ascii_characters()
		{
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string title = "विकी";

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			string expectedPath = "Děkujemeविकीвики-движка";

			Assert.That(actualPath, Is.EqualTo(title));
		}

		[Test]
		public void should_replace_whitespace_with_dashes_and_clean_dashes_and_punctuation()
		{ 
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string title = "this is my title - and some \t\t\t\t\n   clever; (piece) of text here: [ok].";

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			string expectedPath = "this-is-my-title-and-some-clever-piece-of-text-here-ok";

			Assert.That(actualPath, Is.EqualTo(expectedPath));
		}

		[Test]
		public void should_replace_reserved_characters()
		{
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string title = "this is my title? and #firstworldproblems :* :sadface=true";

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			string expectedPath = "this-is-my-title-and-firstworldproblems-sadfacetrue";

			Assert.That(actualPath, Is.EqualTo(expectedPath));
		}
	}
}
