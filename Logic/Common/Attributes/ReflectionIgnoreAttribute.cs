using System;

namespace UnityPlugins.Reflection.Logic
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
	public class ReflectionIgnoreAttribute : Attribute
	{
	}
}
