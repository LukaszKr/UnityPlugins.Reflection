using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Tests.Comparer
{
	public class NestedClassTests : AComparerTests
	{
		private class RootClass
		{
			public object Value = null;
			public NestedClass Nested = new NestedClass();
		}

		private class NestedClass
		{
			public object Value;
		}

		private class DeepNestClass
		{
			public RootClass Root = new RootClass();
		}

		private RootClass m_Left;
		private RootClass m_Right;

		[SetUp]
		public override void Setup()
		{
			base.Setup();

			m_Left = new RootClass();
			m_Right = new RootClass();
		}

		[Test]
		public void CompareDeeplyNestedClasses()
		{
			DeepNestClass left = new DeepNestClass();
			DeepNestClass right = new DeepNestClass();
			left.Root.Nested.Value = 2;
			right.Root.Nested.Value = 1;

			ObjectIssue diff = m_Comparer.Compare(left, right);
			Assert.AreEqual(1, diff.Nodes.Count);
			ComparerTestHelper.AssertDiff(diff.Nodes[0].Nodes[0], typeof(DifferentValueIssue));
		}

		[Test]
		public void CanCompareNestedClasses()
		{
			ObjectIssue diff = m_Comparer.Compare(m_Left, m_Right);
			Assert.IsNull(diff);
		}

		[Test]
		public void DiffInNested()
		{
			m_Left.Nested.Value = "321";
			m_Right.Nested.Value = "123";

			ObjectIssue diff = m_Comparer.Compare(m_Left, m_Right);
			Assert.AreEqual(1, diff.Nodes.Count);
			ComparerTestHelper.AssertDiff(diff.Nodes[0], typeof(DifferentValueIssue));
		}

		[Test]
		public void NestedNullClass()
		{
			m_Right.Nested = null;

			ObjectIssue diff = m_Comparer.Compare(m_Left, m_Right);
			Assert.AreEqual(0, diff.Nodes.Count);
			ComparerTestHelper.AssertDiff(diff, typeof(DifferentValueIssue));
		}
	}
}
