using System.Collections.Generic;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class ReflectionComparerTests
	{
		private ReflectionComparer m_Comparer;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			m_Comparer = new ReflectionComparer();
		}

		#region Primitive
		[Test]
		public void Compare_Primitive_Equal()
		{
			ComparisionGroup result = m_Comparer.Compare(1, 1);
			Assert.IsNull(result);
		}

		[Test]
		public void Compare_Primitive_NotEqual()
		{
			ComparisionGroup result = m_Comparer.Compare(1, 2);
			Assert.AreEqual(0, result.SubResults.Count);
			Assert.AreEqual(1, result.Entries.Count);

			Assert.IsTrue(result.Entries[0] is DifferentValueResultEntry);
		}
		#endregion

		#region Null
		[Test]
		public void Compare_Null_Both()
		{
			ComparisionGroup result = m_Comparer.Compare(null, null);
			Assert.IsNull(result);
		}

		[Test]
		public void Compare_Null_Left()
		{
			ComparisionGroup result = m_Comparer.Compare(null, 1);
			Assert.AreEqual(0, result.SubResults.Count);
			Assert.AreEqual(1, result.Entries.Count);

			Assert.IsTrue(result.Entries[0] is DifferentValueResultEntry);
		}

		[Test]
		public void Compare_Null_Right()
		{
			ComparisionGroup result = m_Comparer.Compare(1, null);
			Assert.AreEqual(0, result.SubResults.Count);
			Assert.AreEqual(1, result.Entries.Count);

			Assert.IsTrue(result.Entries[0] is DifferentValueResultEntry);
		}
		#endregion

		#region Classes
		private class TestClass
		{
			public int IntValue = 0;
			public string StringValue = "";
			public bool BoolValue = true;
		}

		[Test]
		public void Compare_Class_DifferentInstances_SameValues()
		{
			TestClass left = new TestClass();
			TestClass right = new TestClass();

			ComparisionGroup result = m_Comparer.Compare(left, right);
			Assert.IsNull(result);
		}

		[Test]
		public void Compare_Class_SameInstances()
		{
			TestClass instance = new TestClass();

			ComparisionGroup result = m_Comparer.Compare(instance, instance);
			Assert.AreEqual(0, result.SubResults.Count);
			Assert.AreEqual(1, result.Entries.Count);

			Assert.IsTrue(result.Entries[0] is SharedInstanceResultEntry);
		}
		#endregion

		#region Cyclic
		private class CyclicClass
		{
			public int Value;
			public CyclicClass Reference;

			public CyclicClass()
			{
				Reference = new CyclicClass(this);
			}

			public CyclicClass(CyclicClass other)
			{
				Reference = other;
			}
		}

		[Test]
		public void Compare_CyclicReference()
		{
			CyclicClass cycleA = new CyclicClass();
			CyclicClass cycleB = new CyclicClass();

			ComparisionGroup result = m_Comparer.Compare(cycleA, cycleB);
			Assert.IsNull(result);
		}

		[Test]
		public void Compare_CyclicReference_ComparerIsRestarted()
		{
			CyclicClass cycleA = new CyclicClass();
			CyclicClass cycleB = new CyclicClass();

			ComparisionGroup result = m_Comparer.Compare(cycleA, cycleB);
			Assert.IsNull(result);

			cycleA.Value = 5;
			result = m_Comparer.Compare(cycleA, cycleB);
			Assert.IsNotNull(result);
		}
		#endregion

		#region Collections
		[Test]
		public void Compare_Collection_List()
		{
			List<int> listA = new List<int>()
			{
				1, 2, 3
			};
			List<int> listB = new List<int>()
			{
				2, 3, 1
			};

			ComparisionGroup result = m_Comparer.Compare(listA, listB);
			Assert.IsNotNull(result);
		}

		[Test]
		public void Compare_Collection_Array()
		{
			int[] arrayA = new int[]
			{
				1, 2, 3
			};
			int[] arrayB = new int[]
			{
				2, 3, 1
			};

			ComparisionGroup result = m_Comparer.Compare(arrayA, arrayB);
			Assert.IsNotNull(result);
		}

		[Test]
		public void Compare_Collection_Dictionary()
		{
			Dictionary<int, int> dictA = new Dictionary<int, int>()
			{
				{ 1, 2 },
				{ 2, 3 }
			};
			Dictionary<int, int> dictB = new Dictionary<int, int>()
			{
				{ 1, 3 },
				{ 3, 3 }
			};

			ComparisionGroup result = m_Comparer.Compare(dictA, dictB);
			Assert.IsNotNull(result);
		}
		#endregion
	}
}
