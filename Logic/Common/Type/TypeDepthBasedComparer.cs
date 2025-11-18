using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class TypeDepthBasedComparer : IComparer<Type>, IComparer<FieldInfo>, IComparer<PropertyInfo>
	{
		private readonly Dictionary<Type, int> m_Depth = new Dictionary<Type, int>();

		private int GetDepth(Type type)
		{
			int depth;
			if(!m_Depth.TryGetValue(type, out depth))
			{
				depth = TypeUtility.GetTypeDepth(type);
				m_Depth[type] = depth;
			}
			return depth;
		}

		public int Compare(Type x, Type y)
		{
			int depthX = GetDepth(x);
			int depthY = GetDepth(y);
			return depthX.CompareTo(depthY);
		}

		public int Compare(FieldInfo x, FieldInfo y)
		{
			return Compare(x.DeclaringType, y.DeclaringType);
		}

		public int Compare(PropertyInfo x, PropertyInfo y)
		{
			return Compare(x.DeclaringType, y.DeclaringType);
		}
	}
}
