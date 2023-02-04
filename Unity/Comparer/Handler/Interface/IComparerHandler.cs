namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public interface IComparerHandler
	{
		bool Exclusive { get; }

		bool Compare(ReflectionComparer comparer, ObjectIssue parent, string path, object left, object right, out bool processed);
	}
}
