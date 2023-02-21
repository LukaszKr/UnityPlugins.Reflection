namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public interface IValueFilter : IFilter
	{
		bool ShouldIgnore(object value);
	}
}
