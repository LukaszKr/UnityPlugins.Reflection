using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class TypeDifferenceDetector : AIssueDetector
	{
		public override ADetectedIssue Detect(ObjectIssue parent, string path, object left, object right)
		{
			if(left == null || right == null)
			{
				return null;
			}

			Type leftType = left.GetType();
			Type rightType = right.GetType();
			if(leftType != rightType)
			{
				return new DifferentTypeIssue(parent, path, leftType, rightType);
			}
			return null;
		}
	}
}
