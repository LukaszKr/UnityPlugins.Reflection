using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Utilities
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class AssemblyUtilityTests
	{
		[Test]
		public void FindAssembly_Returns()
		{
			Assert.IsNotNull(AssemblyUtility.FindAssembly("UnityPlugins.Reflection.Tests"));
		}

		[Test]
		public void FindAssembly_ReturnNull_MissingAssembly()
		{
			Assert.IsNull(AssemblyUtility.FindAssembly("Assembly.That.Doesnt.Exist"));
		}
	}
}
