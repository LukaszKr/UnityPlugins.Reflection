using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public interface IFieldFilter
	{
		bool ShouldIgnore(object parent, FieldInfo field);
	}
}
