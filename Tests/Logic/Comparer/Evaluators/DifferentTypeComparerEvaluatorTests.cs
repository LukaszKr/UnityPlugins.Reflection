using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Evaluators
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]

	internal class DifferentTypeComparerEvaluatorTests : AComparerEvaluatorTests<DifferentTypeComparerEvaluator>
	{
		[Test]
		public void NullType_Both()
		{
			AssertEvaluate(true, null, null);
		}

		[Test]
		public void NullType_Left()
		{
			AssertEvaluate(true, 1, null);
		}

		[Test]
		public void NullType_Right()
		{
			AssertEvaluate(true, null, 1);
		}

		[Test]
		public void Types_Same()
		{
			AssertEvaluate(true, 1, 1);
		}

		[Test]
		public void Types_Different()
		{
			AssertEvaluate(false, 1f, 1);
		}

		#region Helpers
		private void AssertEvaluate(bool isValid, object left, object right)
		{
			AssertEvaluate<DifferentTypeResultEntry>(isValid, left, right);
		}
		#endregion
	}
}
