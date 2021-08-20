using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
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
			int[] arrayA = new int[] { 3, 2, 1 };
			int[] arrayB = new int[] { 1, 2, 3 };

			ObjectIssue diff = m_Comparer.Compare(arrayA, arrayB);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), 2);
		}

		[Test]
		public void DifferentArrayLength()
		{
			int[] arrayA = new int[] { 1 };
			int[] arrayB = new int[] { 2, 2 };
			
			ObjectIssue diff = m_Comparer.Compare(arrayA, arrayB);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), typeof(DifferentLengthIssue));
		}

		[Test]
		public void NestedArrays()
		{
			int[][] arrayA = new int[1][];
			int[][] arrayB = new int[1][];
			arrayA[0] = new int[] { 1 };
			arrayB[0] = new int[] { 2, 3 };

			ObjectIssue diff = m_Comparer.Compare(arrayA, arrayB);
			TestHelper.AssertDiff(diff.Nodes[0], typeof(DifferentValueIssue), typeof(DifferentLengthIssue));
		}

		[Test]
		public void NestedClassArray()
		{
			NestedClass[][] arrayA = new NestedClass[1][];
			NestedClass[][] arrayB = new NestedClass[1][];
			arrayA[0] = new NestedClass[] { new NestedClass(1), new NestedClass(2) };
			arrayB[0] = new NestedClass[] { new NestedClass(2) };

			ObjectIssue diff = m_Comparer.Compare(arrayA, arrayB);
			TestHelper.AssertDiff(diff.Nodes[0], typeof(DifferentLengthIssue));
			TestHelper.AssertDiff(diff.Nodes[0].Nodes[0], typeof(DifferentValueIssue));
		}
	}
}
