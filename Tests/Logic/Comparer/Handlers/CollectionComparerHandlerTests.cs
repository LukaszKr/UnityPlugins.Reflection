using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Handlers
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class CollectionComparerHandlerTests : AComparerHandlerTests<CollectionComparerHandler>
	{
		private ReflectionComparer m_Comparer;
		private CollectionComparerHandler m_Handler;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			m_Comparer = new ReflectionComparer();
			m_Handler = new CollectionComparerHandler(m_Comparer);
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
				new CanHandleTest(typeof(IList), true),
				new CanHandleTest(typeof(List<int>), true),
				new CanHandleTest(typeof(IDictionary), true),
				new CanHandleTest(typeof(AComparerHandler), false),
			};
		}

		[Test]
		public override void CanHandle([ValueSource(nameof(GetCanHandleTests))] CanHandleTest test)
		{
			test.Run(m_Handler);
		}
		#endregion

		[Test]
		public override void Same()
		{
			List<int> listA = new List<int>() { 1, 2, 3 };
			List<int> listB = new List<int>() { 1, 2, 3 };
			CompareValues<List<int>>(m_Handler, listA, listB, false);
		}

		[Test]
		public void Different()
		{
			List<int> listA = new List<int>() { 3, 2, 1 };
			List<int> listB = new List<int>() { 1, 2, 4 };
			ComparisionGroup group = CompareValues<List<int>>(m_Handler, listA, listB, true);

			Assert.AreEqual(2, group.SubResults.Count);
			DifferentValueResultEntry entry = (DifferentValueResultEntry)group.SubResults[0].Entries[0];
			Assert.AreEqual(3, entry.Left);
			Assert.AreEqual(1, entry.Right);

			entry = (DifferentValueResultEntry)group.SubResults[1].Entries[0];
			Assert.AreEqual(1, entry.Left);
			Assert.AreEqual(4, entry.Right);
		}

		[Test]
		public void Different_Length()
		{
			List<int> listA = new List<int>() { 3, 2, 1 };
			List<int> listB = new List<int>() { 1, 2 };
			ComparisionGroup group = CompareValues<List<int>>(m_Handler, listA, listB, true);

			Assert.AreEqual(1, group.Entries.Count);
			DifferentLengthResultEntry entry = (DifferentLengthResultEntry)group.Entries[0];
			Assert.AreEqual(3, entry.Left);
			Assert.AreEqual(2, entry.Right);

			Assert.AreEqual(0, group.SubResults.Count); //if length is different, comparer ignores content
		}

		[Test]
		public override void Null_Both()
		{
			CompareValues<List<int>>(m_Handler, null, null, false);
		}

		[Test]
		public override void Null_Left()
		{
			ComparisionGroup group = CompareValues<List<int>>(m_Handler, null, new List<int>(), true);
			DifferentValueResultEntry entry = (DifferentValueResultEntry)group.Entries[0];
			Assert.IsNull(entry.Left);
			Assert.IsNotNull(entry.Right);
		}

		[Test]
		public override void Null_Right()
		{
			ComparisionGroup group = CompareValues<List<int>>(m_Handler, new List<int>(), null, true);
			DifferentValueResultEntry entry = (DifferentValueResultEntry)group.Entries[0];
			Assert.IsNotNull(entry.Left);
			Assert.IsNull(entry.Right);
		}
	}
}
