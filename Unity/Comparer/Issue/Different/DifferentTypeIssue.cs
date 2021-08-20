using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DifferentTypeIssue : ADetectedIssue
	{
		public readonly Type LeftType;
		public readonly Type RightType;

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
