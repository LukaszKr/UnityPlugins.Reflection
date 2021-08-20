using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ObjectIssue : ADetectedIssue
	{
		public readonly List<ObjectIssue> Nodes = new List<ObjectIssue>();
		public readonly List<ADetectedIssue> Differences = new List<ADetectedIssue>();

		public ObjectIssue(ObjectIssue parent, string key)
			: base(parent, key)
		{
		}

		protected override string ToStringImpl()
		{
			return $"[Diffs: {Differences.Count}]";
		}
	}
}
