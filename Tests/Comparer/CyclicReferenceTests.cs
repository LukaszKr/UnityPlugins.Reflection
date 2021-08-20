using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class CyclicReferenceTests : AComparerTests
	{
		public class CyclicRef
		{
			public CyclicRef Cyclic;
		}

		[Test]
		public void TracksVisitedObjects()
		{
			CyclicRef cyclicA = new CyclicRef();
			cyclicA.Cyclic = new CyclicRef();
			cyclicA.Cyclic.Cyclic = cyclicA;

			CyclicRef cyclicB = new CyclicRef();
			cyclicB.Cyclic = new CyclicRef();
			cyclicB.Cyclic.Cyclic = cyclicB;

			ObjectDifference diff = m_Comparer.Compare(cyclicA, cyclicB);
			Assert.AreEqual(0, diff.Differences.Count);
			Assert.AreEqual(0, diff.Nodes.Count);
		}
	}
}
