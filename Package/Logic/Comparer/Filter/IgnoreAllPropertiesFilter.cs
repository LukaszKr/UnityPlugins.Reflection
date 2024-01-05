using System.Reflection;

namespace ProceduralLevel.Reflection.Logic
{
	public class IgnoreAllPropertiesFilter : IPropertyFilter
	{
		public bool ShouldIgnore(object parent, PropertyInfo property)
		{
			return true;
		}
	}
}
