using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class StructTests : AComparerTests
	{
		private readonly struct TestStruct
		{
			public readonly int Value;

			public TestStruct(int value)
			{
				Value = value;
			}
		}

		[Test]
		public void CompareStructs()
		{
			TestStruct left = new TestStruct(1);
			TestStruct right = new TestStruct(2);

			ObjectIssue diff = m_Comparer.Compare(left, right);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}
	}
}
