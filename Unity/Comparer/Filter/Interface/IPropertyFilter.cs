using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public interface IPropertyFilter : IFilter
	{
		bool ShouldIgnore(object parent, PropertyInfo property);
	}
}
