namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public abstract class ACompareHandler<TObject> : IComparerHandler
		where TObject : class
	{
		public abstract bool Exclusive { get; }

		public bool Compare(ReflectionComparer comparer, ObjectIssue parent, string path, object left, object right)
		{
			TObject castedLeft = left as TObject;
			TObject castedRight = right as TObject;
			if(castedLeft != null && castedRight != null)
			{
				return CompareImpl(comparer, parent, path, castedLeft, castedRight);
			}
			return false;
		}

		protected abstract bool CompareImpl(ReflectionComparer comparer, ObjectIssue parent, string path, TObject left, TObject right);
	}
}
