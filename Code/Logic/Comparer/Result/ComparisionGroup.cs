using System;
using System.Collections.Generic;

namespace UnityPlugins.Reflection.Logic
{
	public class ComparisionGroup
	{
		public readonly ComparisionGroup Parent;
		public readonly string Key;
		public readonly object Left;
		public readonly object Right;

		public readonly List<ComparisionGroup> SubResults = new List<ComparisionGroup>();
		public readonly List<AComparisionResultEntry> Entries = new List<AComparisionResultEntry>();

		public ComparisionGroup(ComparisionGroup parent, string key, object left, object right)
		{
			Parent = parent;

			Key = key;
			Left = left;
			Right = right;
		}
	}
}
