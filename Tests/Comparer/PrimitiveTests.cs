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
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void DifferentPrimitives()
		{
			ObjectIssue diff = m_Comparer.Compare(1, 1.0f);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), typeof(DifferentTypeIssue));
		}

		[Test]
		public void RightSideNull()
		{
			ObjectIssue diff = m_Comparer.Compare("hello", null);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void LeftSideNull()
		{
			ObjectIssue diff = m_Comparer.Compare(null, "hello");
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void PrimitiveAndString()
		{
			ObjectIssue diff = m_Comparer.Compare(1, "hello");
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), typeof(DifferentTypeIssue));
		}
	}
}
