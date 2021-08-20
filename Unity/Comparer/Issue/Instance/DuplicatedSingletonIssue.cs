using System;
namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DuplicatedSingletonIssue : ADetectedIssue
	{
		public readonly Type LeftSingleton;
		public readonly Type RightSingleton;

		public DuplicatedSingletonIssue(ObjectIssue parent, string key, Type leftSingleton, Type rightSingleton)
			: base(parent, key)
		{
			LeftSingleton = leftSingleton;
			RightSingleton = rightSingleton;
		}

		protected override string ToStringImpl()
		{
			return $"['{LeftSingleton.Name}' | '{RightSingleton.Name}']";
		}
	}
}
