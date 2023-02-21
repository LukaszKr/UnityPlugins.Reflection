using System;

namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct)]
	public class ReflectionIgnoreAttribute : Attribute
	{
	}
}
