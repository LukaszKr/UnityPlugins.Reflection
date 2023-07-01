namespace ProceduralLevel.Reflection.Logic
{
	public class SharedObjectIssue : ADetectedIssue, IDebugValueIssue
	{
		public readonly object Value;

		public override string Type => "Shared";
		public string DebugValue => Value.ToString();

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
