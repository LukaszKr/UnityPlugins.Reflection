using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Evaluators
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class SharedInstanceComparerEvaluatorTests : AComparerEvaluatorTests<SharedInstanceComparerEvaluator>
	{
		private struct TestStruct
		{
			public int Value;
		}

		private class TestClass
		{
			public int Value = 1;
		}

		private class WhiteListedClass
		{
		}

		[Test]
		public void NullValue_Both()
		{
			AssertEvaluate(true, null, null);
		}

		[Test]
		public void NullValue_Left()
		{
			AssertEvaluate(true, typeof(int), null);
		}

		[Test]
		public void NullValue_Right()
		{
			AssertEvaluate(true, null, typeof(int));
		}

		[Test]
		public void Value_SamePrimitive()
		{
			AssertEvaluate(true, 1, 1);
		}

		[Test]
		public void Value_SameString()
		{
			string str = "Hello World";
			AssertEvaluate(true, str, str);
		}

		[Test]
		public void Value_SameStruct()
		{
			TestStruct value = new TestStruct() { Value = 1 };
			AssertEvaluate(true, value, value);
		}

		[Test]
		public void Value_SameClassInstance()
		{
			TestClass value = new TestClass();
			AssertEvaluate(false, value, value);
		}

		[Test]
		public void Value_SameClassInstance_Whitelisted()
		{
			WhiteListedClass value = new WhiteListedClass();
			AssertEvaluate(false, value, value);
		}

		#region Helpers
		private void AssertEvaluate(bool isValid, object left, object right)
		{
			AssertEvaluate<SharedInstanceResultEntry>(isValid, left, right);
		}
		#endregion
	}
}
