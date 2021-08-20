using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class TypeDifference : ADifference
	{
		public readonly Type TypeA;
		public readonly Type TypeB;

		public TypeDifference(ObjectDifference parent, string path, Type typeA, Type typeB)
			: base(parent, path)
		{
			TypeA = typeA;
			TypeB = typeB;
		}

		protected override string ToStringImpl()
		{
			return $"[{TypeA.Name} =/= {TypeB.Name}]";
		}
	}
}
