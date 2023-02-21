using System;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct)]
	public class ReflectionIgnoreAttribute : Attribute
	{
	}
}
