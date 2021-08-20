using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ObjectDifference : ADifference
	{
		public readonly List<ObjectDifference> Nodes = new List<ObjectDifference>();
		public readonly List<ADifference> Differences = new List<ADifference>();

		public ObjectDifference(ObjectDifference parent, string key)
			: base(parent, key)
		{
		}

		protected override string ToStringImpl()
		{
			return $"[Diffs: {Differences.Count}]";
		}
	}
}
