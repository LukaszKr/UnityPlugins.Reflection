using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class ComparePrimitivesTests : AComparerTests
	{
		// A Test behaves as an ordinary method
		[Test]
		public void String()
		{
			ObjectDifference diff = m_Comparer.Compare("stringA", "stringB");
			TestHelper.AssertDiff(diff, typeof(ValueDifference));
		}

		[Test]
		public void DifferentPrimitives()
		{
			ObjectDifference diff = m_Comparer.Compare(1, 1.0f);
			TestHelper.AssertDiff(diff, typeof(ValueDifference), typeof(TypeDifference));
		}

		[Test]
		public void RightSideNull()
		{
			ObjectDifference diff = m_Comparer.Compare("hello", null);
			TestHelper.AssertDiff(diff, typeof(ValueDifference));
		}

		[Test]
		public void LeftSideNull()
		{
			ObjectDifference diff = m_Comparer.Compare(null, "hello");
			TestHelper.AssertDiff(diff, typeof(ValueDifference));
		}

		[Test]
		public void PrimitiveAndString()
		{
			ObjectDifference diff = m_Comparer.Compare(1, "hello");
			TestHelper.AssertDiff(diff, typeof(ValueDifference), typeof(TypeDifference));
		}
	}
}
