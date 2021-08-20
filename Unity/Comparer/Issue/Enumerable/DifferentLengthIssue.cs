namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DifferentLengthIssue : ADetectedIssue
	{
		public readonly int LeftLength;
		public readonly int RightLength;

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
