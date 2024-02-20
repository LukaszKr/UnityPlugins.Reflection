using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;

namespace ProceduralLevel.Reflection.Tests.Comparer
{
	[Category(ReflectionTestsConsts.CATEGORY)]
	public class ArrayTests : AComparerTests
	{
		private class NestedClass
		{
			public int Value;

			public NestedClass(int value)
			{
				Value = value;
			}
		}

		[Test]
		public void ArrayOfInts()
		{
			int[] left = new int[] { 3, 2, 1 };
			int[] right = new int[] { 1, 2, 3 };

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue), 2);
		}

		[Test]
		public void DifferentArrayLength()
		{
			int[] left = new int[] { 1 };
			int[] right = new int[] { 2, 2 };

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue), typeof(DifferentLengthIssue));
		}

		[Test]
		public void NestedArrays()
		{
			int[][] left = new int[1][];
			int[][] right = new int[1][];
			left[0] = new int[] { 1 };
			right[0] = new int[] { 2, 3 };

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertDiff(diff.Nodes[0], typeof(DifferentValueIssue), typeof(DifferentLengthIssue));
		}

		[Test]
		public void NestedClassArray()
		{
			NestedClass[][] left = new NestedClass[1][];
			NestedClass[][] right = new NestedClass[1][];
			left[0] = new NestedClass[] { new NestedClass(1), new NestedClass(2) };
			right[0] = new NestedClass[] { new NestedClass(2) };

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertDiff(diff.Nodes[0], typeof(DifferentLengthIssue));
			ComparerTestHelper.AssertDiff(diff.Nodes[0].Nodes[0], typeof(DifferentValueIssue));
		}
	}
}
