namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public abstract class ACompareHandler<TObject> : IComparerHandler
		where TObject : class
	{
		public abstract bool Exclusive { get; }

		public bool Compare(ReflectionComparer comparer, ObjectIssue parent, string path, object left, object right, out bool processed)
		{
			TObject castedLeft = left as TObject;
			TObject castedRight = right as TObject;
			if(castedLeft != null && castedRight != null)
			{
				processed = true;
				return CompareImpl(comparer, parent, path, castedLeft, castedRight);
			}
			processed = false;
			return false;
		}

		protected abstract bool CompareImpl(ReflectionComparer comparer, ObjectIssue parent, string path, TObject left, TObject right);
	}
}
