using System;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public static class TestHelper
	{
		public static void AssertDiff(ObjectDifference diff, params Type[] expectedDiffs)
		{
			int length = expectedDiffs.Length;
			int diffCount = diff.Differences.Count;
			Assert.AreEqual(length, diffCount);
			for(int x = 0; x < length; ++x)
			{
				Type type = expectedDiffs[x];
				bool found = false;
				for(int y = 0; y < length; ++y)
				{
					if(diff.Differences[y].GetType() == type)
					{
						found = true;
						break;
					}
				}
				if(!found)
				{
					Assert.Fail($"Diff of type '{type.Name}' was not found.");
				}
			}
		}
	}
}
