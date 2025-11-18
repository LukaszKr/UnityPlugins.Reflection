namespace UnityPlugins.Reflection.Logic
{
	public abstract class AListValueSource : AValueSource
	{
		public int Index;

		public override string Name => string.Empty;

		public abstract void AddElement();
		public abstract void RemoveAt(int index);
	}
}
