using System.Collections;

namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public class CompareEnumerableHandler : ACompareHandler<IEnumerable>
	{
		public override bool Exclusive => true;

		protected override bool CompareImpl(ReflectionComparer comparer, ObjectIssue parent, string path, IEnumerable left, IEnumerable right)
		{
			bool foundIssue = false;

			IEnumerator leftEnumerator = left.GetEnumerator();
			IEnumerator rightEnumerator = right.GetEnumerator();

			int oldIssueCount = parent.Issues.Count;
			int leftCount = 0;
			int rightCount = 0;
			do
			{
				bool leftProgressed = leftEnumerator.MoveNext();
				if(leftProgressed)
				{
					++leftCount;
				}
				bool rightProgressed = rightEnumerator.MoveNext();
				if(rightProgressed)
				{
					++rightCount;
				}

				if(leftCount != rightCount)
				{
					parent.Issues.Insert(oldIssueCount, new DifferentLengthIssue(parent, "Length", leftCount, rightCount));
					return true;
				}

				if(!leftProgressed || !rightProgressed)
				{
					return foundIssue;
				}

				object leftValue = leftEnumerator.Current;
				object rightValue = rightEnumerator.Current;
				string elementPath = $"[{leftCount-1}]";
				foundIssue |= comparer.Compare(parent, elementPath, leftValue, rightValue);
			}
			while(true);
		}
	}
}
