using System;
using System.Collections.Generic;

namespace UnityPlugins.Reflection.Logic
{
	public class ReflectionComparer
	{
		public readonly TypeAnalyzer Analyzer = new TypeAnalyzer();
		public readonly List<AComparerEvaluator> Evaluators = new List<AComparerEvaluator>();
		public readonly List<AComparerHandler> Comparers = new List<AComparerHandler>();

		public readonly SharedInstanceComparerEvaluator SharedInstanceEvaluator;

		private HashSet<object> m_VisitedObjects = new HashSet<object>();

		public ReflectionComparer()
		{
			Analyzer.Filters.Add(new ReflectionIgnoreTypeFilter());

			Evaluators.Add(new DifferentTypeComparerEvaluator());
			Evaluators.Add(new DifferentValueComparerEvaluator());

			SharedInstanceEvaluator = new SharedInstanceComparerEvaluator();
			Evaluators.Add(SharedInstanceEvaluator);

			Comparers.Add(new PrimitiveComparerHandler(this));
			Comparers.Add(new DictionaryComparerHandler(this));
			Comparers.Add(new CollectionComparerHandler(this));
			Comparers.Add(new ObjectComparerHandler(this));
		}

		private void Reset()
		{
			m_VisitedObjects.Clear();

			foreach(AComparerEvaluator evaluator in Evaluators)
			{
				evaluator.Reset();
			}
		}

		public ComparisionGroup Compare(object left, object right)
		{
			ComparisionGroup result = CompareInternal(null, "/", left, right);
			Reset();
			return result;
		}

		public ComparisionGroup Compare(string key, object left, object right)
		{
			ComparisionGroup result = CompareInternal(null, key, left, right);
			Reset();
			return result;
		}

		internal ComparisionGroup CompareInternal(ComparisionGroup parent, string key, object left, object right)
		{
			Type type = null;
			if(left != null)
			{
				type = left.GetType();
			}
			else if(right != null)
			{
				type = right.GetType();
			}

			if(type == null)
			{
				return null;
			}

			ComparisionGroup result = new ComparisionGroup(parent, key, left, right);
			if(SharedInstanceEvaluator.Evaluate(result))
			{
				return result;
			}

			if(type.IsClass)
			{
				if(left != null && !m_VisitedObjects.Add(left))
				{
					return null;
				}

				if(right != null && !m_VisitedObjects.Add(right))
				{
					return null;
				}
			}

			int count = Comparers.Count;
			for(int x = 0; x < count; ++x)
			{
				AComparerHandler comparer = Comparers[x];
				if(comparer.CanHandle(type))
				{
					comparer.Compare(result, type);
					break;
				}
			}

			if(result.Entries.Count > 0 || result.SubResults.Count > 0)
			{
				parent?.SubResults.Add(result);
				return result;
			}

			return null;
		}

		internal bool Evaluate(ComparisionGroup result)
		{
			bool detected = false;
			int count = Evaluators.Count;
			for(int x = 0; x < count; ++x)
			{
				AComparerEvaluator evaluator = Evaluators[x];
				detected |= evaluator.Evaluate(result);
			}
			return detected;
		}
	}
}
