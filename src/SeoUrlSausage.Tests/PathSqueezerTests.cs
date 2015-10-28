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
		public static string[] ReservedChars
		{
			get { return PathSqueezer.RESERVED_CHARS; }
		}

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
		public void should_replace_reserved_characters_combined_with_spaces()
		{
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string title = "this is my title?!! /r/science/ and #firstworldproblems :* :sadface=true";

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			string expectedPath = "this-is-my-title-rscience-and-firstworldproblems-sadfacetrue";

			Assert.That(actualPath, Is.EqualTo(expectedPath));
		}

		[Test]
		public void should_remove_multiple_dashes()
		{
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string title = "one-two-three--four--five and a six--seven--eight-nine------ten";

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			string expectedPath = "onetwothreefourfive-and-a-sixseveneightnineten";

			Assert.That(actualPath, Is.EqualTo(expectedPath));
		}

		[Test]
		[TestCaseSource("ReservedChars")]
		public void should_remove_dashes(string character)
		{
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string manyCharactersWow = new String(character[0], 10);
            string title = string.Format("testing {0} some of {0} these {0}", manyCharactersWow);

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			string expectedPath = "testing-some-of-these-";

			Assert.That(actualPath, Is.EqualTo(expectedPath));
		}

		[Test]
		public void should_ignore_empty_or_null_string()
		{
			// Arrange
			var urlSqueezer = new PathSqueezer();
			string title = null;

			// Act
			string actualPath = urlSqueezer.Squeeze(title);

			// Assert
			Assert.That(actualPath, Is.EqualTo(""));
		}
	}
}
