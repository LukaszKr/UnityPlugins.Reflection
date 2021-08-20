using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class IgnoreMemberByTypeFilter : ATypeFilter
	{
		private readonly Type m_MemberType;

		public IgnoreMemberByTypeFilter(Type parentType, Type memberType)
			: base(parentType)
		{
			m_MemberType = memberType;
		}

		public override bool ShouldIgnore(object value)
		{
			return false;
		}

		protected override bool OnShouldIgnore(object parent, FieldInfo field)
		{
			return m_MemberType.IsAssignableFrom(field.FieldType);
		}

		protected override bool OnShouldIgnore(object parent, PropertyInfo property)
		{
			return m_MemberType.IsAssignableFrom(property.PropertyType);
		}
	}
}
