using System.Reflection;

namespace ProceduralLevel.Reflection.Logic
{
	public interface IFieldFilter : IFilter
	{
		bool ShouldIgnore(object parent, FieldInfo field);
	}
}
