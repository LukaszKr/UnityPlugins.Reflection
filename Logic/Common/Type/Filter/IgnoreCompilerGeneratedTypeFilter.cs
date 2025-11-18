using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityPlugins.Reflection.Logic
{
	public class IgnoreCompilerGeneratedTypeFilter : ITypeFilter
	{
		public bool IsValid(MemberInfo member, Type type)
		{
			return !member.IsDefined(typeof(CompilerGeneratedAttribute), true);
		}

		public bool IsValid(Type type)
		{
			return !type.IsDefined(typeof(CompilerGeneratedAttribute), true);
		}
	}
}
