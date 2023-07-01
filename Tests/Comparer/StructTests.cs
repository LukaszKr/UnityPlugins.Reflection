using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;

namespace ProceduralLevel.Reflection.Tests.Comparer
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
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}
	}
}
