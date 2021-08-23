using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DifferentTypeIssue : ADetectedIssue, IDebugPairIssue
	{
		public readonly Type LeftType;
		public readonly Type RightType;

		public override string Type => "Type";
		public string DebugLeft => LeftType.Name;
		public string DebugRight => RightType.Name;

		public DifferentTypeIssue(ObjectIssue parent, string path, Type leftType, Type rightType)
			: base(parent, path)
		{
			LeftType = leftType;
			RightType = rightType;
		}

		protected override string ToStringImpl()
		{
			return $"[{LeftType.Name} =/= {RightType.Name}]";
		}
	}
}
