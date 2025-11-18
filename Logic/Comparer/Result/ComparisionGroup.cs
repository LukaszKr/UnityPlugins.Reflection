using System.Collections.Generic;
using System.Text;

namespace UnityPlugins.Reflection.Logic
{
	public class ComparisionGroup
	{
		private const string PADDING_STR = "    ";

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

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			ToString(sb);
			return sb.ToString();
		}

		public void ToString(StringBuilder sb, string depthPadding = "")
		{
			sb.Append(depthPadding);
			sb.AppendLine($"{Key}");
			depthPadding += PADDING_STR;

			int entriesCount = Entries.Count;
			for(int x = 0; x < entriesCount; ++x)
			{
				AComparisionResultEntry entry = Entries[x];
				sb.Append(depthPadding);
				sb.AppendLine(entry.ToString());
			}

			int subResultsCount = SubResults.Count;
			for(int x = 0; x < subResultsCount; ++x)
			{
				ComparisionGroup subGroup = SubResults[x];
				subGroup.ToString(sb, depthPadding);
			}
		}
	}
}
