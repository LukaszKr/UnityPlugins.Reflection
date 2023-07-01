using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;

namespace ProceduralLevel.Reflection.Tests
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
