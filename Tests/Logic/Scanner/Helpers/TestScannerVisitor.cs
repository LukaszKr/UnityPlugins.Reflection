using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Scanner
{
	internal class TestScannerVisitor : IScannerVisitor
	{
		public int CallCount;
		public int ConsumeCount;

		public void Visit(ScannerVisitData data)
		{
			CallCount++;
			if(data.Value is TestScannerTarget)
			{
				ConsumeCount++;
			}
		}

		public void AssertResult(int callCount, int consumeCount)
		{
			Assert.IsTrue(CallCount >= ConsumeCount);
			Assert.AreEqual(callCount, CallCount);
			Assert.AreEqual(consumeCount, ConsumeCount);
		}
	}
}
