namespace UnityPlugins.Reflection.Logic
{
	public class DifferentValueComparerEvaluator : AComparerEvaluator
	{
		public override void Reset()
		{
		}

		public override bool Evaluate(ComparisionGroup group)
		{
			if(Equals(group.Left, group.Right))
			{
				return false;
			}

			group.Entries.Add(new DifferentValueResultEntry(group.Left, group.Right));
			return true;
		}
	}
}
