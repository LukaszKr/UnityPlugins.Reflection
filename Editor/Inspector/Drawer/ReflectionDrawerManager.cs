using System;
using System.Collections.Generic;

namespace UnityPlugins.Reflection.Editor
{
	public class ReflectionDrawerManager
	{
		private readonly Dictionary<Type, AInspectorDrawer> m_SpecificDrawers = new Dictionary<Type, AInspectorDrawer>();
		private readonly List<AInspectorDrawer> m_GenericDrawers = new List<AInspectorDrawer>();

		public void AddSpecificDrawer<TValue>(AValueInspectorDrawer<TValue> drawer)
		{
			m_SpecificDrawers.Add(typeof(TValue), drawer);
		}

		public void AddSpecificDrawer(Type type, AInspectorDrawer drawer)
		{
			m_SpecificDrawers.Add(type, drawer);
		}

		public void AddGenericDrawer(AInspectorDrawer drawer)
		{
			m_GenericDrawers.Insert(0, drawer);
		}

		public AInspectorDrawer GetDrawer(Type type)
		{
			AInspectorDrawer drawer;
			if(m_SpecificDrawers.TryGetValue(type, out drawer))
			{
				return drawer;
			}

			int count = m_GenericDrawers.Count;
			for(int x = 0; x < count; ++x)
			{
				drawer = m_GenericDrawers[x];
				if(drawer.CanDraw(type))
				{
					return drawer;
				}
			}

			return null;
		}
	}
}
