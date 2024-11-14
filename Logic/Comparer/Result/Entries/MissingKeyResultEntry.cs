using System.Collections;

namespace UnityPlugins.Reflection.Logic
{
	public class MissingKeyResultEntry : AComparisionResultEntry
	{
		public readonly IDictionary PresentIn;
		public readonly IDictionary MissingIn;
		public readonly object Key;

		public MissingKeyResultEntry(IDictionary presentIn, IDictionary missingIn, object key)
		{
			PresentIn = presentIn;
			MissingIn = missingIn;
			Key = key;
		}

		protected override string ToStringImpl()
		{
			return $"{nameof(Key)}: {Key}, {nameof(PresentIn)}: {PresentIn}, {nameof(MissingIn)}: {MissingIn}";
		}
	}
}
