﻿using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public class IgnoreMemberFilter : ATypeFilter
	{
		private readonly MemberInfo m_Member;

		public IgnoreMemberFilter(Type parentType, MemberInfo member)
			: base(parentType)
		{
			m_Member = member;
		}

		protected override bool OnShouldIgnore(object parent, FieldInfo field)
		{
			return m_Member == field;
		}

		protected override bool OnShouldIgnore(object parent, PropertyInfo property)
		{
			return m_Member == property;
		}
	}
}
