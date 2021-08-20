using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public abstract class AIssueDetector
	{
		public abstract ADetectedIssue Detect(ObjectIssue parent, string key, object left, object right);
	}
}
