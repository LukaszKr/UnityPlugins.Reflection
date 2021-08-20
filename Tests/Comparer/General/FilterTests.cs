using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class FilterTests : AComparerTests
	{
		[Test]
		public void IgnoreType()
		{
			m_Comparer.Ignore(typeof(int));
			ObjectIssue diff = m_Comparer.Compare(1, 2);

			Assert.IsNull(diff);
		}

		[Test]
		public void TypeCheckHappensBeforeFilters()
		{
			m_Comparer.Ignore(typeof(int));
			ObjectIssue diff = m_Comparer.Compare(1, "test");

			TestHelper.AssertDiff(diff, typeof(DifferentTypeIssue));
		}
	}
}
