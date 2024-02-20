using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;

namespace ProceduralLevel.Reflection.Tests.Comparer
{
	[Category(ReflectionTestsConsts.CATEGORY)]
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


		[Test]
		public void SameDictionaries()
		{
			Dictionary<int, int> left = new Dictionary<int, int>();
			Dictionary<int, int> right = new Dictionary<int, int>();

			Random rand = new Random(12345);
			for(int x = 0; x < 10; ++x)
			{
				int key = rand.Next();
				int value = rand.Next();
				left[key] = value;
				right[key] = value;
			}

			ObjectIssue diff = m_Comparer.Compare(left, right);
			Assert.IsNull(diff);
		}
	}
}
