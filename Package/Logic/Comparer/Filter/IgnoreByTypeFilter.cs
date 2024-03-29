﻿using System;
using System.Reflection;

namespace ProceduralLevel.Reflection.Logic
{
	public class IgnoreByTypeFilter : IValueFilter, IFieldFilter, IPropertyFilter
	{
		private readonly Type m_IgnoredType;

		public IgnoreByTypeFilter(Type ignoredType)
		{
			m_IgnoredType = ignoredType;
		}

		public bool ShouldIgnore(object value)
		{
			return value != null && m_IgnoredType.IsAssignableFrom(value.GetType());
		}

		public bool ShouldIgnore(object parent, FieldInfo field)
		{
			return m_IgnoredType.IsAssignableFrom(field.FieldType);
		}

		public bool ShouldIgnore(object parent, PropertyInfo property)
		{
			return m_IgnoredType.IsAssignableFrom(property.PropertyType);
		}
	}
}
