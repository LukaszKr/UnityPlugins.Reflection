using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProceduralLevel.UnityPlugins.Comparer.Unity;

namespace ProceduralLevel.UnityPlugins.Comparer.Tests
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
