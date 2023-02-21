using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public class IgnoreAllPropertiesFilter : IPropertyFilter
	{
		public bool ShouldIgnore(object parent, PropertyInfo property)
		{
			return true;
		}
	}
}
