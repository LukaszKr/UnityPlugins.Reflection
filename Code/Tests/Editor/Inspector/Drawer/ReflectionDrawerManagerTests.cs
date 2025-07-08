using System;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Editor.Inspector.Drawer
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class ReflectionDrawerManagerTests
	{
		private abstract class ABaseClass
		{
		}

		private class SubClassA : ABaseClass
		{
		}

		private class SubClassB : ABaseClass
		{
		}

		private class TestDrawer<TValue> : AValueInspectorDrawer<TValue>
		{
			protected override TValue OnDraw(object parent, Type type, string label, TValue value)
			{
				throw new System.NotImplementedException();
			}
		}

		[Test, Description("Trying to get drawer for unhandled type should return null.")]
		public void GetDrawer_Missing()
		{
			ReflectionDrawerManager manager = new ReflectionDrawerManager();
			Assert.IsNull(manager.GetDrawer(typeof(int)));
		}

		[Test, Description("Generic drawers can handle sub classes.")]
		public void GetDrawer_Generic()
		{
			ReflectionDrawerManager manager = new ReflectionDrawerManager();
			TestDrawer<ABaseClass> drawer = new TestDrawer<ABaseClass>();

			manager.AddGenericDrawer(drawer);
			Assert.AreEqual(drawer, manager.GetDrawer(typeof(ABaseClass)));
			Assert.AreEqual(drawer, manager.GetDrawer(typeof(SubClassA)));
			Assert.AreEqual(drawer, manager.GetDrawer(typeof(SubClassB)));
		}

		[Test, Description("If there is specific drawer, it takes priority over generic one.")]
		public void GetDrawer_SpecificOverridesGeneric()
		{
			ReflectionDrawerManager manager = new ReflectionDrawerManager();
			TestDrawer<ABaseClass> genericDrawer = new TestDrawer<ABaseClass>();
			TestDrawer<SubClassB> specificDrawer = new TestDrawer<SubClassB>();

			manager.AddGenericDrawer(genericDrawer);
			manager.AddSpecificDrawer(specificDrawer);
			Assert.AreEqual(genericDrawer, manager.GetDrawer(typeof(ABaseClass)));
			Assert.AreEqual(genericDrawer, manager.GetDrawer(typeof(SubClassA)));
			Assert.AreEqual(specificDrawer, manager.GetDrawer(typeof(SubClassB)));
		}

		[Test, Description("If there are multiple generic drawers for the same type, first one registered will be used.")]
		public void GetDrawer_MultipleGenericDrawers()
		{
			ReflectionDrawerManager manager = new ReflectionDrawerManager();
			TestDrawer<ABaseClass> genericDrawerA = new TestDrawer<ABaseClass>();
			TestDrawer<ABaseClass> genericDrawerB = new TestDrawer<ABaseClass>();

			manager.AddGenericDrawer(genericDrawerA);
			manager.AddGenericDrawer(genericDrawerB);
			Assert.AreEqual(genericDrawerB, manager.GetDrawer(typeof(ABaseClass)));
			Assert.AreEqual(genericDrawerB, manager.GetDrawer(typeof(SubClassA)));
			Assert.AreEqual(genericDrawerB, manager.GetDrawer(typeof(SubClassB)));
		}
	}
}
