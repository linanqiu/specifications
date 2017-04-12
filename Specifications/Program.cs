using System;

namespace Specifications
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var expected1 = "hello";
			var actual1 = "hello";

			var check1 = Is.StringEqual;

			Console.WriteLine(check1.That(expected1, actual1));

			var expected2 = "hello world";
			var actual2 = "hello";

			var check2 = Is.ExpectedContainsActualCharacters
						   .And(Is.StartsWithSameCharacter);

			Console.WriteLine(check2.That(expected2, actual2));

			var check3 = Is.StringEqual
						   .And(Is.StartsWithSameCharacter);

			Console.WriteLine(check3.That(expected2, actual2));
		}
	}
}
