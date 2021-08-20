namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public interface IValueFilter
	{
		bool ShouldIgnore(object value);
	}
}
