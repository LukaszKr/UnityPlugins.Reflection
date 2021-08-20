namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ValueDifference : ADifference
	{
		public readonly object ValueA;
		public readonly object ValueB;

		public ValueDifference(ObjectDifference parent, string key, object valueA, object valueB)
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
