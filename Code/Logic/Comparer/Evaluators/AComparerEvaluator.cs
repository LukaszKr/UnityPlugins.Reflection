namespace UnityPlugins.Reflection.Logic
{
	public abstract class AComparerEvaluator
	{
		public abstract void Reset();
		public abstract bool Evaluate(ComparisionGroup group);
	}
}
