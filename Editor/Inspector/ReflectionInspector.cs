using System;
using System.Collections.Generic;
using System.Reflection;
using ProceduralLevel.UnityPlugins.Common.Logic;
using ProceduralLevel.UnityPlugins.Reflection.Unity;
using UnityEditor.UIElements;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ReflectionInspector
	{
		public static readonly ReflectionInspector Instance = new ReflectionInspector();

		public readonly ReflectionInspectorLayout Layout = new ReflectionInspectorLayout();

		private readonly List<AReflectionDrawer> m_Drawers = new List<AReflectionDrawer>();
		private readonly NotSupportedReflectionDrawer m_NotSupportedDrawer = new NotSupportedReflectionDrawer();
		private readonly ObjectReflectionDrawer m_ObjectDrawer = new ObjectReflectionDrawer();

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
			m_Drawers.Add(drawer);
		}

		public TValue DrawLayout<TValue>(string label, TValue obj, float width)
		{
			TValue result = Draw(label, obj, width);
			GUILayoutUtility.GetRect(Layout.Current.width, Layout.Current.height);
			return result;
		}

		public TValue Draw<TValue>(string label, TValue obj, float width)
		{
			Layout.Start(width);

			AReflectionDrawer drawer = GetDrawer(typeof(TValue));
			return (TValue)drawer.Draw(this, label, obj, typeof(TValue));
		}

		public AReflectionDrawer GetDrawer(Type type)
		{
			int count = m_Drawers.Count;
			for(int x = 0; x < count; ++x)
			{
				AReflectionDrawer drawer = m_Drawers[x];
				if(drawer.CanDraw(type))
				{
					return drawer;
				}
			}
			return m_NotSupportedDrawer;
		}

		public FieldInfo[] GetFields(Type type)
		{
			return type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		}
	}
}
