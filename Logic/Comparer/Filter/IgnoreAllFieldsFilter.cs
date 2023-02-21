using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public class IgnoreAllFieldsFilter : IFieldFilter
	{
		public bool ShouldIgnore(object parent, FieldInfo field)
		{
			return true;
		}
	}
}
