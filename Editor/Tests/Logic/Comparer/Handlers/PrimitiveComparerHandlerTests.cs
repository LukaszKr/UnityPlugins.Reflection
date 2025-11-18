using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Handlers
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class PrimitiveComparerHandlerTests : AComparerHandlerTests<PrimitiveComparerHandler>
	{
		private ReflectionComparer m_Comparer;
		private PrimitiveComparerHandler m_Handler;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			m_Comparer = new ReflectionComparer();
			m_Handler = new PrimitiveComparerHandler(m_Comparer);
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
				new CanHandleTest(typeof(IList), false),
				new CanHandleTest(typeof(List<int>), false),
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
			CompareValues<int>(m_Handler, 1, 1, false);
		}

		[Test]
		public void Different()
		{
			ComparisionGroup group = CompareValues<int>(m_Handler, 1, 0, true);
			Assert.IsTrue(group.Entries[0] is DifferentValueResultEntry);
		}

		[Test]
		public override void Null_Both()
		{
			CompareValues<int>(m_Handler, null, null, false);
		}

		[Test]
		public override void Null_Left()
		{
			ComparisionGroup group = CompareValues<int>(m_Handler, null, 1, true);
			Assert.IsTrue(group.Entries[0] is DifferentValueResultEntry);
		}

		[Test]
		public override void Null_Right()
		{
			ComparisionGroup group = CompareValues<int>(m_Handler, 1, null, true);
			Assert.IsTrue(group.Entries[0] is DifferentValueResultEntry);
		}
	}
}
