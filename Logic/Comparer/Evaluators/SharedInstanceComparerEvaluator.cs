using System;
using System.Collections.Generic;

namespace UnityPlugins.Reflection.Logic
{
	public class SharedInstanceComparerEvaluator : AComparerEvaluator
	{
		public readonly HashSet<Type> Whitelist = new HashSet<Type>();

		public SharedInstanceComparerEvaluator()
		{
			Whitelist.Add(typeof(string));
		}

		public override void Reset()
		{
		}

		public override bool Evaluate(ComparisionGroup group)
		{
			if(group.Left == null || group.Right == null)
			{
				return false;
			}

			if(!ReferenceEquals(group.Left, group.Right))
			{
				return false;
			}

			if(Whitelist.Contains(group.Left.GetType()))
			{
				return false;
			}

			group.Entries.Add(new SharedInstanceResultEntry(group.Left));
			return true;
		}
	}
}
