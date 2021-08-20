using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public abstract class ADifferenceDetector
	{
		public abstract ADifference Detect(ObjectDifference parent, string key, object a, object b);
	}
}
