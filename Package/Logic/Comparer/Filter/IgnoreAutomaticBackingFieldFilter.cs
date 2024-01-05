using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProceduralLevel.Reflection.Logic
{
	public class IgnoreAutomaticBackingFieldFilter : IFieldFilter
	{
		public bool ShouldIgnore(object parent, FieldInfo field)
		{
			return field.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
		}
	}
}
