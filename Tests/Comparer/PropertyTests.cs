using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
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

		[Test]
		public void DifferenceInAutoField()
		{
			ClassWithAutoProperty a = new ClassWithAutoProperty();
			a.AutoField = 2;
			ClassWithAutoProperty b = new ClassWithAutoProperty();
			b.AutoField = 1;
			ObjectIssue diff = m_Comparer.Compare(a, b);

			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}

		[Test]
		public void DifferenceInBackedField()
		{
			ClassWithBackingField a = new ClassWithBackingField(2);
			ClassWithBackingField b = new ClassWithBackingField(1);

			ObjectIssue diff = m_Comparer.Compare(a, b);
			TestHelper.AssertDiff(diff, typeof(DifferentValueIssue), 2);
		}
	}
}
