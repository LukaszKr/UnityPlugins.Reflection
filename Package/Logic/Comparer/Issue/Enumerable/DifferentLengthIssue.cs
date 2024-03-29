﻿namespace ProceduralLevel.Reflection.Logic
{
	public class DifferentLengthIssue : ADetectedIssue, IDebugPairIssue
	{
		public readonly int LeftLength;
		public readonly int RightLength;

		public override string Name => "Different Length";
		public string DebugLeft => LeftLength.ToString();
		public string DebugRight => RightLength.ToString();

		public DifferentLengthIssue(ObjectIssue parent, string key, int leftLength, int rightLength)
			: base(parent, key)
		{
			LeftLength = leftLength;
			RightLength = rightLength;
		}

		protected override string ToStringImpl()
		{
			return $"[{LeftLength} =/= {RightLength}]";
		}
	}
}
