using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public interface ITypeFilter
	{
		bool IsValid(MemberInfo member, Type type);
		bool IsValid(Type type);

		bool IsValid(FieldInfo field)
		{
			return IsValid(field, field.FieldType);
		}

		bool IsValid(PropertyInfo property)
		{
			return IsValid(property, property.PropertyType);
		}
	}
}
