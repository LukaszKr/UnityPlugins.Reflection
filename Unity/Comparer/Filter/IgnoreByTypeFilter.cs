using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class IgnoreByTypeFilter : AFilter
	{
		private readonly Type m_IgnoredType;

		public IgnoreByTypeFilter(Type ignoredType)
		{
			m_IgnoredType = ignoredType;
		}

		public override bool ShouldIgnore(object value)
		{
			return value != null && m_IgnoredType.IsAssignableFrom(value.GetType());
		}

		public override bool ShouldIgnore(object parent, FieldInfo field)
		{
			return m_IgnoredType.IsAssignableFrom(field.FieldType);
		}

		public override bool ShouldIgnore(object parent, PropertyInfo property)
		{
			return m_IgnoredType.IsAssignableFrom(property.PropertyType);
		}
	}
}
