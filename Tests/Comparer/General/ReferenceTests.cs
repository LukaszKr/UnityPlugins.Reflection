﻿using System;
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
			RootClass left = new RootClass();
			RootClass right = new RootClass();
			left.Nested = new NestedClass();
			right.Nested = left.Nested;

			m_Comparer.DetectSharedObject(left.Nested);
			ObjectIssue diff = m_Comparer.Compare(left, right);
			TestHelper.AssertDiff(diff, typeof(SharedObjectIssue));
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
			TestHelper.AssertDiff(diff, typeof(DuplicatedSingletonIssue));
		}
	}
}
