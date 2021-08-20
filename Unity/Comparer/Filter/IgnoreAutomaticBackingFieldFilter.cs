using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class IgnoreAutomaticBackingFieldFilter : AFilter
	{
		public override bool ShouldIgnore(object value)
		{
			return false;
		}

		public override bool ShouldIgnore(object parent, FieldInfo field)
		{
			return field.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
		}

		public override bool ShouldIgnore(object parent, PropertyInfo property)
		{
			return false;
		}
	}
}
