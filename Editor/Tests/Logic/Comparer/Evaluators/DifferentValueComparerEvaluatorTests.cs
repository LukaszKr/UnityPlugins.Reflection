using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Evaluators
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]

	internal class DifferentValueComparerEvaluatorTests : AComparerEvaluatorTests<DifferentValueComparerEvaluator>
	{
		[Test]
		public void NullValue_Both()
		{
			AssertEvaluate(true, null, null);
		}

		[Test]
		public void NullValue_Left()
		{
			AssertEvaluate(false, 1, null);
		}

		[Test]
		public void NullValue_Right()
		{
			AssertEvaluate(false, null, 1);
		}

		[Test]
		public void Value_Same()
		{
			AssertEvaluate(true, 1, 1);
		}

		[Test]
		public void Value_Different()
		{
			AssertEvaluate(false, 1, 2);
		}

		[Test]
		public void DifferentValuesAndTypes_Primitives()
		{
			AssertEvaluate(false, 1, true);
		}

		#region Helpers
		private void AssertEvaluate(bool isValid, object left, object right)
		{
			AssertEvaluate<DifferentValueResultEntry>(isValid, left, right);
		}
		#endregion
	}
}
