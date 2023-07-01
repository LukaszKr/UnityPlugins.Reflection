using NUnit.Framework;
using ProceduralLevel.Reflection.Tests.Comparer;
using ProceduralLevel.Reflection.Logic;
using UnityEngine;

namespace ProceduralLevel.Reflection.Tests
{
	public class AttributeTests : AComparerTests
	{
		private class TestClass
		{
			public ClassIgnoreToIgnore IgnoreClass;
			
			[ReflectionIgnore]
			public NestedClass NestedToIgnore;
			[ReflectionIgnore]
			public int FieldToIgnore;
			[ReflectionIgnore]
			public int PropertyToIgnore { get; set; }
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
