namespace UnityPlugins.Reflection.Logic
{
	public readonly struct ScannerVisitData
	{
		public readonly object Parent;
		public readonly object Value;

		public ScannerVisitData(object parent, object value)
		{
			Parent = parent;
			Value = value;
		}
	}
}
