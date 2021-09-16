using System;
using System.Collections.Generic;
using System.Reflection;
using ProceduralLevel.UnityPlugins.Common.Logic;
using ProceduralLevel.UnityPlugins.Reflection.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ReflectionInspector
	{
		public static readonly ReflectionInspector Instance = new ReflectionInspector();

		public readonly ReflectionInspectorLayout Layout = new ReflectionInspectorLayout();

		private readonly List<AReflectionDrawer> _builtInDrawers = new List<AReflectionDrawer>();
		private readonly NotSupportedReflectionDrawer _notSupportedDrawer = new NotSupportedReflectionDrawer();

		public ReflectionInspector()
		{
			List<Type> types = AssemblyHelper.GetAllWithAttribute<CustomReflectionDrawerAttribute>();
			int count = types.Count;
			for(int x = 0; x < count; ++x)
			{
				Type type = types[x];
				AReflectionDrawer drawer = (AReflectionDrawer)Activator.CreateInstance(type);
				Add(drawer);
			}

			Add(new ObjectReflectionDrawer());
		}

		private void Add(AReflectionDrawer drawer)
		{
			_builtInDrawers.Add(drawer);
		}

		public void DrawLayout(object inspectedObject, FieldInfo field, float width)
		{
			Rect rect = Draw(inspectedObject, field, width);
			GUILayoutUtility.GetRect(rect.width, rect.height);
		}

		public Rect Draw(object inspectedObject, FieldInfo field, float width)
		{
			Assert.IsNotNull(inspectedObject);
			Layout.Start(width);

			Type type = inspectedObject.GetType();
			Assert.IsTrue(!type.IsPrimitive);
			Assert.IsTrue(!type.IsValueType);

			AReflectionDrawer drawer = GetDrawer(type);
			drawer.Draw(this, inspectedObject, field);

			return Layout.Current;
		}

		public AReflectionDrawer GetDrawer(Type type)
		{
			int count = _builtInDrawers.Count;
			for(int x = 0; x < count; ++x)
			{
				AReflectionDrawer drawer = _builtInDrawers[x];
				if(drawer.CanDraw(type))
				{
					return drawer;
				}
			}
			return _notSupportedDrawer;
		}

		public FieldInfo[] GetFields(Type type)
		{
			return type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		}
	}
}
