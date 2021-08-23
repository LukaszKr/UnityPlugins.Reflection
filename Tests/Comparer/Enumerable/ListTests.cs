using System.Collections.Generic;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class ListTests : AComparerTests
	{
		[Test]
		public void ListOfInts()
		{
			List<int> left = new List<int>() { 1 };
			List<int> right = new List<int>() { 2, 2 };

			ObjectIssue diff = m_Comparer.Compare(left, right);
			TestHelper.AssertDiff(diff, typeof(DifferentLengthIssue), typeof(DifferentValueIssue));
		}
	}
}
