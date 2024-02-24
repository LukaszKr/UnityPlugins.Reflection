using System;
using System.Collections.Generic;

namespace ProceduralLevel.Reflection.Logic
{
	public class SharedObjectDetector : AIssueDetector
	{
		public readonly HashSet<Type> CanBeShared = new HashSet<Type>();

		public SharedObjectDetector()
		{
			CanBeShared.Add(typeof(string));
		}

		public override bool Compare(ObjectIssue parent, string key, object a, object b)
		{
			if(a == null || b == null)
			{
				return false;
			}
			Type type = a.GetType();
			if(type.IsPrimitive || type.IsValueType)
			{
				return false;
			}

			if(CanBeShared.Contains(type))
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
