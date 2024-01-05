using System.Reflection;

namespace ProceduralLevel.Reflection.Logic
{
	public interface IPropertyFilter : IFilter
	{
		bool ShouldIgnore(object parent, PropertyInfo property);
	}
}
