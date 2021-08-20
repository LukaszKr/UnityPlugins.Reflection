using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class TypeDifferenceDetector : ADifferenceDetector
	{
		public override ADifference Detect(ObjectDifference parent, string path, object a, object b)
		{
			if(a == null || b == null)
			{
				return null;
			}

			Type typeA = a.GetType();
			Type typeB = b.GetType();
			if(typeA != typeB)
			{
				return new TypeDifference(parent, path, typeA, typeB);
			}
			return null;
		}
	}
}
