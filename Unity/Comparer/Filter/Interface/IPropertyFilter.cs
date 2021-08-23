using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public interface IPropertyFilter
	{
		bool ShouldIgnore(object parent, PropertyInfo property);
	}
}
