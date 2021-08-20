namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public abstract class ADifference
	{
		public readonly ObjectDifference Parent;
		public readonly string Key;

		public ADifference(ObjectDifference parent, string key)
		{
			Parent = parent;
			Key = key;
		}

		public string GetPath()
		{
			if(Parent != null)
			{
				return $"/{Parent.GetPath()}/{Key}";
			}
			return $"/{Key}";
		}

		public override string ToString()
		{
			return $"[{GetType().Name}: '{Key}'] {ToStringImpl()}";
		}

		protected abstract string ToStringImpl();
	}
}
