using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Handlers
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class ObjectComparerHandlerTests : AComparerHandlerTests<ObjectComparerHandler>
	{
		private class TestClass
		{
			public int Value;

			public TestClass(int value = 0)
			{
				Value = value;
			}
		}

		private ReflectionComparer m_Comparer;
		private ObjectComparerHandler m_Handler;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			m_Comparer = new ReflectionComparer();
			m_Handler = new ObjectComparerHandler(m_Comparer);
		}

		#region Can Handle
		public static CanHandleTest[] GetCanHandleTests()
		{
			return new CanHandleTest[]
			{
				new CanHandleTest(typeof(string), true),
				new CanHandleTest(typeof(bool), true),
				new CanHandleTest(typeof(sbyte), true),
				new CanHandleTest(typeof(byte), true),
				new CanHandleTest(typeof(ushort), true),
				new CanHandleTest(typeof(short), true),
				new CanHandleTest(typeof(uint), true),
				new CanHandleTest(typeof(int), true),
				new CanHandleTest(typeof(ulong), true),
				new CanHandleTest(typeof(long), true),
				new CanHandleTest(typeof(float), true),
				new CanHandleTest(typeof(double), true),
				new CanHandleTest(typeof(TestClass), true),
				new CanHandleTest(typeof(IList), true),
				new CanHandleTest(typeof(List<int>), true),
				new CanHandleTest(typeof(AComparerHandler), true),
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
			TestClass left = new TestClass();
			TestClass right = new TestClass();
			CompareValues<TestClass>(m_Handler, left, right, false);
		}

		[Test]
		public void Different()
		{
			TestClass left = new TestClass();
			TestClass right = new TestClass(1);
			ComparisionGroup group = CompareValues<TestClass>(m_Handler, left, right, true);
			DifferentValueResultEntry entry = (DifferentValueResultEntry)group.SubResults[0].Entries[0];
			Assert.AreEqual(1, group.SubResults.Count);
			Assert.AreEqual(1, group.SubResults[0].Entries.Count);

			Assert.AreEqual(0, entry.Left);
			Assert.AreEqual(1, entry.Right);
		}

		[Test]
		public override void Null_Both()
		{
			CompareValues<TestClass>(m_Handler, null, null, false);
		}

		[Test]
		public override void Null_Left()
		{
			ComparisionGroup group = CompareValues<TestClass>(m_Handler, null, new TestClass(), true);
			Assert.IsTrue(group.Entries[0] is DifferentValueResultEntry);
		}

		[Test]
		public override void Null_Right()
		{
			ComparisionGroup group = CompareValues<TestClass>(m_Handler, new TestClass(), null, true);
			Assert.IsTrue(group.Entries[0] is DifferentValueResultEntry);
		}
	}
}
