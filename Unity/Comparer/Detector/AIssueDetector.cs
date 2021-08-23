namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public abstract class AIssueDetector
	{
		public abstract bool Compare(ObjectIssue parent, string key, object left, object right);
	}
}
