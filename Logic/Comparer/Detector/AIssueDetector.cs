namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public abstract class AIssueDetector
	{
		public abstract bool Compare(ObjectIssue parent, string key, object left, object right);
	}
}
