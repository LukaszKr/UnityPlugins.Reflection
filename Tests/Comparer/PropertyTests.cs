using System;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Tests
{
	public class PropertyTests : AComparerTests
	{
		private class ClassWithAutoProperty
		{
			public int AutoField { get; set; }
		}

		private class ClassWithBackingField
		{
			private int _backingField;
			public int BackedField => _backingField;

			public ClassWithBackingField(int backField = 0)
			{
				_backingField = backField;
			}
		}

		private class ExceptionThrowingProperty
		{
			public int Exception => throw new Exception();
		}

		[Test]
		public void PropertyThatThrowsException()
		{
			ExceptionThrowingProperty left = new ExceptionThrowingProperty();
			ExceptionThrowingProperty right = new ExceptionThrowingProperty();

			ObjectIssue diff = m_Comparer.Compare(left, right);
			TestHelper.AssertDiff(diff, typeof(ExceptionIssue));
		}

		[Test]
		public void DifferenceInAutoField()
		{
			ClassWithAutoProperty left = new ClassWithAutoProperty();
			left.AutoField = 2;
			ClassWithAutoProperty right = new ClassWithAutoProperty();
			right.AutoField = 1;

			ObjectIssue diff = m_Comparer.Compare(left, right);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void DifferenceInBackedField()
		{
			ClassWithBackingField left = new ClassWithBackingField(2);
			ClassWithBackingField right = new ClassWithBackingField(1);

			ObjectIssue diff = m_Comparer.Compare(left, right);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), 2);
		}
	}
}
