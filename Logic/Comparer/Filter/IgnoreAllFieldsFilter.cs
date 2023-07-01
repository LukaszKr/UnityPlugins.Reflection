using System.Reflection;

namespace ProceduralLevel.Reflection.Logic
{
	public class IgnoreAllFieldsFilter : IFieldFilter
	{
		public bool ShouldIgnore(object parent, FieldInfo field)
		{
			return true;
		}
	}
}
