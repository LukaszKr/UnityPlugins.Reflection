using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Reflection.Unity;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ReflectionTypeProvider
	{
		private readonly Dictionary<Type, List<Type>> m_Assignables = new Dictionary<Type, List<Type>>();

		public List<Type> GetConstructableFor(Type type)
		{
			List<Type> types;
			if(!m_Assignables.TryGetValue(type, out types))
			{
				types = AssemblyHelper.GetAllAssignableTo(type);
				int count = types.Count;
				for(int x = count-1; x >= 0; --x)
				{
					Type typeToFilter = types[x];
					if(typeToFilter.IsAbstract)
					{
						types.RemoveAt(x);
						continue;
					}
				}
				m_Assignables[type] = types;
			}
			return types;
		}
	}
}
