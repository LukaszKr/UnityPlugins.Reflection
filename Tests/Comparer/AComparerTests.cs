using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Tests
{
	public class AComparerTests
	{
		protected ReflectionComparer m_Comparer;

		[SetUp]
		public virtual void Setup()
		{
			m_Comparer = new ReflectionComparer();
		}
	}
}
