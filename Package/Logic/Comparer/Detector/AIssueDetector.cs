namespace ProceduralLevel.Reflection.Logic
{
	public abstract class AIssueDetector
	{
		public abstract bool Compare(ObjectIssue parent, string key, object left, object right);
	}
}
