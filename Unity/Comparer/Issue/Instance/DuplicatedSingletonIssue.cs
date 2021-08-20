using System;
namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class DuplicatedSingletonIssue : ADetectedIssue
	{
		public readonly Type SingletonTypeA;
		public readonly Type SingletonTypeB;

		public DuplicatedSingletonIssue(ObjectIssue parent, string key, Type singletonTypeA, Type singletonTypeB)
			: base(parent, key)
		{
			SingletonTypeA = singletonTypeA;
			SingletonTypeB = singletonTypeB;
		}

		protected override string ToStringImpl()
		{
			return $"['{SingletonTypeA.Name}' | '{SingletonTypeB.Name}']";
		}
	}
}
