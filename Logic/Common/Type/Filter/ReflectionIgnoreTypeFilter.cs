using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class ReflectionIgnoreTypeFilter : ITypeFilter
	{
		public bool IsValid(MemberInfo member, Type type)
		{
			if(!IsValid(type))
			{
				return false;
			}

			if(member.IsDefined(typeof(ReflectionIgnoreAttribute), true))
			{
				return false;
			}

			return true;
		}

		public bool IsValid(Type type)
		{
			return !type.IsDefined(typeof(ReflectionIgnoreAttribute), true);
		}
	}
}
