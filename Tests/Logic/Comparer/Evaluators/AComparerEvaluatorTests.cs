using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Evaluators
{
	internal abstract class AComparerEvaluatorTests<TEvaluator>
		where TEvaluator : AComparerEvaluator, new()
	{
		protected TEvaluator m_Evaluator;

		[SetUp]
		public void Setup()
		{
			m_Evaluator = new TEvaluator();
		}

		#region Helpers
		protected void AssertEvaluate<TEntry>(bool isValid, object left, object right)
			where TEntry : AComparisionResultEntry
		{
			ComparisionGroup group = new ComparisionGroup(null, "", left, right);
			m_Evaluator.Evaluate(group);
			if(isValid)
			{
				Assert.AreEqual(0, group.Entries.Count);
			}
			else
			{
				Assert.AreEqual(1, group.Entries.Count);
				Assert.AreEqual(typeof(TEntry), group.Entries[0].GetType());
			}
		}
		#endregion
	}
}
