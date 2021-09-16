using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public abstract class AReflectionDrawer
	{
		private ReflectionInspector m_Inspector;

		protected ReflectionInspector Inspector => m_Inspector;
		protected ReflectionInspectorLayout Layout => m_Inspector.Layout;

		public abstract bool CanDraw(Type type);

		public void Draw(ReflectionInspector inspector, object obj, FieldInfo field)
		{
			m_Inspector = inspector;
			OnDraw(obj, field);
		}

		protected abstract void OnDraw(object obj, FieldInfo field);
	}
}
