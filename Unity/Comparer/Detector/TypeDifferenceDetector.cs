using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class TypeDifferenceDetector : AIssueDetector
	{
		public override ADetectedIssue Detect(ObjectIssue parent, string path, object a, object b)
		{
			if(a == null || b == null)
			{
				return null;
			}

			Type typeA = a.GetType();
			Type typeB = b.GetType();
			if(typeA != typeB)
			{
				return new DifferentTypeIssue(parent, path, typeA, typeB);
			}
			return null;
		}
	}
}
