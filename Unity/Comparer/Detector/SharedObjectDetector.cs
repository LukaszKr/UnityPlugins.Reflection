using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class SharedObjectDetector : AIssueDetector
	{
		private readonly Type m_SharedType;

		public SharedObjectDetector(Type sharedType)
		{
			m_SharedType = sharedType;
		}

		public override ADetectedIssue Detect(ObjectIssue parent, string key, object a, object b)
		{
			if(a == null || b == null)
			{
				return null;
			}
			if(m_SharedType != a.GetType())
			{
				return null;
			}
			if(Equals(a, b))
			{
				return new SharedObjectIssue(parent, key, a);
			}
			return null;
		}
	}
}
