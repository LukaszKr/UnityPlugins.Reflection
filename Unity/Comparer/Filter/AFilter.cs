using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public abstract class AFilter
	{
		public abstract bool ShouldIgnore(object value);
		public abstract bool ShouldIgnore(object parent, FieldInfo field);
		public abstract bool ShouldIgnore(object parent, PropertyInfo property);
	}
}
