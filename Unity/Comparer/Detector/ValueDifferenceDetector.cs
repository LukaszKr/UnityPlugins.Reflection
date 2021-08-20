using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ValueDifferenceDetector : AIssueDetector
	{
		public override ADetectedIssue Detect(ObjectIssue parent, string key, object a, object b)
		{
			Type type;
			if(a == null)
			{
				if(b == null)
				{
					return null;
				}
				type = b.GetType();
			}
			else
			{
				type = a.GetType();
			}
			if(type.IsPrimitive || typeof(string).IsAssignableFrom(type))
			{
				if(!Equals(a, b))
				{
					return new DifferentValueIssue(parent, key, a, b);
				}
			}
			else
			{
				if(a == null || b == null)
				{
					return new DifferentValueIssue(parent, key, a, b);
				}
			}
			return null;
		}
	}
}
