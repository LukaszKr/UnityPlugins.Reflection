using System;
using System.Collections;

namespace UnityPlugins.Reflection.Logic
{
	public class CollectionComparerHandler : AComparerHandler
	{
		public CollectionComparerHandler(ReflectionComparer comparer)
			: base(comparer)
		{
		}

		public override bool CanHandle(Type type)
		{
			return typeof(ICollection).IsAssignableFrom(type);
		}

		public override void Compare(ComparisionGroup group, Type type)
		{
			ICollection leftCollection = group.Left as ICollection;
			ICollection rightCollection = group.Right as ICollection;

			if(leftCollection == null)
			{
				if(rightCollection == null)
				{
					return;
				}
				group.Entries.Add(new DifferentValueResultEntry(group.Left, group.Right));
				return;
			}
			else if(rightCollection == null)
			{
				group.Entries.Add(new DifferentValueResultEntry(group.Left, group.Right));
				return;
			}

			int leftCount = leftCollection.Count;
			int rightCount = rightCollection.Count;

			if(leftCount != rightCount)
			{
				group.Entries.Add(new DifferentLengthResultEntry(leftCount, rightCount));
				return;
			}

			if(leftCollection == null || rightCollection == null)
			{
				return;
			}

			IEnumerator leftEnumerator = leftCollection.GetEnumerator();
			IEnumerator rightEnumerator = rightCollection.GetEnumerator();

			for(int x = 0; x < leftCount; ++x)
			{
				leftEnumerator.MoveNext();
				rightEnumerator.MoveNext();

				object leftValue = leftEnumerator.Current;
				object rightValue = rightEnumerator.Current;
				m_Comparer.CompareInternal(group, $"[{x}]", leftValue, rightValue);
			}
		}
	}
}
