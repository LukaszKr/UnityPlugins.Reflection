using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Tests.Comparer
{
	public class PrimitiveTests : AComparerTests
	{
		// A Test behaves as an ordinary method
		[Test]
		public void String()
		{
			ObjectIssue diff = m_Comparer.Compare("stringA", "stringB");
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void DifferentPrimitives()
		{
			ObjectIssue diff = m_Comparer.Compare(1, 1.0f);
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue), typeof(DifferentTypeIssue));
		}

		[Test]
		public void RightSideNull()
		{
			ObjectIssue diff = m_Comparer.Compare("hello", null);
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void LeftSideNull()
		{
			ObjectIssue diff = m_Comparer.Compare(null, "hello");
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void PrimitiveAndString()
		{
			ObjectIssue diff = m_Comparer.Compare(1, "hello");
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue), typeof(DifferentTypeIssue));
		}
	}
}
