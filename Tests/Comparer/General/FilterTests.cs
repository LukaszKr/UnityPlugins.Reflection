using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Tests.Comparer
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
		public void IgnoreByType()
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
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentTypeIssue));
		}

		[Test]
		public void IgnoreByMemberName()
		{
			TestClass left = new TestClass(1);
			TestClass right = new TestClass(2);
			m_Comparer.IgnoreMember<TestClass>(nameof(left.Value));

			ObjectIssue diff = m_Comparer.Compare(left, right);
			Assert.IsNull(diff);
		}
	}
}
