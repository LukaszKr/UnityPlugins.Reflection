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
			Dictionary<string, int> dictA = new Dictionary<string, int>();
			dictA["a"] = 1;

			Dictionary<string, int> dictB = new Dictionary<string, int>();
			dictB["a"] = 2;
			dictB["key"] = 2;

			ObjectIssue diff = m_Comparer.Compare(dictA, dictB);
			TestHelper.AssertDiff(diff, typeof(DifferentLengthIssue), 1);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), 2);
		}
	}
}
