﻿using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class IgnoreMemberByNameFilter : ATypeFilter
	{
		private readonly string m_MemberName;

		public IgnoreMemberByNameFilter(Type parentType, string memberName)
			: base(parentType)
		{
			m_MemberName = memberName;
		}

		public override bool ShouldIgnore(object value)
		{
			return false;
		}

		protected override bool OnShouldIgnore(object parent, FieldInfo field)
		{
			return field.Name == m_MemberName;
		}

		protected override bool OnShouldIgnore(object parent, PropertyInfo property)
		{
			return property.Name == m_MemberName;
		}
	}
}
