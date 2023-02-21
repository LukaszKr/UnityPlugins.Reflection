using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public interface IPropertyFilter : IFilter
	{
		bool ShouldIgnore(object parent, PropertyInfo property);
	}
}
