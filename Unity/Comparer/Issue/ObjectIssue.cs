using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public class ObjectIssue : ADetectedIssue
	{
		public readonly List<ObjectIssue> Nodes = new List<ObjectIssue>();
		public readonly List<ADetectedIssue> Issues = new List<ADetectedIssue>();

		public override string Type => "Object";

		public ObjectIssue(ObjectIssue parent, string key)
			: base(parent, key)
		{
		}

		protected override string ToStringImpl()
		{
			return $"[{nameof(Nodes)}: {Nodes.Count}, {nameof(Issues)}: {Issues.Count}]";
		}
	}
}
