using System;

namespace UnityPlugins.Reflection.Logic
{
	public class DifferentTypeResultEntry : AComparisionResultEntry
	{
		public readonly Type Left;
		public readonly Type Right;

		public DifferentTypeResultEntry(Type left, Type right)
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
