namespace UnityPlugins.Reflection.Logic
{
	public class DifferentLengthResultEntry : AComparisionResultEntry
	{
		public readonly int Left;
		public readonly int Right;

		public DifferentLengthResultEntry(int left, int right)
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
