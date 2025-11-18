using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class AttributeTypeFilter<TAttribute> : ITypeFilter
		where TAttribute : Attribute
	{
		public bool IsValid(MemberInfo member, Type type)
		{
			if(!IsValid(type))
			{
				return false;
			}

			if(member.IsDefined(typeof(TAttribute), true))
			{
				return false;
			}

			return true;
		}

		public bool IsValid(Type type)
		{
			return !type.IsDefined(typeof(TAttribute), true);
		}
	}
}
