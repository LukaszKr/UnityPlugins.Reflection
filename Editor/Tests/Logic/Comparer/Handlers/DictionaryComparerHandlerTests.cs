using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Handlers
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class DictionaryComparerHandlerTests : AComparerHandlerTests<DictionaryComparerHandler>
	{
		private ReflectionComparer m_Comparer;
		private DictionaryComparerHandler m_Handler;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			m_Comparer = new ReflectionComparer();
			m_Handler = new DictionaryComparerHandler(m_Comparer);
		}

		#region Can Handle
		public static CanHandleTest[] GetCanHandleTests()
		{
			return new CanHandleTest[]
			{
				new CanHandleTest(typeof(string), false),
				new CanHandleTest(typeof(bool), false),
				new CanHandleTest(typeof(sbyte), false),
				new CanHandleTest(typeof(byte), false),
				new CanHandleTest(typeof(ushort), false),
				new CanHandleTest(typeof(short), false),
				new CanHandleTest(typeof(uint), false),
				new CanHandleTest(typeof(int), false),
				new CanHandleTest(typeof(ulong), false),
				new CanHandleTest(typeof(long), false),
				new CanHandleTest(typeof(float), false),
				new CanHandleTest(typeof(double), false),
				new CanHandleTest(typeof(IList), false),
				new CanHandleTest(typeof(List<int>), false),
				new CanHandleTest(typeof(IDictionary), true),
				new CanHandleTest(typeof(AComparerHandler), false),
			};
		}

		[Test, TestCaseSource(nameof(GetCanHandleTests))]
		public override void CanHandle(CanHandleTest test)
		{
			test.Run(m_Handler);
		}
		#endregion

		[Test]
		public override void Same()
		{
			Dictionary<int, int> dictA = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 2, 3 }
			};
			Dictionary<int, int> dictB = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 2, 3 }
			};

			CompareValues<Dictionary<int, int>>(m_Handler, dictA, dictB, false);
		}

		[Test]
		public void Different_Values()
		{
			Dictionary<int, int> dictA = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 2, 3 }
			};
			Dictionary<int, int> dictB = new Dictionary<int, int>
			{
				{ 1, 2 },
				{ 2, 4 }
			};

			ComparisionGroup group = CompareValues<Dictionary<int, int>>(m_Handler, dictA, dictB, true);

			Assert.AreEqual(2, group.SubResults.Count);
			DifferentValueResultEntry entry = (DifferentValueResultEntry)group.SubResults[0].Entries[0];
			Assert.AreEqual(entry.Left, 1);
			Assert.AreEqual(entry.Right, 2);

			entry = (DifferentValueResultEntry)group.SubResults[1].Entries[0];
			Assert.AreEqual(entry.Left, 3);
			Assert.AreEqual(entry.Right, 4);
		}

		[Test]
		public void Different_Keys()
		{
			Dictionary<int, int> dictA = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 2, 3 },
				{ 5, 9 }
			};
			Dictionary<int, int> dictB = new Dictionary<int, int>
			{
				{ 3, 1 },
				{ 4, 3 },
				{ 5, 9 },
			};

			ComparisionGroup group = CompareValues<Dictionary<int, int>>(m_Handler, dictA, dictB, true);
			Assert.AreEqual(4, group.Entries.Count);

			MissingKeyResultEntry entry = (MissingKeyResultEntry)group.Entries[0];
			Assert.AreEqual(entry.Key, 1);

			entry = (MissingKeyResultEntry)group.Entries[1];
			Assert.AreEqual(entry.Key, 2);

			entry = (MissingKeyResultEntry)group.Entries[2];
			Assert.AreEqual(entry.Key, 3);

			entry = (MissingKeyResultEntry)group.Entries[3];
			Assert.AreEqual(entry.Key, 4);
		}

		[Test]
		public void Different_KeysAndValues()
		{
			Dictionary<int, int> dictA = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 2, 3 }
			};
			Dictionary<int, int> dictB = new Dictionary<int, int>
			{
				{ 1, 2 },
				{ 3, 3 }
			};

			ComparisionGroup group = CompareValues<Dictionary<int, int>>(m_Handler, dictA, dictB, true);

			Assert.AreEqual(2, group.Entries.Count);

			MissingKeyResultEntry entry = (MissingKeyResultEntry)group.Entries[0];
			Assert.AreEqual(entry.Key, 2);

			entry = (MissingKeyResultEntry)group.Entries[1];
			Assert.AreEqual(entry.Key, 3);

			Assert.AreEqual(1, group.SubResults.Count);

			DifferentValueResultEntry valueEntry = (DifferentValueResultEntry)group.SubResults[0].Entries[0];
			Assert.AreEqual(valueEntry.Left, 1);
			Assert.AreEqual(valueEntry.Right, 2);

		}

		[Test]
		public void Different_Length()
		{
			Dictionary<int, int> dictA = new Dictionary<int, int>
			{
				{ 1, 1 },
				{ 2, 3 }
			};
			Dictionary<int, int> dictB = new Dictionary<int, int>
			{
				{ 1, 1 },
			};

			ComparisionGroup group = CompareValues<Dictionary<int, int>>(m_Handler, dictA, dictB, true);
			DifferentLengthResultEntry entry = (DifferentLengthResultEntry)group.Entries[0];
			Assert.AreEqual(2, entry.Left);
			Assert.AreEqual(1, entry.Right);

			Assert.AreEqual(0, group.SubResults.Count); //if length is different, comparer ignores content
		}

		[Test]
		public override void Null_Both()
		{
			CompareValues<Dictionary<int, int>>(m_Handler, null, null, false);
		}

		[Test]
		public override void Null_Left()
		{
			Dictionary<int, int> dict = new Dictionary<int, int>();
			ComparisionGroup group = CompareValues<Dictionary<int, int>>(m_Handler, null, dict, true);
			Assert.IsTrue(group.Entries[0] is DifferentValueResultEntry);
		}

		[Test]
		public override void Null_Right()
		{
			Dictionary<int, int> dict = new Dictionary<int, int>();
			ComparisionGroup group = CompareValues<Dictionary<int, int>>(m_Handler, dict, null, true);
			Assert.IsTrue(group.Entries[0] is DifferentValueResultEntry);
		}
	}
}
