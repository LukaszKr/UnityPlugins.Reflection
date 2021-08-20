namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class SharedObjectIssue : ADetectedIssue
	{
		public readonly object Value;

		public SharedObjectIssue(ObjectIssue parent, string key, object value)
			: base(parent, key)
		{
			Value = value;
		}

		protected override string ToStringImpl()
		{
			return $"[Shared: {Value.GetType().Name}]";
		}
	}
}
