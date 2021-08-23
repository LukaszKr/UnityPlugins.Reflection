namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public interface IValueFilter
	{
		bool ShouldIgnore(object value);
	}
}
