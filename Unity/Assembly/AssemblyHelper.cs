using System;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public static class AssemblyHelper
	{
		public static Type FindFirstMatching(string typeName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			int assemblyCount = assemblies.Length;
			for(int x = 0; x < assemblyCount; ++x)
			{
				Assembly assembly = assemblies[x];
				Type[] types = assembly.GetTypes();
				int typeCount = types.Length;
				for(int y = 0; y < typeCount; ++y)
				{
					Type type = types[y];
					if(type.Name == typeName)
					{
						return type;
					}
				}
			}

			return null;
		}
	}
}
