using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public interface IFieldFilter : IFilter
	{
		bool ShouldIgnore(object parent, FieldInfo field);
	}
}
