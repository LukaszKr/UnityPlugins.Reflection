using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
{
	public class ReferenceTests : AComparerTests
	{
		private class RootClass
		{
			public NestedClass Nested;
		}

		private class NestedClass
		{
			public int Value;
		}

		[Test]
		public void DetectSharedObject()
		{
			RootClass rootA = new RootClass();
			RootClass rootB = new RootClass();
			rootA.Nested = new NestedClass();
			rootB.Nested = rootA.Nested;

			m_Comparer.DetectSharedObject(rootA.Nested);
			ObjectIssue diff = m_Comparer.Compare(rootA, rootB);
			TestHelper.AssertDiff(diff, typeof(SharedObjectIssue));
		}

		[Test]
		public void DetectDuplicatedSingleton()
		{
			RootClass rootA = new RootClass();
			RootClass rootB = new RootClass();
			rootA.Nested = new NestedClass();
			rootB.Nested = new NestedClass();

			m_Comparer.DetectSingleton(rootA.Nested);
			ObjectIssue diff = m_Comparer.Compare(rootA, rootB);
			TestHelper.AssertDiff(diff, typeof(DuplicatedSingletonIssue));
		}
	}
}
