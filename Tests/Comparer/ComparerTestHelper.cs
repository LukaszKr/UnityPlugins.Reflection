using System;
using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;

namespace ProceduralLevel.Reflection.Tests.Comparer
{
	[Category(ReflectionTestsConsts.CATEGORY)]
	public static class ComparerTestHelper
	{
		public static void AssertNoDiff(ObjectIssue diff)
		{
			Assert.IsNull(diff);
		}

		public static void AssertDiff(ObjectIssue diff, Type type, int expectedCount)
		{
			int length = diff.Issues.Count;
			int count = 0;
			for(int y = 0; y < length; ++y)
			{
				if(diff.Issues[y].GetType() == type)
				{
					++count;
				}
			}
			if(count != expectedCount)
			{
				Assert.Fail($"Diff count of type '{type.Name}' doesn't match. {expectedCount} =/= {count}");
			}
		}

		public static void AssertDiff(ObjectIssue diff, params Type[] expectedDiffs)
		{
			int length = expectedDiffs.Length;
			int diffCount = diff.Issues.Count;
			Assert.AreEqual(length, diffCount);
			for(int x = 0; x < length; ++x)
			{
				Type type = expectedDiffs[x];
				AssertDiff(diff, type, 1);
			}
		}
	}
}
