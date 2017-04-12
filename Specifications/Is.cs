using System;
using System.Linq;
using System.Linq.Expressions;

namespace Specifications
{
	public static class Is
	{
		public static ISpecification<string> StringEqual
		{
			get { return new ConditionSpecification<string>((expected, actual) => expected.Equals(actual)); }
		}

		public static ISpecification<string> StartsWithSameCharacter
		{
			get { return new ConditionSpecification<string>((expected, actual) => expected[0] == actual[0]); }
		}

		public static ISpecification<string> ExpectedContainsActualCharacters
		{
			get { return new ConditionSpecification<string>((expected, actual) => ComplexCheck(expected, actual)); }
		}

		private static bool ComplexCheck(string a, string b)
		{
			var aChars = a.Select(x => x).Distinct();
			return b.All(x => aChars.Contains(x));
		}
	}
}
