using System;
using System.Collections;

namespace UnityPlugins.Reflection.Logic
{
	public class DictionaryComparerHandler : AComparerHandler
	{
		public DictionaryComparerHandler(ReflectionComparer comparer)
			: base(comparer)
		{
		}

		public override bool CanHandle(Type type)
		{
			return typeof(IDictionary).IsAssignableFrom(type);
		}

		public override void Compare(ComparisionGroup group, Type type)
		{
			IDictionary leftDict = (IDictionary)group.Left;
			IDictionary rightDict = (IDictionary)group.Right;

			if(leftDict == null)
			{
				if(rightDict == null)
				{
					return;
				}
				group.Entries.Add(new DifferentValueResultEntry(group.Left, group.Right));
				return;
			}
			else if(rightDict == null)
			{
				group.Entries.Add(new DifferentValueResultEntry(group.Left, group.Right));
				return;
			}

			if(leftDict == null || rightDict == null)
			{
				return;
			}

			int leftCount = leftDict.Count;
			int rightCount = rightDict.Count;

			if(leftCount != rightCount)
			{
				group.Entries.Add(new DifferentLengthResultEntry(leftCount, rightCount));
				return;
			}

			CompareDictionaries(group, leftDict, rightDict, true);
			CompareDictionaries(group, rightDict, leftDict, false);
		}

		private void CompareDictionaries(ComparisionGroup group, IDictionary dictA, IDictionary dictB, bool compareValues)
		{
			IEnumerator leftEnumerator = dictA.GetEnumerator();

			while(leftEnumerator.MoveNext())
			{
				DictionaryEntry leftValue = (DictionaryEntry)leftEnumerator.Current;
				object key = leftValue.Key;
				if(!dictB.Contains(key))
				{
					group.Entries.Add(new MissingKeyResultEntry(dictA, dictB, key));
					continue;
				}
				if(compareValues)
				{
					object rightValue = dictB[key];
					m_Comparer.CompareInternal(group, $"[{key}]", leftValue.Value, rightValue);
				}
			}
		}
	}
}
