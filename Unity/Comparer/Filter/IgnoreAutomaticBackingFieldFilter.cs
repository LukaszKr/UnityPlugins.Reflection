using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public class IgnoreAutomaticBackingFieldFilter : AFilter, IFieldFilter
	{
		public bool ShouldIgnore(object parent, FieldInfo field)
		{
			return field.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
		}
	}
}
