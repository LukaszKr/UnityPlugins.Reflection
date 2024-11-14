using System;

namespace UnityPlugins.Reflection.Logic
{
	public abstract class AComparerHandler
	{
		protected readonly ReflectionComparer m_Comparer;

		public AComparerHandler(ReflectionComparer comparer)
		{
			m_Comparer = comparer;
		}

		public abstract bool CanHandle(Type type);
		public abstract void Compare(ComparisionGroup group, Type type);
	}
}
