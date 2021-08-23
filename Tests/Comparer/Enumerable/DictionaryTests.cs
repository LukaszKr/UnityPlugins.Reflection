using System.Collections.Generic;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class DictionaryTests : AComparerTests
	{
		[Test]
		public void BasicDictionary()
		{
			Dictionary<string, int> left = new Dictionary<string, int>();
			left["a"] = 1;

			Dictionary<string, int> right = new Dictionary<string, int>();
			right["a"] = 2;
			right["key"] = 2;

			ObjectIssue diff = m_Comparer.Compare(left, right);
			TestHelper.AssertDiff(diff, typeof(DifferentLengthIssue), 1);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), 2);
		}
	}
}
