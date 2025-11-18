namespace UnityPlugins.Reflection.Logic
{
	public class SharedInstanceResultEntry : AComparisionResultEntry
	{
		public readonly object Value;

		public SharedInstanceResultEntry(object value)
		{
			Value = value;
		}

		protected override string ToStringImpl()
		{
			return $"{Value}";
		}
	}
}
