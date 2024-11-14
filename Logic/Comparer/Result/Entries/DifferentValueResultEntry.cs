namespace UnityPlugins.Reflection.Logic
{
	public class DifferentValueResultEntry : AComparisionResultEntry
	{
		public readonly object Left;
		public readonly object Right;

		public DifferentValueResultEntry(object left, object right)
		{
			Left = left;
			Right = right;
		}

		protected override string ToStringImpl()
		{
			return $"{Left} =/= {Right}";
		}
	}
}
