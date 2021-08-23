namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public abstract class ADetectedIssue
	{
		public readonly ObjectIssue Parent;
		public readonly string Key;
		
		public abstract string Type { get; }

		public ADetectedIssue(ObjectIssue parent, string key)
		{
			Parent = parent;
			Key = key;
		}

		public string GetPath()
		{
			if(Parent != null)
			{
				return $"{Parent.GetPath()}.{Key}";
			}
			return $"{Key}";
		}

		public override string ToString()
		{
			return $"[{GetType().Name}({Key})] {ToStringImpl()}";
		}

		protected abstract string ToStringImpl();
	}
}
