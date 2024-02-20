namespace ProceduralLevel.Reflection.Logic
{
	public class DifferentValueIssue : ADetectedIssue, IDebugPairIssue
	{
		public readonly object LeftValue;
		public readonly object RightValue;

		public override string Name => "Different Value";
		public string DebugLeft => LeftValue?.ToString();
		public string DebugRight => RightValue?.ToString();

		public DifferentValueIssue(ObjectIssue parent, string key, object leftValue, object rightValue)
			: base(parent, key)
		{
			LeftValue = leftValue;
			RightValue = rightValue;
		}

		protected override string ToStringImpl()
		{
			return $"[{LeftValue} =/= {RightValue}]";
		}
	}
}
