using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class IgnorePrimitiveTypeFilter : ITypeFilter
	{
		public bool IsValid(MemberInfo member, Type type)
		{
			return IsValid(type);
		}

		public bool IsValid(Type type)
		{
			return !type.IsPrimitive;
		}
	}
}
