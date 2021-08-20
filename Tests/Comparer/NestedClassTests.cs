using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class NestedClassTests : AComparerTests
	{
		private class RootClass
		{
			public object Value;
			public NestedClass Nested = new NestedClass();
		}

		private class NestedClass
		{
			public object Value;
		}

		private RootClass m_RootA;
		private RootClass m_RootB;

		[SetUp]
		public void Setup()
		{
			m_RootA = new RootClass();
			m_RootB = new RootClass();
		}

		[Test]
		public void CanCompareNestedClasses()
		{
			ObjectDifference diff = m_Comparer.Compare(m_RootA, m_RootB);
			Assert.AreEqual(0, diff.Differences.Count);
		}

		[Test]
		public void DiffInNested()
		{
			m_RootA.Nested.Value = "321";
			m_RootB.Nested.Value = "123";

			ObjectDifference diff = m_Comparer.Compare(m_RootA, m_RootB);
			Assert.AreEqual(1, diff.Nodes.Count);
			TestHelper.AssertDiff(diff.Nodes[0], typeof(ValueDifference));
		}

		[Test]
		public void NestedNullClass()
		{
			m_RootB.Nested = null;

			ObjectDifference diff = m_Comparer.Compare(m_RootA, m_RootB);
			Assert.AreEqual(0, diff.Nodes.Count);
			TestHelper.AssertDiff(diff, typeof(ValueDifference));
		}
	}
}
