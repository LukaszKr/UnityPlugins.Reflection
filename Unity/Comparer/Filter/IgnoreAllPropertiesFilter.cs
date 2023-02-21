using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public class IgnoreAllPropertiesFilter : IPropertyFilter
	{
		public bool ShouldIgnore(object parent, PropertyInfo property)
		{
			return true;
		}
	}
}
