using System.Runtime.InteropServices;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class FilterTests : AComparerTests
	{
		private class TestClass
		{
			public int Value;

			public TestClass(int value)
			{
				Value = value;
			}
		}

		[Test]
		public void IgnoreType()
		{
			m_Comparer.IgnoreType(typeof(int));
			ObjectIssue diff = m_Comparer.Compare(1, 2);

			Assert.IsNull(diff);
		}

		[Test]
		public void TypeCheckHappensBeforeFilters()
		{
			m_Comparer.IgnoreType(typeof(int));

			ObjectIssue diff = m_Comparer.Compare(1, "test");
			TestHelper.AssertDiff(diff, typeof(DifferentTypeIssue));
		}

		[Test]
		public void IgnoreByName()
		{
			TestClass classA = new TestClass(1);
			TestClass classB = new TestClass(2);
			m_Comparer.IgnoreMember<TestClass>(nameof(classA.Value));

			ObjectIssue diff = m_Comparer.Compare(classA, classB);
			Assert.IsNull(diff);
		}
	}
}
