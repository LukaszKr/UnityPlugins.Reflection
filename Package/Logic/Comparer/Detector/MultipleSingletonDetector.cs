using System;

namespace ProceduralLevel.Reflection.Logic
{
	public class MultipleSingletonDetector : AIssueDetector
	{
		private readonly Type m_SingletonType;

		public MultipleSingletonDetector(Type singletonType)
		{
			m_SingletonType = singletonType;
		}

		public override bool Compare(ObjectIssue parent, string key, object a, object b)
		{
			if(a == null || b == null)
			{
				return false;
			}

			if(m_SingletonType != a.GetType())
			{
				return false;
			}
			if(!Equals(a, b))
			{
				parent.Issues.Add(new DuplicatedSingletonIssue(parent, key, a.GetType(), b.GetType()));
				return true;
			}
			return false;
		}
	}
}
