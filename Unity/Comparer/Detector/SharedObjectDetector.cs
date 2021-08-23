using System;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public class SharedObjectDetector : AIssueDetector
	{
		private readonly Type m_SharedType;

		public SharedObjectDetector(Type sharedType)
		{
			m_SharedType = sharedType;
		}

		public override bool Compare(ObjectIssue parent, string key, object a, object b)
		{
			if(a == null || b == null)
			{
				return false;
			}
			if(m_SharedType != a.GetType())
			{
				return false;
			}
			if(Equals(a, b))
			{
				parent.Issues.Add(new SharedObjectIssue(parent, key, a));
				return true;
			}
			return false;
		}
	}
}
