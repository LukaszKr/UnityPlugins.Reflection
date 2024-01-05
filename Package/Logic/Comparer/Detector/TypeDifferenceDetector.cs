using System;

namespace ProceduralLevel.Reflection.Logic
{
	public class TypeDifferenceDetector : AIssueDetector
	{
		public override bool Compare(ObjectIssue parent, string path, object left, object right)
		{
			if(left == null || right == null)
			{
				return false;
			}

			Type leftType = left.GetType();
			Type rightType = right.GetType();
			if(leftType != rightType)
			{
				parent.Issues.Add(new DifferentTypeIssue(parent, path, leftType, rightType));
				return true;
			}
			return false;
		}
	}
}
