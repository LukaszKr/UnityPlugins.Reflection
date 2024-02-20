using NUnit.Framework;
using ProceduralLevel.Reflection.Logic;

namespace ProceduralLevel.Reflection.Editor
{
	public static class EditorComparerUtility
	{
		public static readonly ReflectionComparer Comparer = new ReflectionComparer();

		static EditorComparerUtility()
		{

		}

		public static void AssertWithReflection<TType>(ReflectionComparer comparer, TType left, TType right, bool showUI)
		{
			ObjectIssue result = Comparer.Compare(left, right);
			if(result != null)
			{
				if(showUI)
				{
					ReflectionComparerEditorWindow.Show(result, comparer);
				}
				Assert.Fail();
			}
		}

		public static void AssertWithReflection<TType>(TType left, TType right, bool showUI)
		{
			AssertWithReflection(Comparer, left, right, showUI);
		}
	}
}
