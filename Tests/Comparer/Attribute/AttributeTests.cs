using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;
using ProceduralLevel.Reflection.Tests.Comparer;

namespace ProceduralLevel.Reflection.Tests
{
	[Category(ReflectionTestsConsts.CATEGORY)]
	public class AttributeTests : AComparerTests
	{
		private class TestClass
		{
			public ClassIgnoreToIgnore IgnoreClass;

			[ReflectionIgnore]
			private int m_PrivateFieldToIgnore;
			[ReflectionIgnore]
			public NestedClass NestedToIgnore;
			[ReflectionIgnore]
			public int FieldToIgnore;
			[ReflectionIgnore]
			public int PropertyToIgnore { get; set; }

			public TestClass(int privateFieldToIgnore = 0)
			{
				m_PrivateFieldToIgnore = privateFieldToIgnore;
			}
		}

		private class NestedClass
		{
			public int Value;

			public NestedClass(int value)
			{
				Value = value;
			}
		}

		[ReflectionIgnore]
		private class ClassIgnoreToIgnore
		{
			public int Value;

			public ClassIgnoreToIgnore(int value)
			{
				Value = value;
			}
		}

		[Test]
		public void IgnoreClass()
		{
			TestClass left = new TestClass()
			{
				IgnoreClass = new ClassIgnoreToIgnore(1)
			};
			TestClass right = new TestClass()
			{
				IgnoreClass = new ClassIgnoreToIgnore(2)
			};

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertNoDiff(diff);
		}

		[Test]
		public void IgnoreField()
		{
			TestClass left = new TestClass()
			{
				FieldToIgnore = 1
			};
			TestClass right = new TestClass()
			{
				FieldToIgnore = 2
			};

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertNoDiff(diff);
		}

		[Test]
		public void IgnorePrivateField()
		{
			TestClass left = new TestClass(1);
			TestClass right = new TestClass(2);

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertNoDiff(diff);
		}

		[Test]
		public void IgnoreTested()
		{
			TestClass left = new TestClass()
			{
				NestedToIgnore = new NestedClass(1)
			};
			TestClass right = new TestClass()
			{
				NestedToIgnore = new NestedClass(2)
			};

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertNoDiff(diff);
		}

		[Test]
		public void IgnoreProperty()
		{
			TestClass left = new TestClass()
			{
				PropertyToIgnore = 1
			};
			TestClass right = new TestClass()
			{
				PropertyToIgnore = 2
			};

			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertNoDiff(diff);
		}
	}
}
