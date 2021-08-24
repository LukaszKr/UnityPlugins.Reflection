using System.Collections.Generic;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Tests.Comparer
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
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentLengthIssue), 1);
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue), 2);
		}
	}
}
