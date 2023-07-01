using System;
namespace ProceduralLevel.Reflection.Logic
{
	public class DuplicatedSingletonIssue : ADetectedIssue, IDebugPairIssue
	{
		public readonly Type LeftSingleton;
		public readonly Type RightSingleton;

		public override string Type => "Singleton";
		public string DebugLeft => LeftSingleton.Name;
		public string DebugRight => RightSingleton.Name;

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
