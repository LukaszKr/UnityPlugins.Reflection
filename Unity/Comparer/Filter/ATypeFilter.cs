using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public abstract class ATypeFilter : AFilter, IPropertyFilter, IFieldFilter
	{
		private readonly Type m_ParentType;

		protected ATypeFilter(Type parentType)
		{
			m_ParentType = parentType;
		}

		public bool ShouldIgnore(object parent, FieldInfo field)
		{
			if(m_ParentType.IsAssignableFrom(parent.GetType()))
			{
				return OnShouldIgnore(parent, field);
			}
			return false;
		}

		public bool ShouldIgnore(object parent, PropertyInfo property)
		{
			if(m_ParentType.IsAssignableFrom(parent.GetType()))
			{
				return OnShouldIgnore(parent, property);
			}
			return false;
		}

		protected abstract bool OnShouldIgnore(object parent, FieldInfo field);
		protected abstract bool OnShouldIgnore(object parent, PropertyInfo property);
	}
}
