namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DifferentValueIssue : ADetectedIssue
	{
		public readonly object LeftValue;
		public readonly object RightValue;

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
