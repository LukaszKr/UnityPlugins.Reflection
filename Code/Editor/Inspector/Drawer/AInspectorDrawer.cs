using System;

namespace UnityPlugins.Reflection.Editor
{
	public abstract class AInspectorDrawer
	{
		protected ReflectionInspector m_Inspector;

		public abstract bool CanDraw(Type type);

		public object Draw(ReflectionInspector inspector, object parent, Type type, string label, object value)
		{
			m_Inspector = inspector;
			return Draw(parent, type, label, value);
		}

		protected abstract object Draw(object parent, Type type, string label, object value);
	}
}
