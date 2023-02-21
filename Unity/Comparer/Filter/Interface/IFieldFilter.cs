using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public interface IFieldFilter : IFilter
	{
		bool ShouldIgnore(object parent, FieldInfo field);
	}
}
