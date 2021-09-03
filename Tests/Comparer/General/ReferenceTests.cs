﻿using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Tests.Comparer
{
	public class ReferenceTests : AComparerTests
	{
		private class RootClass
		{
			public NestedClass Nested;
		}

		private class NestedClass
		{
			public int Value = 0;
		}

		[Test]
		public void DetectSharedObject()
		{
			RootClass left = new RootClass();
			RootClass right = new RootClass();
			left.Nested = new NestedClass();
			right.Nested = left.Nested;

			m_Comparer.DetectSharedObject(left.Nested);
			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertDiff(diff, typeof(SharedObjectIssue));
		}

		[Test]
		public void DetectDuplicatedSingleton()
		{
			RootClass left = new RootClass();
			RootClass right = new RootClass();
			left.Nested = new NestedClass();
			right.Nested = new NestedClass();

			m_Comparer.DetectSingleton(left.Nested);
			ObjectIssue diff = m_Comparer.Compare(left, right);
			ComparerTestHelper.AssertDiff(diff, typeof(DuplicatedSingletonIssue));
		}
	}
}
