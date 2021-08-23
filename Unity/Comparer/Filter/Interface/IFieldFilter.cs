using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public interface IFieldFilter
	{
		bool ShouldIgnore(object parent, FieldInfo field);
	}
}
