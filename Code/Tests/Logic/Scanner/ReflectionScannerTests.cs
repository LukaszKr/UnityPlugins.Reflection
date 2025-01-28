using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Scanner
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class ReflectionScannerTests
	{
		private class TestFilter : IScannerFilter
		{
			public readonly Type IgnoreType;

			public TestFilter(Type ignoreType)
			{
				IgnoreType = ignoreType;
			}

			public bool IsValid(Type type)
			{
				return type != IgnoreType;
			}
		}

		private class ContainerClass
		{
			public int Value = 0;
			public TestScannerTarget NullTarget = null;
			public TestScannerTarget NotNullTarget = new TestScannerTarget();
		}

		private class CyclicClass
		{
			public CyclicClass Cycle;
			public TestScannerTarget Target = new TestScannerTarget();

			public CyclicClass()
			{
				Cycle = new CyclicClass(this);
			}

			public CyclicClass(CyclicClass cycle)
			{
				Cycle = cycle;
			}
		}

		public class StaticContainer
		{
			public static TestScannerTarget StaticTarget = new TestScannerTarget();
			public TestScannerTarget NonStaticTarget = new TestScannerTarget();
		}

		private ReflectionScanner m_Scanner;
		private TestScannerVisitor m_Visitor;

		[SetUp]
		public void Setup()
		{
			m_Scanner = new ReflectionScanner();
			m_Visitor = new TestScannerVisitor();
			m_Scanner.Visitors.Add(m_Visitor);
		}

		[Test]
		public void Scan()
		{
			ScanAndAssert(new TestScannerTarget(), 1, 1);
		}

		[Test]
		public void Scan_Null()
		{
			ScanAndAssert(null, 0, 0);
		}

		[Test]
		public void Scan_Primitive()
		{
			ScanAndAssert(1, 1, 0);
		}

		[Test]
		public void Scan_String()
		{
			ScanAndAssert("hello world", 1, 0);
		}

		[Test, Description("Hit for list, and each value.")]
		public void Scan_List()
		{
			List<object> list = new List<object>
			{
				new TestScannerTarget(),
				new TestScannerTarget(),
				1
			};
			ScanAndAssert(list, 4, 2);
		}

		[Test, Description("Hit for array, and each value.")]
		public void Scan_Array()
		{
			object[] array = new object[]
			{
				new TestScannerTarget(),
				new TestScannerTarget(),
				1
			};
			ScanAndAssert(array, 4, 2);
		}

		[Test, Description("Hit for dictionary, and each value.")]
		public void Scan_Dictionary()
		{
			Dictionary<object, object> dictionary = new Dictionary<object, object>()
			{
				{ 0, new TestScannerTarget() },
				{ 1, new TestScannerTarget() },
				{ 2, 5 },
			};
			ScanAndAssert(dictionary, 4, 2);
		}

		[Test, Description("Hit for container, int, and value. Null is ignored.")]
		public void Scan_Nested()
		{
			ContainerClass container = new ContainerClass();

			ScanAndAssert(container, 3, 1);
		}

		[Test, Description("Each object should be visited only once. 2 for cyclic classes, 2 for targets.")]
		public void Scan_CyclicReference()
		{
			CyclicClass cyclic = new CyclicClass();
			ScanAndAssert(cyclic, 4, 2);
		}

		[Test]
		public void Scan_IncludeStatic()
		{
			m_Scanner.Analyzer.IncludeStatic = true;
			StaticContainer container = new StaticContainer();
			//static container + both keys visited
			ScanAndAssert(container, 3, 2);
		}

		[Test]
		public void Scan_IncludeOnlyStatic()
		{
			m_Scanner.Analyzer.IncludeStatic = true;
			m_Scanner.Analyzer.IncludeInstance = false;
			StaticContainer container = new StaticContainer();
			//static container + static key visited
			ScanAndAssert(container, 2, 1);
		}

		[Test]
		public void Scan_IncludeNone()
		{
			m_Scanner.Analyzer.IncludeStatic = false;
			m_Scanner.Analyzer.IncludeInstance = false;
			StaticContainer container = new StaticContainer();
			//1 visited - static container
			ScanAndAssert(container, 1, 0);
		}

		[Test]
		public void Filters_AreApplied()
		{
			m_Scanner.Filters.Add(new TestFilter(typeof(int)));
			ContainerClass container = new ContainerClass();

			ScanAndAssert(container, 2, 1);
		}

		private void ScanAndAssert(object value, int visitedCount, int acceptedCount)
		{
			m_Scanner.Scan(value);
			m_Visitor.AssertResult(visitedCount, acceptedCount);
		}
	}
}
