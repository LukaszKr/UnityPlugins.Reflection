using System;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public abstract class AInspectorDrawer
	{
		protected ReflectionInspector m_Inspector;

		public abstract bool CanDraw(Type type);

		public void Draw(ReflectionInspector inspector, object parent, AValueSource source)
		{
			m_Inspector = inspector;
			Draw(parent, source);
		}

		protected abstract void Draw(object parent, AValueSource source);
	}
}
