using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Scanner
{
	internal class TestScannerVisitor : IScannerVisitor
	{
		public int VisitedCount;
		public int AcceptedCount;

		public void Visit(ScannerVisitData data)
		{
			VisitedCount++;
			if(data.Value is TestScannerTarget)
			{
				AcceptedCount++;
			}
		}

		public void AssertResult(int visitedCount, int acceptedCount)
		{
			Assert.IsTrue(VisitedCount >= AcceptedCount);
			Assert.AreEqual(visitedCount, VisitedCount, nameof(VisitedCount));
			Assert.AreEqual(acceptedCount, AcceptedCount, nameof(AcceptedCount));
		}
	}
}
