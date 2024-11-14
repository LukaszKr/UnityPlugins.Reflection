using System;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Reflection.Logic
{
	public class PrimitiveComparerHandler : AComparerHandler
	{
		public PrimitiveComparerHandler(ReflectionComparer comparer)
			: base(comparer)
		{
		}

		public override bool CanHandle(Type type)
		{
			return (type.IsPrimitive || type.IsString());
		}

		public override void Compare(ComparisionGroup result, Type type)
		{
			m_Comparer.Evaluate(result);
		}
	}
}
