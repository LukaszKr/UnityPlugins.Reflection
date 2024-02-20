using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;

namespace ProceduralLevel.Reflection.Tests.Comparer
{
	[Category(ReflectionTestsConsts.CATEGORY)]
	public class CyclicReferenceTests : AComparerTests
	{
		public class CyclicRef
		{
			public CyclicRef Cyclic;
		}

		[Test]
		public void TracksVisitedObjects()
		{
			CyclicRef left = new CyclicRef();
			left.Cyclic = new CyclicRef();
			left.Cyclic.Cyclic = left;

			CyclicRef right = new CyclicRef();
			right.Cyclic = new CyclicRef();
			right.Cyclic.Cyclic = right;

			ObjectIssue diff = m_Comparer.Compare(left, right);
			Assert.IsNull(diff);
		}
	}
}
