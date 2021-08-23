using System.Collections;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class CompareDictionaryHandler : ACompareHandler<IDictionary>
	{
		public override bool Exclusive => true;

		protected override bool CompareImpl(ReflectionComparer comparer, ObjectIssue parent, string path, IDictionary left, IDictionary right)
		{
			bool foundIssue = false;

			int leftCount = left.Count;
			int rightCount = right.Count;
			if(leftCount != rightCount)
			{
				parent.Issues.Add(new DifferentLengthIssue(parent, path, leftCount, rightCount));
				foundIssue = true;
			}

			foreach(object key in left.Keys)
			{
				object leftValue = left[key];
				string keyPath = $"{path}[{key}]";
				if(right.Contains(key))
				{
					object rightValue = right[key];
					foundIssue |= comparer.Compare(parent, keyPath, leftValue, rightValue);
				}
				else
				{
					parent.Issues.Add(new DifferentValueIssue(parent, keyPath, leftValue, null));
					foundIssue = true;
				}
			}

			foreach(object key in right.Keys)
			{
				object rightValue = right[key];
				if(!left.Contains(key))
				{
					string keyPath = $"{path}[{key}]";
					parent.Issues.Add(new DifferentValueIssue(parent, keyPath, null, rightValue));
					foundIssue = true;
				}
			}

			return foundIssue;
		}
	}
}
