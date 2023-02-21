namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public interface IValueFilter : IFilter
	{
		bool ShouldIgnore(object value);
	}
}
