using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class IgnoreTypeFilter : ITypeFilter
	{
		public readonly HashSet<Type> Ignore = new HashSet<Type>();

		public bool IsValid(MemberInfo member, Type type)
		{
			return IsValid(type);
		}

		public bool IsValid(Type type)
		{
			return !Ignore.Contains(type);
		}
	}
}
