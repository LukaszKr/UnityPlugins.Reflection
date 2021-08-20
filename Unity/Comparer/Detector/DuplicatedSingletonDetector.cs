using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DuplicatedSingletonDetector : AIssueDetector
	{
		private readonly Type m_SingletonType;

		public DuplicatedSingletonDetector(Type singletonType)
		{
			m_SingletonType = singletonType;
		}

		public override ADetectedIssue Detect(ObjectIssue parent, string key, object a, object b)
		{
			if(a == null || b == null)
			{
				return null;
			}

			if(m_SingletonType != a.GetType())
			{
				return null;
			}
			if(!Equals(a, b))
			{
				return new DuplicatedSingletonIssue(parent, key, a.GetType(), b.GetType());
			}
			return null;
		}
	}
}
