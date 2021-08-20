using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DifferentTypeIssue : ADetectedIssue
	{
		public readonly Type TypeA;
		public readonly Type TypeB;

		public DifferentTypeIssue(ObjectIssue parent, string path, Type typeA, Type typeB)
			: base(parent, path)
		{
			TypeA = typeA;
			TypeB = typeB;
		}

		protected override string ToStringImpl()
		{
			return $"[{TypeA.Name} =/= {TypeB.Name}]";
		}
	}
}
