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
			object inspected = field.GetValue(obj);
			inspected = OnDraw(field.Name, inspected, field.FieldType);
			field.SetValue(obj, inspected);
		}

		public object Draw(ReflectionInspector inspector, string label, object value, Type type)
		{
			m_Inspector = inspector;
			return OnDraw(label, value, type);
		}

		protected abstract object OnDraw(string label, object value, Type type);
	}
}
