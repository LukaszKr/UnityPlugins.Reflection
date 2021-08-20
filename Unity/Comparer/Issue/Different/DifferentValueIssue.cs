namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DifferentValueIssue : ADetectedIssue
	{
		public readonly object ValueA;
		public readonly object ValueB;

		public DifferentValueIssue(ObjectIssue parent, string key, object valueA, object valueB)
			: base(parent, key)
		{
			ValueA = valueA;
			ValueB = valueB;
		}

		protected override string ToStringImpl()
		{
			return $"[{ValueA} =/= {ValueB}]";
		}
	}
}
