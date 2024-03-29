﻿using System;

namespace ProceduralLevel.Reflection.Logic
{
	public class ValueDifferenceDetector : AIssueDetector
	{
		public override bool Compare(ObjectIssue parent, string key, object a, object b)
		{
			Type type;
			if(a == null)
			{
				if(b == null)
				{
					return false;
				}
				type = b.GetType();
			}
			else
			{
				type = a.GetType();
			}
			if(ComparerUtility.IsPrimitive(type))
			{
				if(!Equals(a, b))
				{
					parent.Issues.Add(new DifferentValueIssue(parent, key, a, b));
					return true;
				}
			}
			else
			{
				if(a == null || b == null)
				{
					parent.Issues.Add(new DifferentValueIssue(parent, key, a, b));
					return true;
				}
			}
			return false;
		}
	}
}
