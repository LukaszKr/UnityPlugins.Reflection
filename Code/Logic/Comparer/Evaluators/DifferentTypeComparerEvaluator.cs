using System;

namespace UnityPlugins.Reflection.Logic
{
	public class DifferentTypeComparerEvaluator : AComparerEvaluator
	{
		public override void Reset()
		{
		}

		public override bool Evaluate(ComparisionGroup group)
		{
			if(group.Left == null || group.Right == null)
			{
				return false;
			}
			Type leftType = group.Left.GetType();
			Type rightType = group.Right.GetType();
			if(leftType != rightType)
			{
				group.Entries.Add(new DifferentTypeResultEntry(leftType, rightType));
				return true;
			}
			return false;
		}
	}
}
