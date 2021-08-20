using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ValueDifferenceDetector : ADifferenceDetector
	{
		public override ADifference Detect(ObjectDifference parent, string key, object a, object b)
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
					return new ValueDifference(parent, key, a, b);
				}
			}
			else
			{
				if(a == null || b == null)
				{
					return new ValueDifference(parent, key, a, b);
				}
			}
			return null;
		}
	}
}
