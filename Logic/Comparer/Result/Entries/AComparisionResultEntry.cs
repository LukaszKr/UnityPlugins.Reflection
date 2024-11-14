namespace UnityPlugins.Reflection.Logic
{
	public abstract class AComparisionResultEntry
	{
		public override string ToString()
		{
			return $"[{GetType().Name}, {ToStringImpl()}]";
		}

		protected abstract string ToStringImpl();
	}
}
