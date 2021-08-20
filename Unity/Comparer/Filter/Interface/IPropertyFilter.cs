using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public interface IPropertyFilter
	{
		bool ShouldIgnore(object parent, PropertyInfo property);
	}
}
