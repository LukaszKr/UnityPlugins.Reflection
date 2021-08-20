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
			List<int> listA = new List<int>() { 1 };
			List<int> listB = new List<int>() { 2, 2 };

			ObjectIssue diff = m_Comparer.Compare(listA, listB);
			TestHelper.AssertDiff(diff, typeof(DifferentLengthIssue), typeof(DifferentValueIssue));
		}
	}
}
